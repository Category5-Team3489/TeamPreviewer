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

    private readonly List<ExportRowData> export = new List<ExportRowData>();

    public void Load()
    {
        LoadExport();
    }

    public void LoadExport()
    {
        export.Clear();
        string[] data = File.ReadAllLines(exportPath);
        for (int i = 1; i < data.Length; i++)
        {
            ExportRowData exportRow = new ExportRowData(data[i]);
            export.Add(exportRow);
        }
    }

    public List<ExportRowData> GetTeamExports(int teamNumber)
    {
        List<ExportRowData> teamExportRowData = new List<ExportRowData>();
        foreach (ExportRowData exportRow in export)
        {
            if (exportRow.teamNumber == teamNumber)
                teamExportRowData.Add(exportRow);
        }
        return teamExportRowData;
    }
}