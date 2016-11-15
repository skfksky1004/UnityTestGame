using UnityEngine;
using System.Collections;

public class PlayerController : CharacterBase
{
    //컴포넌트
    private UI_TouchPad m_touchController;
    public Transform m_CameraTarget;

    //키입력값 변수
    private float m_inputV;
    private float m_inputH;
    private bool m_isRun = false;
    private bool m_isJump = false;

	// Use this for initialization
	void Start () {
        m_touchController = Manager_UI.instance.m_controller.m_TouchPad;
    }
	
	// Update is called once per frame
	void Update () {
        InputKeyValue();

        MoveCharacter();
        AnimCharacter();
    }

    //입력 키값 
    //변수로 저장
    void InputKeyValue()
    {
#if __ANDROID__
        m_inputV = m_touchController.GetControlVector().y;
        m_inputH = m_touchController.GetControlVector().x;
#else

        if (m_touchController.GetIsClick())
        {
            m_inputV = m_touchController.GetControlVector().y;
            m_inputH = m_touchController.GetControlVector().x;
        }
        else
        {
            m_inputV = Input.GetAxis("Vertical");
            m_inputH = Input.GetAxis("Horizontal");
        }
        m_isRun = Input.GetKey(KeyCode.LeftShift);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
#endif
    }

    //캐릭터의 움직임
    void MoveCharacter()
    {
        //속도 값 변환
        float speedValue;

        if (!m_isRun) speedValue = m_moveSpeed;
        else speedValue = m_runSpeed;

        if(m_inputH != 0 || m_inputV != 0)
        {
            if (m_touchController.GetIsClick())
            {
                //수평 벡터
                Vector3 inputVec = new Vector3(m_inputH, 0, m_inputV);
                inputVec.Normalize();
                //수평벡터의 각도
                float EularX = Mathf.Acos(inputVec.z) * Mathf.Rad2Deg;
                if (inputVec.x < 0.0f) EularX *= -1.0f;
                //수평최대각 클램핑
                EularX = Mathf.Clamp(EularX, -180, 180);
                //수평으로 돌릴 회전량
                Quaternion tempRot = Quaternion.Euler(0, EularX, 0);
                //회전하기 위한 각차
                float distAngleH = Quaternion.Angle(transform.localRotation, tempRot);
                //이번 프레임에 내가 돌릴 수 있는 량
                float deltaRotX = 720.0f * Time.deltaTime;
                //구면 보간량
                float t = Mathf.Clamp01(deltaRotX / distAngleH);
                //구면 보간량
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                                            tempRot,
                                                            t);

                //무브 캐릭터
                transform.Translate(0, 0, m_touchController.GetDist() * speedValue * Time.deltaTime);
            }
            else
            {
                //수평 벡터
                Vector3 inputVec = new Vector3(m_inputH, 0, m_inputV);

                //무브 캐릭터
                transform.Translate(inputVec * speedValue * Time.deltaTime);
            }
        }
    }

    //캐릭터의 애니메이션
    void AnimCharacter()
    {
        if (m_touchController.GetIsClick())
            SetCharAnim(m_touchController.GetDist(), m_inputH);
        else
            SetCharAnim(m_inputV, m_inputH);
    }

    /// <summary>
    /// 점프
    /// </summary>
    public void Jump()
    {
        if(m_isJump == false)
        {
            StartCoroutine(Jumpping());
        }
    }

    /// <summary>
    /// 점프 시작
    /// </summary>
    /// <param name="isJump"></param>
    private IEnumerator Jumpping()
    {
        this.m_isJump = true;
        SetJump(m_isJump);
        m_charRigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        yield return new WaitForSeconds(m_jumpTime);

        this.m_isJump = false;
        SetJump(m_isJump);
    }

    /// <summary>
    /// 달리기
    /// </summary>
    public void Run(bool isRun)
    {
        this.m_isRun = isRun;

        SetRun(m_isRun);
    }
}
