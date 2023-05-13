using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayPanel : MonoBehaviour
{

    //------------------------------------------------------------ fields
    public int dayID;

    [SerializeField]
    InputField weightInputF_Cp;

    //------------------------------------------------------------ properties
    Controller controller_Cp
    {
        get { return GameObject.FindWithTag("GameController").GetComponent<Controller>(); }
    }

    UIManager uiManager_Cp
    {
        get { return controller_Cp.uiManager_Cp; }
    }

    //------------------------------------------------------------ methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------- Callback from UI
    public void OnInputWeight(string weight_pr)
    {
        float weight_tp = 0f;
        bool deleteFlag = false;

        if(string.IsNullOrEmpty(weight_pr))
        {
            deleteFlag = true;
        }
        else
        {
            if(!float.TryParse(weight_pr, out weight_tp))
            {
                deleteFlag = true;
            }
        }

        if(weight_tp <= 0f)
        {
            deleteFlag = true;
        }
        else if(weight_tp > 150f)
        {
            weight_tp = 150f;
            weightInputF_Cp.text = 150.ToString();
        }

        if(deleteFlag)
        {
            weightInputF_Cp.text = string.Empty;
        }

        uiManager_Cp.OnInputWeight(dayID, weight_tp, deleteFlag);
    }
}
