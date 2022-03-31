using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class BlockChart : MonoBehaviour
{
    [SerializeField] private RectTransform chart;
    [SerializeField] private RectTransform blockPrefab;

    private List<RectTransform> blocks = new List<RectTransform>();
    private Stack<RectTransform> unusedBlocks = new Stack<RectTransform>();

    public List<RectTransform> contiguousBlocks = new List<RectTransform>();

    public void Init(float[] blockValues)
    {
        contiguousBlocks.Clear();
        foreach (RectTransform bar in blocks)
        {
            unusedBlocks.Push(bar);
            bar.gameObject.SetActive(false);
            bar.anchorMin = Vector2.zero;
            bar.anchorMax = Vector2.zero;
        }
        blocks.Clear();
        float xMin = 0;
        float width = blockValues.Sum();
        for (int i = 0; i < blockValues.Length; i++)
        {
            float x = blockValues[i] / width;
            RectTransform block = ReserveBlock();
            block.anchorMin = new Vector2(xMin, 0);
            block.anchorMax = new Vector2(xMin + x, 1);
            block.localScale = new Vector2(1f / ((1 / x) + 1), 0.5f);
            //blockValues[i] / (blockValues.Length + 1)
            xMin += x;
            blocks.Add(block);
            contiguousBlocks.Add(block);
        }
    }

    public void SetBlockColor(int index, Color color)
    {
        Image image = blocks[index].GetComponent<Image>();
        image.color = new Color(color.r, color.g, color.b, image.color.a);
    }

    private RectTransform ReserveBlock()
    {
        if (unusedBlocks.TryPop(out RectTransform bar))
        {
            bar.gameObject.SetActive(true);
            return bar;
        }
        return Instantiate(blockPrefab, chart);
    }
}