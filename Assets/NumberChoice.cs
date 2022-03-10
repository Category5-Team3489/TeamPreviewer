using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberChoice : MonoBehaviour
{
    // For numbers:
    // Vertical bar graph of numbers
    // Mean of all matches
    // Mean of past 5 matches
    public int matchesCount;
    public BarGraph barGraph;
    public Text totalMeanText;
    public Text past5MeanText;

    private List<int> scores = new List<int>()
    private List<RectTransform> bars;

    public void Start()
    {
        bars = barGraph.InitBars(choicesCount);
        foreach (RectTransform bar in bars)
        {
            BarGraph.SetBar(bar, 0f);
        }
    }

    public void SetBar(int bar, float value)
    {
        BarGraph.SetBar(bars[bar], 0f);
    }

    public void UpdateMatches(List<int> scores)
    {

    }
}
