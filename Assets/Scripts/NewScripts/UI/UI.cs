using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _money;
    [SerializeField] private Text _praise;
    [SerializeField] private float _timeShowPraise;
    [SerializeField] private float _timeChangeMoney;
    [SerializeField] private float _PraiseYMoveDistance;
    [SerializeField] private float _speedPraiseMove;
    [SerializeField] private CanvasGroup _canvasGroup;

    private string _currentPraise;

    public int Coins { get; private set; } = 0;

    private IEnumerator ShowPraiseAnimation()
    {
        _praise.text = "";
        _praise.text = _currentPraise;
        _canvasGroup.alpha = 0;

        _praise.rectTransform.position = new Vector3(_praise.rectTransform.position.x, 1600, _praise.rectTransform.position.z); 

        while (_canvasGroup.alpha <1)
        {
            _canvasGroup.alpha += 0.02f;
            _praise.rectTransform.position = new Vector3(_praise.rectTransform.position.x, _praise.rectTransform.position.y - 0.5f, _praise.rectTransform.position.z);
            yield return new WaitForSeconds(0.01f);
        }


        float elapsedTime = 0;

        while (elapsedTime < _timeShowPraise)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0;

        while (_canvasGroup.alpha != 0)
        {
            _canvasGroup.alpha -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

        _praise.text = "";
    }

    private IEnumerator ChangeMoney(int targetCoinsCount)
    {
        targetCoinsCount += Coins;
        while (targetCoinsCount > Coins)
        {
            Coins++;
            _money.text = Coins.ToString(); 

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void AddMoney(int money)
    {
        StartCoroutine(ChangeMoney(money));
    }

    public void ShowPraise(string praise)
    {
        _currentPraise = praise;
        StopCoroutine(ShowPraiseAnimation());
        StartCoroutine(ShowPraiseAnimation());
    }
}