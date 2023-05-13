using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileManager : MonoBehaviour
{

    //------------------------------------------------------------ fields
    [SerializeField]
    Controller controller;

    string dataRelativePath = "db";

    //------------------------------------------------------------properties
    string dataPath
    {
        get { return Path.Combine(Application.dataPath, dataRelativePath); }
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
        if(!File.Exists(dataPath))
        {
            File.Create(dataPath).Close();
        }
    }

    //---------------------------------------- Play
    public void Play()
    {

    }

    //---------------------------------------- Encrypt/Descryp data
    string EncryptData(string data_pr)
    {
        char[] oldValues = data_pr.ToCharArray();
        char[] newValues = new char[oldValues.Length];

        for(int i = 0; i < oldValues.Length; i++)
        {
            newValues[i] = (char)((byte)oldValues[i] + (byte)32);
        }

        string data_tp = new string(newValues);

        return data_tp;
    }

    string DecryptData(string data_pr)
    {
        char[] oldValues = data_pr.ToCharArray();
        char[] newValues = new char[oldValues.Length];

        for(int i = 0; i < oldValues.Length; i++)
        {
            newValues[i] = (char)((byte)oldValues[i] - (byte)32);
        }

        string data_tp = new string(newValues);

        return data_tp;
    }

    //---------------------------------------- Interact with file
    public Dictionary<int, string> ReadWeightMonthData(DateTime date_pr)
    {
        Dictionary<DateTime, string> allWeightData_tp = ReadAllWeightData();
        Dictionary<int, string> weightMonthData_tp = new Dictionary<int, string>();

        foreach(DateTime date_tp in allWeightData_tp.Keys)
        {
            if(date_tp.Year == date_pr.Year && date_tp.Month == date_pr.Month)
            {
                weightMonthData_tp[date_tp.Day] = allWeightData_tp[date_tp];
            }
        }

        return weightMonthData_tp;
    }

    public Dictionary<DateTime, float> ReadWeightRecentData(DateTime date_pr)
    {
        Dictionary<DateTime, string> allWeightData_tp = ReadAllWeightData();
        Dictionary<DateTime, float> weightMonthData_tp = new Dictionary<DateTime, float>();

        foreach(DateTime date_tp in allWeightData_tp.Keys)
        {
            if((date_pr - date_tp).Days <= 30 && (date_pr - date_tp).Days >= 0)
            {
                weightMonthData_tp[date_tp] = float.Parse(allWeightData_tp[date_tp]);
            }
        }

        return weightMonthData_tp;
    }

    public void ReadWeightYearData(DateTime date_pr)
    {

    }

    Dictionary<DateTime, string> ReadAllWeightData()
    {
        Dictionary<DateTime, string> allWeightData = new Dictionary<DateTime, string>();
        List<string> lines = new List<string>(File.ReadAllLines(dataPath));

        foreach(string line in lines)
        {
            string line_tp = DecryptData(line);
            
            string date_tp = line_tp.Split(":")[0];
            string weight_tp = line_tp.Split(":")[1];

            int year = int.Parse(date_tp.Split("/")[0]);
            int month = int.Parse(date_tp.Split("/")[1]);
            int day = int.Parse(date_tp.Split("/")[2]);

            DateTime dateTime_tp = new DateTime(year, month, day);

            allWeightData[dateTime_tp] = weight_tp;
        }

        return allWeightData;
    }

    public void WriteWeightData(DateTime date_pr, float weight_pr)
    {
        string date_tp = date_pr.ToString("yyyy/MM/dd");
        string weight_tp = weight_pr.ToString();
        string writingData = date_tp + ":" + weight_tp.ToString();

        string dateValue = EncryptData(date_tp);
        string writingValue = EncryptData(writingData);

        List<string> values = new List<string>();
        string[] lines = File.ReadAllLines(dataPath);
        for(int i = 0; i < lines.Length; i++)
        {
            values.Add(lines[i]);
        }

        bool existDateValue = false;
        foreach(string value_tp in values)
        {
            if(value_tp.StartsWith(dateValue))
            {
                int index = values.IndexOf(value_tp);
                values.RemoveAt(index);
                values.Insert(index, writingValue);
                existDateValue = true;
                break;
            }
        }
        if(!existDateValue) 
        {
            values.Add(writingValue);
        }

        File.WriteAllLines(dataPath, values.ToArray());
    }

    public void DeleteWeightData(DateTime date_pr)
    {
        string date_tp = date_pr.ToString("yyyy/MM/dd");
        string dateValue = EncryptData(date_tp);

        List<string> values = new List<string>();
        string[] lines = File.ReadAllLines(dataPath);
        for(int i = 0; i < lines.Length; i++)
        {
            values.Add(lines[i]);
        }

        foreach(string value_tp in values)
        {
            if(value_tp.StartsWith(dateValue))
            {
                int index = values.IndexOf(value_tp);
                values.RemoveAt(index);
                break;
            }
        }

        File.WriteAllLines(dataPath, values.ToArray());
    }
}
