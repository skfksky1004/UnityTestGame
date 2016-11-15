using UnityEngine;
using System.Collections;

public class Manager_UI : MonoBehaviour {

    //private GameObject str_Prefabs = Resources.Load("Prefabs/UIRoot") as GameObject;

    private static Manager_UI _instance;
    public static Manager_UI instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (Manager_UI)FindObjectOfType(typeof(Manager_UI));
                if(_instance == null)
                {
                    _instance = Instantiate(Resources.Load("Prefabs/UIRoot") as GameObject).GetComponent<Manager_UI>();
                    DontDestroyOnLoad(_instance);
                }
            }
            return _instance;
        }
    }
    
    public UI_Controller m_controller = null;

    void OnEnable()
    {
        m_controller = GetComponentInChildren<UI_Controller>();
    }
}
