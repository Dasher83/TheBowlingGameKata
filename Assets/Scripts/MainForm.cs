using System;
using UnityEngine;
using TMPro;


public class MainForm : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField shot1InputField;

    [SerializeField]
    private TMP_InputField shot2InputField;

    private int turnScore;

    private int TryParseOrZero(string input)
    {
        int result;
        bool successfulParse = Int32.TryParse(input, out result);
        return successfulParse ? result : 0;
    }

    public void Submit()
    {
        turnScore = TryParseOrZero(shot1InputField.text) + TryParseOrZero(shot2InputField.text);
        Debug.Log($"Your turn score is {turnScore}");
    }    
}
