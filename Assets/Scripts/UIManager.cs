using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //------------------------------------------------------------ fields
    [SerializeField]
    Controller controller_Cp;

    [SerializeField]
    GameObject weekday_Pf, day_Pf, weekPanel_Pf;

    [SerializeField]
    GameObject calendarPanel_GO;

    [SerializeField]
    RectTransform weekdayPanel_RT, daysPanel_RT, tracker_RT;

    [SerializeField]
    Text calendarTitleText_Cp, graphTitleText_Cp, yearMonthText_Cp;

    //------------------------------------------------------------ properties
    CalendarManager calendarManager_Cp
    {
        get { return controller_Cp.calManager_Cp; }
    }

    GraphManager graphManager_Cp
    {
        get { return controller_Cp.graphManager_Cp; }
    }

    public string yearMonth
    {
        set { yearMonthText_Cp.text = value; }
    }

    List<RectTransform> weeks_RTs = new List<RectTransform>();
    List<RectTransform> days_RTs = new List<RectTransform>();

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
        
    }

    public void InitWeekdayPanel()
    {
        for(int i = 0; i < 7; i++)
        {
            RectTransform weekday_RT = Instantiate(weekday_Pf, weekdayPanel_RT).GetComponent<RectTransform>();
            weekday_RT.anchorMin = new Vector2(0.01f + 0.14f * i, weekday_RT.anchorMin.y);
            weekday_RT.anchorMax = new Vector2(0.01f + 0.14f * (i + 1), weekday_RT.anchorMax.y);

            Text weekdayText_Cp = weekday_RT.GetComponentInChildren<Text>();
            switch(i)
            {
            case 0:
                weekdayText_Cp.text = "日";
                weekdayText_Cp.color = new Color(1f, 0f, 0f);
                break;
            case 1:
                weekdayText_Cp.text = "月";
                break;
            case 2:
                weekdayText_Cp.text = "火";
                break;
            case 3:
                weekdayText_Cp.text = "水";
                break;
            case 4:
                weekdayText_Cp.text = "木";
                break;
            case 5:
                weekdayText_Cp.text = "金";
                break;
            case 6:
                weekdayText_Cp.text = "土";
                weekdayText_Cp.color = new Color(1f, 0f, 0f);
                break;
            }
        }
    }

    public void InitMonthCalendarPanel()
    {
        for(int i = 0; i < 6; i++)
        {
            RectTransform week_RT_tp = Instantiate(weekPanel_Pf, daysPanel_RT).GetComponent<RectTransform>();
            week_RT_tp.anchorMin = new Vector2(week_RT_tp.anchorMin.x, Mathf.Clamp(1f - 0.17f * i - 0.16f, 0f, 1f));
            week_RT_tp.anchorMax = new Vector2(week_RT_tp.anchorMax.x, 1f - 0.17f * i);

            weeks_RTs.Insert(i, week_RT_tp);
        }
        
        for(int i = 0; i < 31; i++)
        {
            RectTransform day_RT_tp = Instantiate(day_Pf, weeks_RTs[0]).GetComponent<RectTransform>();

            days_RTs.Insert(i, day_RT_tp);
            days_RTs[i].Find("Day Text (Legacy)").GetComponent<Text>().text = (i + 1).ToString();
            days_RTs[i].GetComponent<DayPanel>().dayID = (i + 1);
        }
    }

    //---------------------------------------- Play
    public void Play()
    {

    }

    //---------------------------------------- Set CalendarPanel
    public void SetMonthCalendarPanel(DateTime date_pr)
    {
        // check weeks
        int firstWeekday = ((int)date_pr.DayOfWeek);
        int daysInMonth = DateTime.DaysInMonth(date_pr.Year, date_pr.Month);
        int weekOrder = 0;

        for(int i = 1; i <= daysInMonth; i++)
        {
            DateTime date_tp = new DateTime(date_pr.Year, date_pr.Month, i);

            if((date_tp.Day - (7 - firstWeekday)) > 0)
            {
                weekOrder = (date_tp.Day - (7 - firstWeekday) - 1) / 7 + 1;
            }
        }

        for(int i = 0; i < weeks_RTs.Count; i++)
        {
            if(i > weekOrder)
            {
                weeks_RTs[i].gameObject.SetActive(false);
            }
            else
            {
                weeks_RTs[i].gameObject.SetActive(true);
            }
        }

        // check days
        for(int i = 0; i < days_RTs.Count; i++)
        {
            days_RTs[i].Find("Day InputField (Legacy)").GetComponent<InputField>().text = string.Empty;

            if(i >= DateTime.DaysInMonth(date_pr.Year, date_pr.Month))
            {
                days_RTs[i].gameObject.SetActive(false);
                continue;
            }
            else
            {
                days_RTs[i].gameObject.SetActive(true);
            }

            DateTime date_tp = new DateTime(date_pr.Year, date_pr.Month, i + 1);
            int weekday_tp = ((int)date_tp.DayOfWeek);
            int weekOrder_2 = 0;
            if((date_tp.Day - (7 - firstWeekday)) > 0)
            {
                weekOrder_2 = (date_tp.Day - (7 - firstWeekday) - 1) / 7 + 1;
            }

            days_RTs[i].SetParent(weeks_RTs[weekOrder_2]);
            days_RTs[i].anchorMin = new Vector2(0.01f + 0.14f * weekday_tp, days_RTs[i].anchorMin.y);
            days_RTs[i].anchorMax = new Vector2(0.01f + 0.14f * (weekday_tp + 1), days_RTs[i].anchorMax.y);
            days_RTs[i].offsetMin = new Vector2(0f, 0f);
            days_RTs[i].offsetMax = new Vector2(0f, 0f);
            if(weekday_tp == 0 || weekday_tp == 6)
            {
                days_RTs[i].Find("Day Text (Legacy)").GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
            }
            else
            {
                days_RTs[i].Find("Day Text (Legacy)").GetComponent<Text>().color = new Color(0f, 0f, 0f, 1f);
            }

            if(date_tp.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd"))
            {
                days_RTs[i].Find("TodayMark Panel").gameObject.SetActive(true);
            }
            else
            {
                days_RTs[i].Find("TodayMark Panel").gameObject.SetActive(false);
            }
        }
    }

    public void SetWeightToMonthCalendarPanel(Dictionary<int, string> weightData_pr)
    {
        foreach(int day_tp in weightData_pr.Keys)
        {
            days_RTs[day_tp - 1].Find("Day InputField (Legacy)").GetComponent<InputField>().text
                = weightData_pr[day_tp];
        }
    }

    //---------------------------------------- Set GraphPanel
    public void SetRecentGraphPanel(Dictionary<float, float> weightPoints_pr)
    {
        StartCoroutine(CorouSetRecentGraphPanel(weightPoints_pr));
    }

    IEnumerator CorouSetRecentGraphPanel(Dictionary<float, float> weightPoints_pr)
    {
        int i = 0;
        foreach(float x_tp in weightPoints_pr.Keys)
        {
            if(i == 0)
            {
                tracker_RT.GetComponent<TrailRenderer>().time = 0f;

                tracker_RT.anchorMin = new Vector2(x_tp, weightPoints_pr[x_tp]);
                tracker_RT.anchorMax = tracker_RT.anchorMin;

                yield return new WaitForSeconds(0.3f);
                
                tracker_RT.GetComponent<TrailRenderer>().time = 3600000f;
            }
            else
            {
                tracker_RT.anchorMin = new Vector2(x_tp, weightPoints_pr[x_tp]);
                tracker_RT.anchorMax = tracker_RT.anchorMin;
            }
            
            yield return new WaitForSeconds(Time.deltaTime);
            i++;
        }
    }

    public void SetYearGraphPanel(DateTime date_pr)
    {

    }

    //---------------------------------------- Callback from UI
    public void OnClickExchangeButton()
    {
        calendarPanel_GO.SetActive(!calendarPanel_GO.activeInHierarchy);
        graphManager_Cp.SetActiveToggleGraphPanel();

        string calendarTitle_tp = calendarTitleText_Cp.text;
        calendarTitleText_Cp.text = graphTitleText_Cp.text;
        graphTitleText_Cp.text = calendarTitle_tp;
    }

    public void OnClickNextButton()
    {
        calendarManager_Cp.OnClickNextButton();
    }

    public void OnClickPreviewButton()
    {
        calendarManager_Cp.OnClickPreviewButton();
    }

    public void OnInputWeight(int dayID, float weight_pr, bool deleteFlag_pr)
    {
        calendarManager_Cp.OnInputWeight(dayID, weight_pr, deleteFlag_pr);
    }
}
