using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {

    public Animator m_charAnim = null;
    public Rigidbody m_charRigid = null;

    //기본 스피드 값
    public float m_moveSpeed;
    public float m_runSpeed;
    public float m_rotSpeed;
    public float m_jumpTime = 2f;

    void Awake()
    {
        m_charAnim = GetComponentInChildren<Animator>();
        m_charRigid = GetComponentInChildren<Rigidbody>();
    }
    
    /// <summary>
    /// 캐릭터 방향 애니메이션
    /// </summary>
    /// <param name="inputV"></param>
    /// <param name="inputH"></param>
    public void SetCharAnim(float inputV,float inputH)
    {
        m_charAnim.SetFloat("InputV", inputV);
        m_charAnim.SetFloat("InputH", inputH);
    }

    /// <summary>
    /// 달리기 시작
    /// </summary>
    /// <param name="isRun"></param>
    public void SetRun(bool isRun)
    {
        m_charAnim.SetBool("IsRun", isRun);
    }

    /// <summary>
    /// 점프 시작
    /// </summary>
    /// <param name="isJump"></param>
    public void SetJump(bool isJump)
    {
        m_charAnim.SetBool("IsJump", isJump);
    }
}
