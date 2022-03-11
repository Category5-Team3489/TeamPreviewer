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

    public NumberChoice aq3;
    public NumberChoice aq4;

    public NumberChoice tq1;
    public NumberChoice tq2;

    public NumberChoice eq2;

    public float autoPlaySpeed;

    public Text input;
    public Text selectedTeamText;

    private List<DataRow> dataRows = new List<DataRow>();
    private List<int> teams = new List<int>();

    private bool autoPlayEnabled = false;
    private float timeUntilSwitchAutoPlay;
    private int teamIndex = 0;

    private void Start()
    {
        ReloadData();
        timeUntilSwitchAutoPlay = autoPlaySpeed;
    }

    private void Update()
    {
        if (autoPlayEnabled)
        {
            timeUntilSwitchAutoPlay -= Time.deltaTime;
            if (timeUntilSwitchAutoPlay <= 0)
            {
                timeUntilSwitchAutoPlay += autoPlaySpeed;
                teamIndex++;
                if (teamIndex >= teams.Count)
                    teamIndex = 0;
                Load(teams[teamIndex]);
            }
        }
    }

    public void GetTeamData()
    {
        autoPlayEnabled = false;
        int.TryParse(input.text, out int teamNumber);
        Load(teamNumber);
    }

    public void Load(int teamNumber)
    {
        selectedTeamText.text = $"Selected: {teamNumber}";
        List<DataRow> teamData = new List<DataRow>();
        foreach (DataRow dataRow in dataRows)
        {
            if (dataRow.team == teamNumber)
                teamData.Add(dataRow);
        }

        List<int> aq3Scores = new List<int>();
        foreach (DataRow dataRow in teamData)
        {
            aq3Scores.Add(dataRow.answers[2]);
        }
        aq3.UpdateMatches(aq3Scores);

        List<int> aq4Scores = new List<int>();
        foreach (DataRow dataRow in teamData)
        {
            aq4Scores.Add(dataRow.answers[3]);
        }
        aq4.UpdateMatches(aq4Scores);

        List<int> tq1Scores = new List<int>();
        foreach (DataRow dataRow in teamData)
        {
            tq1Scores.Add(dataRow.answers[5]);
        }
        tq1.UpdateMatches(tq1Scores);

        List<int> tq2Scores = new List<int>();
        foreach (DataRow dataRow in teamData)
        {
            tq2Scores.Add(dataRow.answers[6]);
        }
        tq2.UpdateMatches(tq2Scores);

        List<int> eq2Scores = new List<int>();
        foreach (DataRow dataRow in teamData)
        {
            eq2Scores.Add(dataRow.answers[9]);
        }
        eq2.UpdateMatches(eq2Scores);
    }

    public void ToggleAutoPlay()
    {
        timeUntilSwitchAutoPlay = 0;
        autoPlayEnabled = !autoPlayEnabled;
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
            int team = int.Parse(rowSplit[1]);
            if (!teams.Contains(team))
                teams.Add(team);
            DataRow dataRow = new DataRow(team, rowSplit[4], answersInt.ToArray());
            dataRows.Add(dataRow);
        }
    }
}
