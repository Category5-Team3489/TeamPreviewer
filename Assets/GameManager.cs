using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BarGraph barGraph;

    private void Start()
    {
        List<RectTransform> bars = barGraph.InitBars(100);
        for (int i = 0; i < bars.Count; i++)
        {
            BarGraph.SetBar(bars[i], i / (float)(bars.Count - 1));
        }
    }

    private void Update()
    {
        
    }
}
