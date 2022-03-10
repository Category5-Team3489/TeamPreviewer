using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    // For multiple choice, % of answer
    // Horizontal bar graph

    // For numbers:
    // Vertical bar graph of numbers
    // Mean of all matches
    // Mean of past 5 matches

    private string[] data;
    private List<DataRow> dataRows = new List<DataRow>();

    private void Start()
    {
        ReloadData();
        List<RectTransform> bars = barGraph.InitBars(100);
        for (int i = 0; i < bars.Count; i++)
        {
            BarGraph.SetBar(bars[i], i / (float)(bars.Count - 1));
        }
    }

    private void Update()
    {
        
    }

    public void ReloadData()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\TeamPreviewer\export.php";
        data = File.ReadAllLines(path);

    }
}
