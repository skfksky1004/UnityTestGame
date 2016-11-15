using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateDamege : MonoBehaviour {

    public Camera m_UICamera;

    public GameObject m_DamegeText;

    public GameObject m_DamegeTextPanel;

    public Transform m_tagetPos;


    string tempStr = "atabad";
    char[] tempArr;

    // Use this for initialization
    void Start ()
    {
        tempArr = tempStr.ToCharArray();
        Debug.Log(tempArr[0]+"  " + 
                    tempArr[1] + "  " +
                    tempArr[2] + "  " +
                    tempArr[3] + "  " +
                    tempArr[4] + "  " +
                    tempArr[5] + "  ");
    }
	
	// Update is called once per frame
	void Update () {
        TestScreenPos();
    }


    void TestScreenPos()
    {
        float ScreenX = Screen.width;  //스크린 가로 길이
        float ScreenY = Screen.height;  //스크린 세로 길이
        float ScreenResolution = ScreenX / ScreenY;

        Debug.Log("스크린X" + ScreenX + "스크린Y" + ScreenY + "평균" + ScreenResolution);
                        
        Vector3 ScreenPos = m_UICamera.WorldToScreenPoint(m_tagetPos.position);  //스크린 포지션을 구합니다. 

        Debug.Log("  X  " + ScreenPos.x.ToString("##") + 
                  "  Y  " + ScreenPos.y.ToString("##") + 
                  "  Z  " + ScreenPos.z.ToString("##"));
        
        float UIHeight = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution.y;
        float UIWidth = Mathf.Floor(UIHeight * ScreenResolution);  //NGUI의 가로 픽셀.

        Debug.Log("  Height  "+ UIHeight + "  Width  " + UIWidth);


        Vector3 Resoul = new Vector3(ScreenX, ScreenY, 0f) * 0.5f;   // 가로 세로의 중간 크기 값
        Vector2 ScrRes = new Vector2(UIWidth / ScreenX, UIHeight / ScreenY);  // NGUI와 스크린 사이즈의 보정 값
        
        Debug.Log("  ScrResX  " + ScrRes.x + "  ScrResY  " + ScrRes.y);

        ScreenPos = ScreenPos - Resoul;  // 기본적인  좌표 보정
        ScreenPos = new Vector3(ScreenPos.x - ScrRes.x, ScreenPos.y - ScrRes.y, 0f); // NGUI와 해상도를 비교해서 좌표보정
        
        m_DamegeText.GetComponent<RectTransform>().localPosition = ScreenPos;
    }
}
