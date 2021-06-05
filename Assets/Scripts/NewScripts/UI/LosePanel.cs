using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
}
