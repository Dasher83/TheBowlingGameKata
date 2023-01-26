using System;
using System.Linq;
using UnityEngine;
using TMPro;


public class MainForm : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField shot1InputField;

    [SerializeField]
    private TMP_InputField shot2InputField;

    [SerializeField] 
    private TMP_Text formTitle;

    [SerializeField] 
    private GameObject _submitButton;

    private BowlingTurn[] _turns = new BowlingTurn [11];

    private int bonusTurnIndex, lastRegularTurnIndex;


    private int _turnIndex = 0;

    private void Start()
    {
        bonusTurnIndex = _turns.Length - 1;
        lastRegularTurnIndex = _turns.Length - 2;
    }

    private int TryParseOrZero(string input)
    {
        return Int32.TryParse(input, out int result) ? result : 0;
    }

    private void ClearFields()
    {
        shot1InputField.text = "";
        shot2InputField.text = "";
        
        shot1InputField.Select();
    }

    public void Submit()
    {
        _turns[_turnIndex] = new BowlingTurn(TryParseOrZero(shot1InputField.text), TryParseOrZero(shot2InputField.text));
        
        Debug.Log(String.Join(" ", _turns.ToList()));

        

        if (_turnIndex == lastRegularTurnIndex)
        {
            if (!BowlingScore.IsSpare(_turns[lastRegularTurnIndex]) && !BowlingScore.IsStrike(_turns[lastRegularTurnIndex]))
            {
                _submitButton.SetActive(false);
            }
            else
            {
                formTitle.text = "Bonus turn";
                _turnIndex++;
            }
        }
        else if (_turnIndex == bonusTurnIndex)
        {
            _submitButton.SetActive(false);
        }
        else
        {
            _turnIndex++;
            formTitle.text = "Turn " + (_turnIndex + 1);
        }
        
        
        ClearFields();
    }    
}
