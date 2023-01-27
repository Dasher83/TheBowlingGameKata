using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainFormValidator : MonoBehaviour
{
    [SerializeField] 
    private Button _submitButton;
    
    [SerializeField] private TMP_Text _errorLabelShot1;
    [SerializeField] private TMP_Text _errorLabelShot2;

    [SerializeField] private TMP_InputField _shot1Input;
    [SerializeField] private TMP_InputField _shot2Input;

    private int _shot1 = -1;
    private int _shot2 = -1;

    private bool _shot1IsValid = false;
    private bool _shot2IsValid = false;

    private const float TimeBetweenValidations = 0.2f;

    private float validationTimer = TimeBetweenValidations; 

    private bool _isRegularTurn = true;

    public bool IsRegularTurn { set { _isRegularTurn = value; } }

    private bool ValidateInput(string inputValue, out int result)
    {
        bool isParsed = Int32.TryParse(inputValue, out int parsedResult);
        result = -1;

        if (!isParsed) return false;

        if (parsedResult < 0 || parsedResult > 10) return false;

        result = parsedResult;
        return true;
    }

    private void ValidateRegularShot1 ()
    {
        if (string.IsNullOrEmpty(_shot1Input.text)) return;
        _shot1IsValid = ValidateInput(_shot1Input.text, out _shot1);

        if (!_shot1IsValid)
        {
            _errorLabelShot1.text = "The input must be an integer between 10 and 0.";
            return;
        }
        _errorLabelShot1.text = "";
    }

    private void ValidateRegularShot2 ()
    {
        if (string.IsNullOrEmpty(_shot2Input.text)) return;
        _shot2IsValid = ValidateInput(_shot2Input.text, out _shot2);

        if (!_shot2IsValid)
        {
            _errorLabelShot2.text = "The input must be an integer between 10 and 0.";
            _submitButton.interactable = false;
            return;
        }
        _errorLabelShot2.text = "";
    }

    private void ValidateBonusShot1()
    {
        if (string.IsNullOrEmpty(_shot1Input.text)) return;
        _shot1IsValid = ValidateInput(_shot1Input.text, out _shot1);

        if (!_shot1IsValid)
        {
            _errorLabelShot1.text = "The input must be an integer between 10 and 0.";
            _submitButton.interactable = false;
            return;
        }

        _errorLabelShot1.text = "";
    }

    private void ValidateBonusShot2()
    {
        if (string.IsNullOrEmpty(_shot2Input.text)) return;
        _shot2IsValid = ValidateInput(_shot2Input.text, out _shot2);

        if (!_shot2IsValid)
        {
            _errorLabelShot2.text = "The input must be an integer between 10 and 0.";
            _submitButton.interactable = false;
            return;
        }

        _errorLabelShot2.text = "";
    }

    private void ValidateSumOfShots()
    {
        if (_shot1IsValid && _shot2IsValid && _shot1 + _shot2 > 10)
        {
            _shot1IsValid = false;
            _shot2IsValid = false;
            _errorLabelShot1.text = "The sum of both shots must be 10 or less.";
            _errorLabelShot2.text = "The sum of both shots must be 10 or less.";
        }
    }

    private void SubmitButtonToggle()
    {
        if(!_shot1IsValid || !_shot2IsValid)
        {
            _submitButton.interactable = false;
        }
        else
        {
            _submitButton.interactable = true;
        }
    }

    public void CleanTurn()
    {
        _shot1 = -1; 
        _shot2 = -1;
        _shot1IsValid = false;
        _shot2IsValid = false;
    }

    private void Update()
    {
        validationTimer -= Time.deltaTime;
        if (validationTimer > 0) return;

        if (_isRegularTurn)
        {
            ValidateRegularShot1();
            ValidateRegularShot2();
            ValidateSumOfShots();
        }
        else
        {
            ValidateBonusShot1();
            ValidateBonusShot2();
        }

        SubmitButtonToggle();
        validationTimer = TimeBetweenValidations;
    }
}
