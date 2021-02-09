using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalGold : MonoBehaviour
{
    public TextMeshProUGUI finalGoldText;

    // Start is called before the first frame update
    void Start()
    {
        finalGoldText.SetText("You've used up all your extractions! Final Gold: " + StaticDataHolder.finalGold + ".");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
