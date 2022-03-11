using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // For multiple choice, % of answer
    // Horizontal bar graph

    // For numbers:
    // Vertical bar graph of numbers
    // Mean of all matches
    // Mean of past 5 matches

    public NumberChoice numberChoice;

    public Text input;

    private List<DataRow> dataRows = new List<DataRow>();

    private void Start()
    {
        ReloadData();
    }

    private void Update()
    {
        
    }

    public void GetTeamData()
    {
        int.TryParse(input.text, out int teamNumber);
        List<DataRow> teamData = new List<DataRow>();
        foreach (DataRow dataRow in dataRows)
        {
            if (dataRow.team != teamNumber) continue;
            teamData.Add(dataRow);
        }

        List<int> aq3Scores = new List<int>();
        foreach (DataRow dataRow in dataRows)
        {
            aq3Scores.Add(dataRow.answers[2]);
        }
        numberChoice.UpdateMatches(aq3Scores);
    }

    public void ReloadData()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\TeamPreviewer\export.php";
        string[] data = File.ReadAllLines(path);
        dataRows.Clear();
        for (int i = 1; i < data.Length; i++)
        {
            string[] rowSplit = data[i].Split('\t');
            List<string> rowSplitList = new List<string>(rowSplit);
            List<string> answers = rowSplitList.GetRange(5, 10);
            List<int> answersInt = new List<int>();
            foreach (string answer in answers)
            {
                int.TryParse(answer, out int answerInt);
                answersInt.Add(answerInt);
            }
            DataRow dataRow = new DataRow(int.Parse(rowSplit[1]), rowSplit[4], answersInt.ToArray());
            dataRows.Add(dataRow);
        }
    }
}
