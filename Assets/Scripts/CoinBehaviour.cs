using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject coin;

    [SerializeField]
    TextMeshProUGUI coinAmountText;
    public int coinAmount;

    public CanvasGroup coinUI;
    float fadeDuration = 1f;
    public float displayDuration = 2f;

    private void Start()
    {
        coinAmount = PlayerPrefs.GetInt("Coin", 0);
        coinAmountText.text = "Coins: " + coinAmount;
        UpdateCoinText();
    }

    void Update()
    {
        transform.Rotate(0f, 0f, 1f);
        PlayerPrefs.SetInt("Coins", coinAmount);
        PlayerPrefs.Save();
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinAmountText.text = "Coins: " + coinAmount.ToString();
    }

    public void ShowCoinUI()
    {
        LeanTween.cancel(coinUI.gameObject);
        LeanTween.alphaCanvas(coinUI, 1f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(coinUI, 0f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setDelay(displayDuration).setOnComplete(() =>
            {
                coinUI.alpha = 0f;
            });
        });
    }
}
