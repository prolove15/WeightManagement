using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphManager : MonoBehaviour
{

    //------------------------------------------------------------ fields
    [SerializeField]
    Controller controller_Cp;

    [SerializeField]
    GameObject graphPanel_GO;

    Dictionary<DateTime, float> weightRecentHistory = new Dictionary<DateTime, float>();
    Dictionary<DateTime, float> weightYearHistory = new Dictionary<DateTime, float>();

    //------------------------------------------------------------ properties
    UIManager uiManager_Cp
    {
        get { return controller_Cp.uiManager_Cp; }
    }

    CalendarManager calManager_Cp
    {
        get { return controller_Cp.calManager_Cp; }
    }

    FileManager fileManager_Cp
    {
        get { return controller_Cp.fileManager_Cp; }
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

    //---------------------------------------- Init
    public void Init()
    {
        // InitRecentGraphPanel();

        InitYearGraphPanel();
    }

    public void InitRecentGraphPanel()
    {
        SetRecentGraphPanel(DateTime.Now);
    }

    void InitYearGraphPanel()
    {

    }

    //---------------------------------------- Play
    public void Play()
    {

    }

    //---------------------------------------- Set GraphPanel
    void SetRecentGraphPanel(DateTime date_pr)
    {
        Dictionary<DateTime, float> date_tp = fileManager_Cp.ReadWeightRecentData(date_pr);
        Dictionary<float, float> weightPoints_tp = new Dictionary<float, float>();
        
        foreach(DateTime dateTime_tp in date_tp.Keys)
        {
            float x = 1f - ((date_pr - dateTime_tp).Days / 30f);
            float y = date_tp[dateTime_tp] / 150f;
            weightPoints_tp[x] = y;
        }

        // order weight points by size
        float[] xPoints = new List<float>(weightPoints_tp.Keys).ToArray();
        Array.Sort(xPoints);

        Dictionary<float, float> weightPoints = new Dictionary<float, float>();
        foreach(float xPoint_tp in xPoints)
        {
            weightPoints[xPoint_tp] = weightPoints_tp[xPoint_tp];
        }

        // call UI
        uiManager_Cp.SetRecentGraphPanel(weightPoints);
    }

    void SetYearGraphPanel(DateTime date_pr)
    {

    }
    
    //---------------------------------------- set active graph
    public void SetActiveToggleGraphPanel()
    {
        bool activeFlag = !graphPanel_GO.activeInHierarchy;
        graphPanel_GO.SetActive(activeFlag);

        if(activeFlag)
        {
            InitRecentGraphPanel();
        }
    }
}
