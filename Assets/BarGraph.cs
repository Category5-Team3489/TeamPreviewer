using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGraph : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform barPrefab;
    private List<RectTransform> bars = new List<RectTransform>();
    private Queue<RectTransform> cachedBars = new Queue<RectTransform>();

    public List<RectTransform> InitBars(int amount)
    {
        foreach (RectTransform rt in bars)
        {
            cachedBars.Enqueue(rt);
            rt.gameObject.SetActive(false);
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
        }
        bars.Clear();
        for (int i = 0; i < amount; i++)
        {
            bars.Add(GetBar());
        }
        Vector3[] corners = new Vector3[4]; // BL, TL, TR, BR
        rect.GetLocalCorners(corners);
        float width = 1f / amount;
        for (int i = 0; i < amount; i++)
        {
            RectTransform bar = bars[i];
            bar.anchorMin = new Vector2(i * width, 0);
            bar.anchorMax = new Vector2((i * width) + width, 1);
            //print("ib");
            bar.localScale = new Vector3((1f / (amount + 1)) * 1.1f, bar.localScale.y, bar.localScale.z); 
        }
        return bars;
    }

    private RectTransform GetBar()
    {
        if (cachedBars.Count > 0)
        {
            RectTransform cachedBar = cachedBars.Dequeue();
            cachedBar.gameObject.SetActive(true);
            return cachedBar;
        }
        return Instantiate(barPrefab, transform);
    }

    public static void SetBar(RectTransform bar, float value)
    {
        //print("sb");
        float y = value / 2;
        if (float.IsNaN(y))
            y = 0;
        bar.localScale = new Vector3(bar.localScale.x, y + 0.01f, bar.localScale.z);
    }

}
