using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{
    [SerializeField] private Chart chart;

    [SerializeField] private string title;

    [SerializeField] private bool ignoreZeroAverage;

    [SerializeField] private Text label;
    [SerializeField] private List<Text> scale;

    [SerializeField] private Image labelImage;

    public void SetLabelColor(Color color)
    {
        labelImage.color = new Color(color.r, color.g, color.b, labelImage.color.a);
    }

    public void Init(int reserveBars, float scaleMax)
    {
        chart.Init(reserveBars, scaleMax);
        InitChartScale();
    }

    public void SetBar(int index, float value)
    {
        chart.SetBar(index, value);
        SetAverageText();
    }

    private void InitChartScale()
    {
        scale[0].text = "0";
        scale[1].text = AppManager.FloatToDisplayableString(chart.ScaleMax * (1f / 4f));
        scale[2].text = AppManager.FloatToDisplayableString(chart.ScaleMax * (2f / 4f));
        scale[3].text = AppManager.FloatToDisplayableString(chart.ScaleMax * (3f / 4f));
        scale[4].text = AppManager.FloatToDisplayableString(chart.ScaleMax);
    }

    private void SetLabel(string subtext)
    {
        label.text = $"<b>{title}</b>\n" + subtext;
    }

    private void SetAverageText()
    {
        int elements = 0;
        float average = 0;
        foreach (float barValue in chart.BarValues)
        {
            if (ignoreZeroAverage && barValue == 0)
                continue;
            average += barValue;
            elements++;
        }
        average /= elements;
        SetLabel($"Match Avg: {AppManager.FloatToDisplayableString(average)}");
    }
}
