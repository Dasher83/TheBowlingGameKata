using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainFormValidator : MonoBehaviour
{
    [SerializeField] 
    private Button _submitButton;
    
    [SerializeField] 
    private TMP_Text _errorLabelShot1;
    
    [SerializeField] 
    private TMP_Text _errorLabelShot2;

    [SerializeField] private TMP_InputField _shot1Input;
    
    [SerializeField] private TMP_InputField _shot2Input;

    private int _shot1 = -1;
    
    private int _shot2 = -1;

    private const float TimeBetweenValidations = 0.2f;

    private float validationTimer = TimeBetweenValidations; 
    private bool ValidateInput(string inputValue, out int result)
    {
        bool isParsed = Int32.TryParse(inputValue, out int parsedResult);
        result = -1;

        if (!isParsed) return false;

        if (parsedResult < 0 || parsedResult > 10) return false;

        result = parsedResult;
        return true;
    }

    public void ValidateShot1 ()
    {
        bool isValid = ValidateInput(_shot1Input.text, out _shot1);

        if (!isValid)
        {
            _errorLabelShot1.text = "The input must be an integer between 10 and 0.";
            _submitButton.interactable = false;
            return;
        }

        _errorLabelShot1.text = "";

        if (!(_shot2 >= 0))
        {
            _submitButton.interactable = false;
            return;
        }
        
        if (!(_shot1 + _shot2 <= 10))
        {
            _errorLabelShot1.text = "The sum of both shots must be 10 or less.";
            _submitButton.interactable = false;
            return;
        }
        
        _submitButton.interactable = true;
    }
    
    public void ValidateShot2 ()
    {
        bool isValid = ValidateInput(_shot2Input.text, out _shot2);
        
        if (!isValid)
        {
            _errorLabelShot2.text = "The input must be an integer between 10 and 0.";
            _submitButton.interactable = false;
            return;
        }

        _errorLabelShot2.text = "";

        if (!(_shot1 >= 0))
        {
            _submitButton.interactable = false;
            return;
        }
        
        if (!(_shot1 + _shot2 <= 10))
        {
            _errorLabelShot2.text = "The sum of both shots must be 10 or less.";
            _submitButton.interactable = false;
            return;
        }
        
        _submitButton.interactable = true;
    }

    public void CleanTurn()
    {
        _shot1 = -1; 
        _shot2 = -1;
    }

    private void Update()
    {
        validationTimer -= Time.deltaTime;

        if (validationTimer > 0) return;
        if (!string.IsNullOrEmpty(_shot1Input.text)) ValidateShot1();
        if (!string.IsNullOrEmpty(_shot2Input.text)) ValidateShot2();

        validationTimer = TimeBetweenValidations;
    }
}
