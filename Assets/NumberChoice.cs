using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class NumberChoice : MonoBehaviour
{
    // For numbers:
    // Vertical bar graph of numbers
    // Mean of all matches
    // Mean of past 5 matches
    public BarGraph barGraph;                                                                                                                           
    public Text totalMeanText;
    public Text past5MeanText;

    private List<int> scores = new List<int>();
    private List<RectTransform> bars;

    public void Start()
    {

    }

    private void SetBar(int bar, float value)
    {
        BarGraph.SetBar(bars[bar], value);
    }

    public void UpdateMatches(List<int> newScores)
    {
        scores.Clear();
        scores.AddRange(newScores);

        int maxScore = scores.Max();
        totalMeanText.text = $"Total:{scores.Count}, Max: {maxScore}, Avg: {scores.Average()}";

        bars = barGraph.InitBars(scores.Count);
        for (int i = 0; i < bars.Count; i++)
        {
            SetBar(i, ((float)scores[i]) / (float)maxScore);
        }

        List<int> last5 = new List<int>(scores.Skip(Math.Max(0, scores.Count() - 5)));
        int last5MaxScore = last5.Max();
        past5MeanText.text = $"Past 5, Max: {last5MaxScore}, Avg: {last5.Average()}";
    }
}
