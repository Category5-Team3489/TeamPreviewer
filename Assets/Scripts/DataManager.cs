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

    public readonly List<int> teams = new List<int>();

    private int nextTeamIndex = 0;

    private Dictionary<int, PitScoutData> pitScouting = new Dictionary<int, PitScoutData>();

    public void Load()
    {
        LoadExport();
    }

    public void LoadExport()
    {
        export.Clear();
        teams.Clear();
        string[] data = File.ReadAllLines(exportPath);
        for (int i = 1; i < data.Length; i++)
        {
            ExportRowData exportRow = new ExportRowData(data[i]);
            export.Add(exportRow);
            int teamNumber = exportRow.teamNumber;
            if (!teams.Contains(teamNumber))
                teams.Add(teamNumber);
        }
        teams.Sort();
    }

    public int GetTeam()
    {
        if (teams.Count < 1)
            return 0;
        int team = teams[nextTeamIndex];
        nextTeamIndex++;
        if (nextTeamIndex >= teams.Count)
            nextTeamIndex = 0;
        return team;
    }

    public bool TryLoadRobotPicture(int teamNumber, out Texture2D robotPicture)
    {
        robotPicture = null;
        string jpg = robotPicturesPath + $"{teamNumber}.jpg";
        if (File.Exists(jpg))
        {
            robotPicture = LoadTexture(jpg);
            return true;
        }
        string jpeg = robotPicturesPath + $"{teamNumber}.jpeg";
        if (File.Exists(jpeg))
        {
            robotPicture = LoadTexture(jpeg);
            return true;
        }
        string png = robotPicturesPath + $"{teamNumber}.png";
        if (File.Exists(png))
        {
            robotPicture = LoadTexture(png);
            return true;
        }
        return false;
    }

    public string GetPitScoutingData(int teamNumber)
    {
        return $"{teamNumber}";
    }

    private static Texture2D LoadTexture(string path)
    {
        byte[] textureData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(textureData);
        return texture;
    }

    public bool TeamNumberExists(int teamNumber)
    {
        foreach (ExportRowData exportRow in export)
        {
            if (exportRow.teamNumber == teamNumber)
                return true;
        }
        return false;
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