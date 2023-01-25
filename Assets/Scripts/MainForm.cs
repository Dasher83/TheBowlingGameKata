using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainForm : MonoBehaviour
{
    [SerializeField]
    private GameObject shot1InputText;

    [SerializeField]
    private GameObject shot2InputText;

    int shot1Score, shot2Score, turnScore;

    private void Start()
    {
        shot1InputText.GetComponent<TMP_InputField>().onSubmit.AddListener((string input) => { shot1Score = TryParseOrZero(input); });
        shot2InputText.GetComponent<TMP_InputField>().onSubmit.AddListener((string input) => { shot2Score = TryParseOrZero(input); });
    }

    private int TryParseOrZero(string input)
    {
        int result;
        bool successfulParse = Int32.TryParse(input, out result);
        return successfulParse ? result : 0;
    }

    public void Submit()
    {
        turnScore = shot1Score + shot2Score;
        Debug.Log($"Your turn score is {turnScore}");
    }    
}
