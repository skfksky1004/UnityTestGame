using UnityEngine;
using System.Collections;

public class ZoomInOut : MonoBehaviour {

    private Vector3 firstClick = Vector3.zero;
    private Vector3 secondClick = Vector3.zero;
    private Vector3 distVec = Vector3.zero;

    private float clickDist = 0;
    private float zoomValue = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        OnTouchZoom();
    }

    private void OnTouchZoom()
    {
        if (2 == Input.touchCount)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began &&
                Input.GetTouch(1).phase == TouchPhase.Began)
            {
                firstClick = Input.GetTouch(0).position;
                secondClick = Input.GetTouch(1).position;

                Vector3 beginDistVec = firstClick - secondClick;
                clickDist = beginDistVec.sqrMagnitude;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved &&
                    Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                firstClick = Input.GetTouch(0).position;
                secondClick = Input.GetTouch(1).position;

                distVec = firstClick - secondClick;
                zoomValue = distVec.sqrMagnitude;

                Vector3 _scaleVec = transform.localScale * (zoomValue / clickDist);
                _scaleVec.y = 1;
                transform.localScale = _scaleVec;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended ||
                    Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                firstClick = Vector3.zero;
                secondClick = Vector3.zero;
            }
        }
    }
}
