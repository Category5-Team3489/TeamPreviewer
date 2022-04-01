using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanel : MonoBehaviour
{
    [SerializeField] private AppManager app;

    [SerializeField] private RawImage robotPicture;

    [SerializeField] private Text teamNumberText;

    [SerializeField] private Text teamNumberInputField;

    [SerializeField] private GameObject pitScoutingPanel;
    [SerializeField] private Text pitScoutingText;

    [SerializeField] private AnswerChart aq1Chart;
    [SerializeField] private AnswerChart aq2Chart;
    [SerializeField] private AnswerChart tq3Chart;
    [SerializeField] private AnswerChart tq4Chart;
    [SerializeField] private AnswerChart eq1Chart;

    [SerializeField] private BarChart aq3Chart;
    [SerializeField] private BarChart aq4Chart;
    [SerializeField] private BarChart tq1Chart;
    [SerializeField] private BarChart tq2Chart;
    [SerializeField] private BarChart eq2Chart;

    private List<ExportRowData> exportRows;

    public int blueTeamNumber = 0;
    public bool bluePitScoutingShown = false;
    public int redTeamNumber = 0;
    public bool redPitScoutingShown = false;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
            return;

        int.TryParse(teamNumberInputField.text, out int teamNumber);
        if (!app.data.TeamNumberExists(teamNumber))
            return;

        SetPitScoutingPanel(false);

        Load(teamNumber);
    }

    public void TogglePitScouting()
    {
        if (app.isBlueAlliance)
        {
            bluePitScoutingShown = !bluePitScoutingShown;
            SetPitScoutingPanel(bluePitScoutingShown);
        }
        else
        {
            redPitScoutingShown = !redPitScoutingShown;
            SetPitScoutingPanel(redPitScoutingShown);
        }
    }

    public void SetTheme(bool isBlueAlliance)
    {
        int teamNumber = isBlueAlliance ? blueTeamNumber : redTeamNumber;
        if (app.data.TeamNumberExists(teamNumber))
        {
            Load(teamNumber);
        }

        Color color = isBlueAlliance ? Color.Lerp(Color.cyan, Color.blue, 0.25f) : Color.red;
        aq1Chart.SetLabelColor(color);
        aq2Chart.SetLabelColor(color);
        tq3Chart.SetLabelColor(color);
        tq4Chart.SetLabelColor(color);
        eq1Chart.SetLabelColor(color);

        aq3Chart.SetLabelColor(color);
        aq4Chart.SetLabelColor(color);
        tq1Chart.SetLabelColor(color);
        tq2Chart.SetLabelColor(color);
        eq2Chart.SetLabelColor(color);
    }

    public void Load(int teamNumber)
    {
        if (!app.data.TeamNumberExists(teamNumber))
            return;

        if (app.isBlueAlliance)
        {
            blueTeamNumber = teamNumber;
            SetPitScoutingPanel(bluePitScoutingShown);
        }
        else
        {
            redTeamNumber = teamNumber;
            SetPitScoutingPanel(redPitScoutingShown);
        }

        teamNumberText.text = $"Team {teamNumber}";

        if (app.data.TryLoadRobotPicture(teamNumber, out Texture2D texture))
            robotPicture.texture = texture;
        else
            robotPicture.texture = null;

        exportRows = app.data.GetTeamExports(teamNumber);

        int rowCount = exportRows.Count;

        List<int> aq1List = GetAnswers(r => r.aq1);
        List<int> aq2List = GetAnswers(r => r.aq2);
        List<int> tq3List = GetAnswers(r => r.tq3);
        List<int> tq4List = GetAnswers(r => r.tq4);
        List<int> eq1List = GetAnswers(r => r.eq1);

        aq1Chart.Load(aq1List);
        aq2Chart.Load(aq2List);
        tq3Chart.Load(tq3List);
        tq4Chart.Load(tq4List);
        eq1Chart.Load(eq1List);



        int aq3Max = GetMaxValue(r => r.aq3);
        int aq4Max = GetMaxValue(r => r.aq4);
        int tq1Max = GetMaxValue(r => r.tq1);
        int tq2Max = GetMaxValue(r => r.tq2);
        int eq2Max = GetMaxValue(r => r.eq2);

        aq3Chart.Init(rowCount, aq3Max);
        aq4Chart.Init(rowCount, aq4Max);
        tq1Chart.Init(rowCount, tq1Max);
        tq2Chart.Init(rowCount, tq2Max);
        eq2Chart.Init(rowCount, eq2Max);

        for (int i = 0; i < rowCount; i++)
        {
            ExportRowData row = exportRows[i];
            aq3Chart.SetBar(i, row.aq3);
            aq4Chart.SetBar(i, row.aq4);
            tq1Chart.SetBar(i, row.tq1);
            tq2Chart.SetBar(i, row.tq2);
            eq2Chart.SetBar(i, row.eq2);
        }
    }

    private void SetPitScoutingPanel(bool isShown)
    {
        int teamNumber = app.isBlueAlliance ? blueTeamNumber : redTeamNumber;
        pitScoutingPanel.SetActive(isShown);
        if (isShown)
        {
            pitScoutingText.text = app.data.GetPitScoutingData(teamNumber);
        }
        else
        {
            pitScoutingText.text = "";
        }
    }

    private List<int> GetAnswers(Func<ExportRowData, int> value)
    {
        List<int> answers = new List<int>();
        foreach (ExportRowData exportRow in exportRows)
        {
            answers.Add(value(exportRow));
        }
        return answers;
    }

    private int GetMaxValue(Func<ExportRowData, int> value)
    {
        int max = int.MinValue;
        foreach (ExportRowData exportRow in exportRows)
        {
            int val = value(exportRow);
            if (val > max)
                max = val;
        }
        return max;
    }
}
