using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{
    [SerializeField] private RectTransform chart;
    [SerializeField] private RectTransform barPrefab;
    [SerializeField] private string title;

    [SerializeField] private bool ignoreZeroAverage;

    [SerializeField] private Text label;
    [SerializeField] private List<Text> scale;

    private List<RectTransform> bars = new List<RectTransform>();
    private Stack<RectTransform> unusedBars = new Stack<RectTransform>();

    private float[] barValues = new float[0];

    private float scaleMax = 1;

    public void Init(int reserveBars, float scaleMax)
    {
        ReserveBars(reserveBars);
        this.scaleMax = scaleMax;
        InitChartScale();
    }

    public void SetBar(int index, float value)
    {
        if (index < 0 || index >= bars.Count)
            return;
        float xScale = 1f / (bars.Count + 1);
        float yScale = (value / scaleMax);
        if (float.IsNaN(yScale))
            yScale = 0;
        bars[index].localScale = new Vector2(xScale * 0.85f, 0.5f * yScale);
        barValues[index] = value;
        SetAverageText();
    }

    public void SetBarColor(int index, Color color)
    {
        Image image = bars[index].GetComponent<Image>();
        image.color = new Color(color.r, color.g, color.b, image.color.a);
    }

    private void ReserveBars(int amount)
    {
        barValues = new float[amount];
        foreach (RectTransform bar in bars)
        {
            unusedBars.Push(bar);
            bar.gameObject.SetActive(false);
            bar.anchorMin = Vector2.zero;
            bar.anchorMax = Vector2.zero;
        }
        bars.Clear();
        float width = 1f / amount;
        for (int i = 0; i < amount; i++)
        {
            RectTransform bar = ReserveBar();
            SetBarAnchors(bar, i, width);
            bars.Add(bar);
            SetBar(i, 0);
        }
    }

    private RectTransform ReserveBar()
    {
        if (unusedBars.TryPop(out RectTransform bar))
        {
            bar.gameObject.SetActive(true);
            return bar;
        }
        return Instantiate(barPrefab, chart);
    }

    private void SetBarAnchors(RectTransform bar, int index, float width)
    {
        bar.anchorMin = new Vector2(index * width, 0);
        bar.anchorMax = new Vector2((index * width) + width, 1);
    }

    private void InitChartScale()
    {
        scale[0].text = "0";
        scale[1].text = FloatToDisplayableString(scaleMax * (1f / 4f));
        scale[2].text = FloatToDisplayableString(scaleMax * (2f / 4f));
        scale[3].text = FloatToDisplayableString(scaleMax * (3f / 4f));
        scale[4].text = FloatToDisplayableString(scaleMax);
    }

    private static string FloatToDisplayableString(float f)
    {
        string raw = f.ToString() + "    ";
        string str;
        if (f > 0 && f < 999)
            str = raw[..4].Trim();
        else
            str = raw[..3].Trim();
        return str;
    }

    private void SetLabel(string subtext)
    {
        label.text = $"<b>{title}</b>\n" + subtext;
    }

    private void SetAverageText()
    {
        int elements = 0;
        float average = 0;
        foreach (float barValue in barValues)
        {
            if (ignoreZeroAverage && barValue == 0)
                continue;
            average += barValue;
            elements++;
        }
        average /= elements;
        SetLabel($"Match Avg: {FloatToDisplayableString(average)}");
    }
}
