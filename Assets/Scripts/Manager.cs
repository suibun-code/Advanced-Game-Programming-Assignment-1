using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum PlayMode
{
    Scan, Extract
}

public class Manager : Singleton<Manager>
{
    public PlayMode playMode = PlayMode.Scan;

    public int remainingScans = 8;
    public int remainingExtracts = 3;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI modeText;
    public TextMeshProUGUI remainingText;
    public Grid grid;
    public int goldAmount = 0;

    void Start()
    {
        if (playMode == PlayMode.Scan)
        {
            modeText.SetText("Current Mode: Scan");
            remainingText.SetText("Remaining scans: " + remainingScans);
        }
        else
        {
            modeText.SetText("Current Mode: Extract");
            remainingText.SetText("Remaining extracts: " + remainingExtracts);
        }

        goldText.SetText("Amount of gold: " + goldAmount);
        remainingText.SetText("Remaining scans: " + remainingScans);

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
            remainingText.SetText("Remaining extracts: " + remainingExtracts);

        }
        else
        {
            playMode = PlayMode.Scan;
            modeText.SetText("Current Mode: Scan");
            remainingText.SetText("Remaining scans: " + remainingScans);
        }

        Debug.Log(playMode);
    }

    public void Scanned()
    {
        remainingScans--;
        remainingText.SetText("Remaining scans: " + remainingScans);
    }

    public void Extracted()
    {
        remainingExtracts--;
        remainingText.SetText("Remaining extracts: " + remainingExtracts);

        if (remainingExtracts == 0)
        {
            StaticDataHolder.finalGold = goldAmount;
            SceneManager.LoadScene("ExitScene");
        }
    }
}
