using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerChart : MonoBehaviour
{
    [SerializeField] private Chart chart;
    [SerializeField] private BlockChart blockChart;

    [SerializeField] private int answerChoiceCount;
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<Text> occuranceTexts;

    [SerializeField] private Image labelImage;

    public void SetLabelColor(Color color)
    {
        labelImage.color = new Color(color.r, color.g, color.b, labelImage.color.a);
    }

    public void Load(List<int> answers)
    {
        float[] occurances = new float[answerChoiceCount];
        chart.Init(answers.Count, 1);
        for (int i = 0; i < answers.Count; i++)
        {
            int answerChoice = answers[i];
            chart.SetBar(i, 1);
            answerChoice = Math.Clamp(answerChoice, 0, answerChoiceCount - 1);
            chart.SetBarColor(i, colors[answerChoice]);
            occurances[answerChoice]++;
        }
        blockChart.Init(occurances);
        for (int i = 0; i < answerChoiceCount; i++)
        {
            blockChart.SetBlockColor(i, colors[i]);
            float occuranceChance = occurances[i] / answers.Count;
            occuranceTexts[i].text = AppManager.FloatToDisplayableString(100 * occuranceChance) + "%";
            Vector3 pos = occuranceTexts[i].rectTransform.position;
            occuranceTexts[i].rectTransform.position = new Vector3(blockChart.contiguousBlocks[i].position.x, pos.y, pos.z);
            if (occuranceChance == 0)
                occuranceTexts[i].gameObject.SetActive(false);
            else
                occuranceTexts[i].gameObject.SetActive(true);
        }
    }
}