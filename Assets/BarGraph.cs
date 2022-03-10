using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGraph : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform barPrefab;
    private List<RectTransform> bars = new List<RectTransform>();

    public List<RectTransform> InitBars(int amount)
    {
        bars.ForEach(bar => Destroy(bar));
        for (int i = 0; i < amount; i++)
        {
            bars.Add(Instantiate(barPrefab, transform));
        }
        Vector3[] corners = new Vector3[4]; // BL, TL, TR, BR
        rect.GetLocalCorners(corners);
        float width = 1f / amount;
        for (int i = 0; i < amount; i++)
        {
            RectTransform bar = bars[i];
            bar.anchorMin = new Vector2(i * width, 0);
            bar.anchorMax = new Vector2((i * width) + width, 1);
            bar.localScale = new Vector3((1f / (amount + 1)) * 1.1f, bar.localScale.y, bar.localScale.z); 
        }
        return bars;
    }

    public static void SetBar(RectTransform bar, float value)
    {
        bar.localScale = new Vector3(bar.localScale.x, value / 2, bar.localScale.z);
    }

}
