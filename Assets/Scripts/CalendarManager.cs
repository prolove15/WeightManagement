using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalendarManager : MonoBehaviour
{

    //------------------------------------------------------------ fields
    [SerializeField]
    Controller controller_Cp;

    bool m_currentExchangeFlag;

    DateTime curSelectedDate;

    Dictionary<DateTime, float> weightMonthHistory = new Dictionary<DateTime, float>();

    //------------------------------------------------------------ properties
    UIManager uiManager_Cp
    {
        get { return controller_Cp.uiManager_Cp; }
    }

    FileManager fileManager_Cp
    {
        get { return controller_Cp.fileManager_Cp; }
    }

    public bool currentExchangeFlag
    {
        get { return m_currentExchangeFlag; }
        set
        {
            m_currentExchangeFlag = value;
            // 
        }
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
        curSelectedDate = DateTime.Now;

        InitYearMonthPanel();

        InitWeekdayPanel();

        InitMonthPanel();
    }

    void InitYearMonthPanel()
    {
        SetYearMonthPanel(DateTime.Now);
    }

    void InitWeekdayPanel()
    {
        uiManager_Cp.InitWeekdayPanel();
    }

    void InitMonthPanel()
    {
        uiManager_Cp.InitMonthCalendarPanel();

        SetMonthPanel(DateTime.Now);
    }

    //---------------------------------------- Play
    public void Play()
    {

    }

    //---------------------------------------- Set CalendarPanel
    void SetYearMonthPanel(DateTime date_pr)
    {
        string year = date_pr.Year.ToString();
        string month = date_pr.Month.ToString();
        string yearMonth = year + "年 " + month + "月";

        uiManager_Cp.yearMonth = yearMonth;
    }

    void SetMonthPanel(DateTime date_pr)
    {
        DateTime date_tp = new DateTime(date_pr.Year, date_pr.Month, 1);
        uiManager_Cp.SetMonthCalendarPanel(date_tp);

        SetWeightToMonthCalendarPanel();
    }

    void SetWeightToMonthCalendarPanel()
    {
        uiManager_Cp.SetWeightToMonthCalendarPanel(fileManager_Cp.ReadWeightMonthData(curSelectedDate));
    }

    //---------------------------------------- Callback from UI
    public void OnClickExchangeButton()
    {
        
    }

    public void OnClickNextButton()
    {
        curSelectedDate = curSelectedDate.AddMonths(1);
        SetYearMonthPanel(curSelectedDate);
        SetMonthPanel(curSelectedDate);
    }

    public void OnClickPreviewButton()
    {
        curSelectedDate = curSelectedDate.AddMonths(-1);
        SetYearMonthPanel(curSelectedDate);
        SetMonthPanel(curSelectedDate);
    }

    public void OnInputWeight(int dayID, float weight_pr, bool deleteFlag_pr)
    {
        DateTime date_tp = new DateTime(curSelectedDate.Year, curSelectedDate.Month, dayID);

        if(deleteFlag_pr)
        {
            fileManager_Cp.DeleteWeightData(date_tp);
        }
        else
        {
            fileManager_Cp.WriteWeightData(date_tp, weight_pr);
        }
    }
}
