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

    private readonly List<ExportRow> export = new List<ExportRow>();

    public void LoadData()
    {
        LoadExport();
    }

    public void LoadExport()
    {
        export.Clear();
        string[] data = File.ReadAllLines(exportPath);
        for (int i = 1; i < data.Length; i++)
        {
            ExportRow exportRow = new ExportRow(data[i]);
            export.Add(exportRow);
        }
    }

}