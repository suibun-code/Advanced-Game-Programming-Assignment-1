using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayMode
{
    Scan, Extract
}

public class Manager : Singleton<Manager>
{
    public PlayMode playMode = PlayMode.Scan;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI modeText;
    public Grid grid;
    public int goldAmount = 0;

    void Start()
    {
        grid.Init();
    }

    public void AddGold(int gold)
    {
        goldAmount += gold;
        goldText.SetText("Amount of gold: " + goldAmount);
    }

    public void ChangeMode()
    {
        if (playMode == PlayMode.Scan)
        {
            playMode = PlayMode.Extract;
            modeText.SetText("Current Mode: Extract");
        }
        else
        {
            playMode = PlayMode.Scan;
           modeText.SetText("Current Mode: Scan");
        }
        
        Debug.Log(playMode);
    }
}
