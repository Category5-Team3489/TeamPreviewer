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
    public TeamPanel teamPanel2;
    public TeamPanel teamPanel3;

    public bool isBlueAlliance = false;

    public bool isAutoPlayEnabled = false;

    private float autoPlayPeriod = 5;
    private float timeToSwitchTeams = 5;

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
        if (isAutoPlayEnabled)
        {
            timeToSwitchTeams -= Time.deltaTime;
            if (timeToSwitchTeams <= 0)
            {
                timeToSwitchTeams += autoPlayPeriod;
                SwitchTeams();
            }
        }
        else
        {
            timeToSwitchTeams = 5;
        }

        if (!Input.GetKeyDown(KeyCode.Enter))
            return;

        float.TryParse(autoPlayPeriodInputField.text, out autoPlayPeriod);
        if (autoPlayPeriod < 5)
            autoPlayPeriod = 5;
    }

    private void SwitchTeams()
    {
        teamPanel1.Load(app.data.GetTeam());
        teamPanel2.Load(app.data.GetTeam());
        teamPanel3.Load(app.data.GetTeam());
    }

    public void AutoPlay()
    {
        isAutoPlayEnabled != isAutoPlayEnabled;
    }

    public void SwitchAlliance()
    {
        isBlueAlliance != isBlueAlliance;
        SetTheme(isBlueAlliance);
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
