
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class CoreManager // переименовтаь
{
    float sumWinNum = 0;
    List<int> nums = new List<int>();
    LogsManager logsManager = new LogsManager("Assets/Logs/PlayerData" + DateTime.Now.ToString("_yyyy_MM_dd_hh_mm_ss") + ".txt");
  
    public void Start()
    {
        logsManager.WriteLogsForStart(nums);
    }

    public void Quit()
    {
        logsManager.WriteLogsForQuit((int)sumWinNum);
    }

    public void SpinResult(string spinResult)
    {
        logsManager.WriteLogsForSpinResult(spinResult);
    }

    public void SetNumsToScoreBoard(string a, Text lastWin, Text sumWin)
    {
        sumWinNum += int.Parse(a);
        lastWin.text = "Last Win: " + (float.Parse(a) / 1000).ToString() + "k";
        if (sumWinNum < 1000000)
        {
            sumWin.text = "Score: " + (sumWinNum / 1000).ToString() + "k";
        }
        else if (sumWinNum >= 1000000)
        {
            string q = (sumWinNum / 1000000).ToString();
            string w = "";
            for (int i = 0; i < q.Length; ++i)
            {

                if (q[i] != ',')
                {
                    w += q[i].ToString();

                }
                if (q[i] == ',')
                {
                    w += "m ";
                    for (int j = 1; j < q.Length - i; ++j)
                    {
                        if (j == 1 && q[j + i] == '0')
                        {
                            if (q[j + i + 1] == '0')
                            {
                                if (q[j + i + 2] == '0')
                                {
                                    if (q[j + i + 3] == '0')
                                    {
                                        sumWin.text = "Score: " + w;
                                        return;
                                    }
                                    sumWin.text = "Score: " + w + q[j + i + 3] + "00";
                                    return;
                                }
                                ++j;
                            }
                        }
                        else
                        {
                            if (j == 4)
                            {
                                w += ',';
                            }
                            w += q[j + i];
                        }
                    }
                    sumWin.text = "Score: " + w + "k";
                    return;
                }
                sumWin.text = "Score: " + w + "m";
            }
        }
    }

    public void NumGenerator(int min, int max)
    {
        int k = UnityEngine.Random.Range(min, max);

        if (max < 1001)
        {
            nums.Add(k);
            NumGenerator(k + 10, k + 30);
        }
        else
        {
            for (int i = nums.Count; i > 16; --i)
            {
                nums.RemoveAt(UnityEngine.Random.Range(0, nums.Count - 1));
            }
        }
        for(int i = nums.Count - 1; i > 0; --i)
        {
            int j = UnityEngine.Random.Range(0, i);
            int v = nums[j];
            nums[j] = nums[i];
            nums[i] = v;
        }
    }

    public void SetNumsToWheelSectors(List<TextMesh> sectors)
    {
        for (int i = 0; i < nums.Count; ++i)
        {
            sectors[i].text = nums[i].ToString() + "00";
        }
    }

    public void SetNumsToWheelSectors(List<Text> sectors)
    {
        for (int i = 0; i < nums.Count; ++i)
        {
            sectors[i].text = nums[i].ToString() + "00";
        }
    }
}