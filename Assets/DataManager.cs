using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class DataManager
{
    private static readonly string basePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\TeamPreviewer\";
    private static readonly string exportPath = basePath + "export.php";
    private static readonly string robotPicturesPath = basePath + @"robotPictures\";

    public void LoadData()
    {
        string[] data = File.ReadAllLines(exportPath);
        for (int i = 1; i < data.Length; i++)
        {
            List<string> parsedRow = ParseExportRow(data[i]);

        }
    }

    private List<string> ParseExportRow(string row)
    {
        List<string> parsedRow = new List<string>();
        StringBuilder sb = new StringBuilder();
        foreach (char c in row)
        {
            if (c == '\t')
            {
                parsedRow.Add(sb.ToString());
                sb.Clear();
            }
            sb.Append(c);
        }
        return parsedRow;
    }

    public class ExportRow
    {
        // event
        // team #
        // match #
        // scout
        // note
        // aq1 Cargo loaded at start of match: no, yes
        // aq2 Fully exited the tarmac at least once: no, yes
        // aq3 Cargo scored in low goal
        // aq4 Cargo scored in high goal
        // tq1 Cargo in low goal
        // tq2 Cargo in high goal
        // tq3 Max cargo ever in robot: 0, 1, 2, >2
        // tq4 Was penalized at least once: no, yes
        // eq1 Bar climbed: no attempt, failed, low, mid, high, traversal
        // eq2 Climbing time after entering hanging station

    }
}