using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour
{
    [SerializeField] private RectTransform chart;
    [SerializeField] private RectTransform barPrefab;

    private List<RectTransform> bars = new List<RectTransform>();
    private Stack<RectTransform> unusedBars = new Stack<RectTransform>();

    public float ScaleMax { get; private set; } = 1;
    public float[] BarValues { get; private set; } = new float[0];

    public void Init(int reserveBars, float scaleMax)
    {
        ReserveBars(reserveBars);
        ScaleMax = scaleMax;
    }

    public void SetBar(int index, float value)
    {
        if (index < 0 || index >= bars.Count)
            return;
        float xScale = 1f / (bars.Count + 1);
        float yScale = (value / ScaleMax);
        if (float.IsNaN(yScale))
            yScale = 0;
        bars[index].localScale = new Vector2(xScale * 0.85f, 0.5f * yScale);
        BarValues[index] = value;
    }

    public void SetBarColor(int index, Color color)
    {
        Image image = bars[index].GetComponent<Image>();
        image.color = new Color(color.r, color.g, color.b, image.color.a);
    }

    private void ReserveBars(int amount)
    {
        BarValues = new float[amount];
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
}