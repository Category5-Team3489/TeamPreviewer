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
    public BarGraph barGraph;
    public Text totalMeanText;
    public Text past5MeanText;

    private List<int> scores = new List<int>();
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

    public void UpdateMatches(List<int> newScores)
    {
        scores.Clear();
        scores.AddRange(newScores);

        int maxScore = scores.Max();
        totalMeanText.text = $"All, Max: {maxScore}";

        bars = barGraph.InitBars(scores.Count);
        for (int i = 0; i < scores.Count; i++)
        {
            SetBar()
        }

        List<int> last5 = new List<int>(scores.Skip(Math.Max(0, collection.Count() - 5)));
        int last5MaxScore = last5.Max();
        past5MeanText.text = $"Past 5, Max: {last5MaxScore}";
    }
}
