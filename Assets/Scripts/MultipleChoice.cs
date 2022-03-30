using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleChoice : MonoBehaviour
{
    // For multiple choice, % of answer
    // Horizontal bar graph
    public int choicesCount;
    public BarGraph barGraph;

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
}
