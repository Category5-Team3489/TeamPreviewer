using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChart : MonoBehaviour
{
    [SerializeField] private RectTransform chart;
    [SerializeField] private RectTransform barPrefab;

    private List<RectTransform> bars = new List<RectTransform>();
    private Stack<RectTransform> unusedBars = new Stack<RectTransform>();

    public void SetBar(int index, float value)
    {
        if (index < 0 || index >= bars.Count)
            return;
        float width = 1f / bars.Count;
        SetBarAnchors(bars[index], index, width, Mathf.Clamp01(value));
    }

    public List<RectTransform> ReserveBars(int amount)
    {
        foreach (RectTransform bar in bars)
        {
            unusedBars.Enqueue(bar);
            bar.gameObject.SetActive(false);
            bar.anchorMin = Vector2.zero;
            bar.anchorMax = Vector2.zero;
        }
        bars.Clear();
        float width = 1f / amount;
        for (int i = 0; i < amount; i++)
        {
            RectTransform bar = ReserveBar();
            SetBarAnchors(bar, i, width, 0f);
            bars.Add(bar);
        }
        return bars;
    }

    private RectTransform ReserveBar()
    {
        if (unusedBars.Count > 0)
        {
            RectTransform bar = unusedBars.Dequeue();
            bar.gameObject.SetActive(true);
            return bar;
        }
        return Instantiate(barPrefab, chart);
    }

    private void SetBarAnchors(RectTransform bar, int index, float width, float height)
    {
        bar.anchorMin = new Vector2(index * width, 0);
        bar.anchorMax = new Vector2((index * width) + width, height);
    }
}
