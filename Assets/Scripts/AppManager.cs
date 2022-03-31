using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] private Image background;

    [SerializeField] private Sprite blueBackground;
    [SerializeField] private Sprite redBackground;

    [SerializeField] private Text autoPlayPeriodInputField;

    public DataManager data;

    public TeamPanel teamPanel1;

    public bool isBlueAlliance = false;

    private void Start()
    {
        SetTheme(isBlueAlliance);

        data = new DataManager();
        data.Load();

        teamPanel1.Load(3489);

        teamPanel1.Load(343);
    }

    private void Update()
    {
        
    }

    public void AutoPlay()
    {

    }

    public void SwitchAlliance()
    {

    }

    public static string FloatToDisplayableString(float f)
    {
        string raw = f.ToString() + "    ";
        string str;
        if (f > 0 && f < 999)
            str = raw[..4].Trim();
        else
            str = raw[..3].Trim();
        return str;
    }

    private void SetTheme(bool isBlueAlliance)
    {
        /*
        if (isBlueAlliance)
            background.sprite = blueBackground;
        else
            background.sprite = redBackground;
        */
        teamPanel1.SetTheme(isBlueAlliance);
    }

}
