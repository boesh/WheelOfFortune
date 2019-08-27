using System.Collections.Generic;
using System.IO;
using System;

public class LogsManager
{
    string path = "Assets/Logs/PlayerData" + DateTime.Now.ToString("_yyyy_MM_dd_hh_mm_ss") + ".txt";

    public LogsManager(string path)
    {
        this.path = path;
    }

    public void WriteLogsForStart(List<int> nums)
    {
        File.AppendAllText(path, "\nGenerated numbers: ");
        for (int i = 0; i < nums.Count; ++i)
        {

            File.AppendAllText(path, "\n" + nums[i].ToString() + "00");

        }
        File.AppendAllText(path, "\nSpin results:\n");
    }

    public void WriteLogsForQuit(int sumWin)
    {
        File.AppendAllText(path,"Score: " + sumWin.ToString());
    }

    public void WriteLogsForSpinResult(string spinResult)
    {
        File.AppendAllText(path, spinResult + "\n");
    }

}