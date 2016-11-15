using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UI_TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler,IPointerUpHandler {
    //UICamera
    private Camera m_UICamera;
    //무브UI
    public Image m_touchBG;
    public Image m_touchBtn;

    private Vector3 m_controlVector;

    private bool m_isClick = false;

    private float m_radius = 0.0f;
    private float m_dist = 0;
    
    void Update()
    {
        if (m_isClick)
        {
            Vector2 screenTouchPos = new Vector2(Input.mousePosition.x,
                                                 Input.mousePosition.y);

            Vector3 worldTouchPos = m_UICamera.ScreenToWorldPoint(screenTouchPos);

            m_touchBtn.rectTransform.position = worldTouchPos;

            //로컬에서 z값이 적용되어
            //0으로 바꿔준다.
            Vector3 tempVec = m_touchBtn.rectTransform.localPosition;
            tempVec.z = 0;
            m_touchBtn.rectTransform.localPosition = tempVec;

            //반지름 이상일 경우 
            //최대위치를 반지름으로 바꾼다.
            m_dist = m_touchBtn.rectTransform.localPosition.magnitude;

            if (m_dist > m_radius)
            {
                Vector2 _touchBtn = m_touchBtn.rectTransform.localPosition;
                _touchBtn *= (1f / m_dist);
                m_touchBtn.rectTransform.localPosition = _touchBtn * m_radius;
            }

            m_controlVector = m_touchBtn.transform.localPosition.normalized / m_radius;
        }
    }

    /// <summary>
    /// 클릭
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_UICamera == null)
        {
            //UI카메라를 가져온다
            m_UICamera = GameObject.Find("UI Camera").GetComponent<Camera>();

            m_radius = (m_touchBG.sprite.rect.width * 0.5f);
        }
        m_isClick = true;
    }

    

    /// <summary>
    /// 드래그중
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 땐 클릭
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        m_touchBtn.rectTransform.localPosition = Vector2.zero;
        m_controlVector = Vector3.zero;
        m_isClick = false;
    }

    /// <summary>
    /// 컨트롤 벡터
    /// </summary>
    /// <returns></returns>
    public Vector3 GetControlVector()
    {
        return m_controlVector;
    }

    /// <summary>
    /// 터치방향의 세기에 따라 걷는 속도 변환
    /// </summary>
    /// <returns></returns>
    public float GetDist()
    {
        if (m_dist > 1) m_dist = 1.0f;

        if (m_isClick)
            return m_dist;
        else
            return 0;
    }

    /// <summary>
    /// 클릭여부
    /// </summary>
    /// <returns></returns>
    public bool GetIsClick()
    {
        return m_isClick;
    }


}
