using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    //바라볼 타겟
    public Transform m_followTarget;
    //타겟과의 카메라거리
    public float m_cameraDist = 3.0f;
    //카메라의 일정 높이
    public float m_cameraHeight = 3.0f;
    //부드러운 회전값
    public float m_dampRotateValue = 5.0f;
    
    void Awake()
    {
        m_followTarget = FindObjectOfType<PlayerController>().m_CameraTarget;
    }

    void LateUpdate()
    {
        CameraDist();

        float curYAngle = Mathf.LerpAngle(transform.eulerAngles.y,
                                            m_followTarget.eulerAngles.y,
                                            m_dampRotateValue * Time.deltaTime);

        //쿼터니언 값으로 변환
        Quaternion rot = Quaternion.Euler(0, curYAngle, 0);
        //카메라 워치를 타겟이 회전한 각도만큼 회전한후
        //디스트만큼 뒤쪽으로 배치하고 헤이트만큼 올려준다.        
        //transform.rotation = rot;
        transform.localPosition = m_followTarget.position - (m_followTarget.forward * m_cameraDist) + (Vector3.up * m_cameraHeight);


        //포지션이 옴겨진 후 타겟을 바라본다.
        //Camera.main.transform.LookAt(m_followTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot,Time.deltaTime * m_dampRotateValue);
    }

    void CameraDist()
    {
        if (m_cameraDist < 1)
        {
            m_cameraHeight = m_cameraDist = 1; 
        }
        else if (m_cameraDist > 3)
        {
            m_cameraHeight = m_cameraDist = 3;
        }
        else
        {
            m_cameraHeight = m_cameraDist -= Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
