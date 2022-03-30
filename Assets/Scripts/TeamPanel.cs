using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPanel : MonoBehaviour
{
    [SerializeField] private AppManager app;

    [SerializeField] private BarChart aq3Chart;
    [SerializeField] private BarChart aq4Chart;
    [SerializeField] private BarChart tq1Chart;
    [SerializeField] private BarChart tq2Chart;
    [SerializeField] private BarChart eq2Chart;

    private List<ExportRowData> exportRows;

    public void Load(int teamNumber)
    {
        exportRows = app.data.GetTeamExports(teamNumber);

        int rowCount = exportRows.Count;

        float aq3Max = GetMaxValue(r => r.aq3);
        float aq4Max = GetMaxValue(r => r.aq4);
        float tq1Max = GetMaxValue(r => r.tq1);
        float tq2Max = GetMaxValue(r => r.tq2);
        float eq2Max = GetMaxValue(r => r.eq2);

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

    private float GetMaxValue(Func<ExportRowData, float> value)
    {
        float max = float.MinValue;
        foreach (ExportRowData exportRow in exportRows)
        {
            float val = value(exportRow);
            if (val > max)
                max = val;
        }
        return max;
    }
}
