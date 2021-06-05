using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    public event UnityAction ContinueButtonClick;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
    }

    private void OnContinueButtonClick()
    {
        ContinueButtonClick?.Invoke();
    }
}
