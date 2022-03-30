using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public DataManager data;

    public TeamPanel teamPanel1;

    private void Start()
    {
        data = new DataManager();
        data.Load();

        teamPanel1.Load(3489);

        teamPanel1.Load(343);
    }

    private void Update()
    {
        
    }
}
