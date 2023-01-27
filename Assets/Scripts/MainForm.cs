using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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

    [SerializeField]
    private GameObject _shot2Container;

    [SerializeField]
    private TotalScoreComponent _totalScoreComponent;

    [SerializeField]
    private MainFormValidator _formValidator;

    private BowlingTurn[] _turns = new BowlingTurn [11];

    private int _bonusTurnIndex, _lastRegularTurnIndex;


    private int _turnIndex = 0;

    private void Start()
    {
        _bonusTurnIndex = _turns.Length - 1;
        _lastRegularTurnIndex = _turns.Length - 2;
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
        _submitButton.GetComponent<Button>().interactable = false;
    }

    public void Submit()
    {
        _turns[_turnIndex] = new BowlingTurn(TryParseOrZero(shot1InputField.text), TryParseOrZero(shot2InputField.text));
        
        Debug.Log(String.Join(" ", _turns.ToList()));

        

        if (_turnIndex == _lastRegularTurnIndex)
        {
            if (!BowlingScore.IsSpare(_turns[_lastRegularTurnIndex]) && !BowlingScore.IsStrike(_turns[_lastRegularTurnIndex]))
            {
                _submitButton.SetActive(false);
                _turns[_bonusTurnIndex] = new BowlingTurn(0, 0);
            }
            else
            {
                if (BowlingScore.IsSpare(_turns[_lastRegularTurnIndex]))
                {
                    _shot2Container.SetActive(false);
                }
                formTitle.text = "Bonus turn";
                _turnIndex++;
            }
        }
        else if (_turnIndex == _bonusTurnIndex)
        {
            _submitButton.SetActive(false);
        }
        else
        {
            _turnIndex++;
            formTitle.text = "Turn " + (_turnIndex + 1);
        }
        
        ClearFields();
        _formValidator.CleanTurn();

        if (!_submitButton.activeSelf)
        {
            _totalScoreComponent.Score = BowlingScore.Calculate(_turns);
            _totalScoreComponent.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }    
}
