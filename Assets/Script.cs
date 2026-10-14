using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Script : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int count = 10;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI moneyInSecond;
    [SerializeField] private TextMeshProUGUI priceUpgrade;
    [SerializeField] private TextMeshProUGUI upgradeCoin;
    private bool beAdv;
    public Button button;
    public Slider slider;
    //private TimerCallback tm;
    private float startTime;
    private float duration = 1f;
    private bool isRunning = false;

    void Start()
    {
        /*tm = new TimerCallback(AddMoneyAuto);
        Timer timer = new Timer(tm, 0, 0, 1000);*/
        StartTimer();
        slider.maxValue = 1500;
        slider.value = 0;
        scoreText.text = "0" + TextTransform(slider.maxValue);
        moneyInSecond.text = 1.ToString();
        priceUpgrade.text = 100.ToString();
        upgradeCoin.text = 2.ToString();
        button.interactable = false;
        beAdv = false;
    }

    void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    //Преобразует число в текст
    private string TextTransform (float maxValue)
    {
        return "/" + maxValue.ToString()[0] + "," + maxValue.ToString()[1] + "k";
    }

    private void AddMoneyAuto()
    {
        score += int.Parse(moneyInSecond.text);
    }

    private void Update()
    {
        if (isRunning && Time.time >= startTime + duration)
        {
            isRunning = false;
            AddMoneyAuto();
            StartTimer();
        }
            scoreText.text = score.ToString() + TextTransform(slider.maxValue);
        slider.value = score;
        if (score >= int.Parse(priceUpgrade.text))
        {
            button.interactable = true;
        }
        if (slider.value >= (slider.maxValue / 2) && !beAdv)
        {
            YG2.InterstitialAdvShow();
            beAdv = true;
        }
        if ((slider.maxValue / 2) > slider.value) { beAdv = false; }
        if (slider.value >= slider.maxValue)
        {
            Restart();
        }
    }

    //Обнуляет счёт, сохраняя улучшение и увеличивая максимальное значение
    private void Restart()
    {
        score = 0;
        slider.maxValue = slider.maxValue + 500;
        slider.value = 0;
        scoreText.text = "0" + TextTransform(slider.maxValue);
        button.interactable = false;
    }

    public void OnClick()
    {
        score += count;
    }

    public void OnClickUpgrade()
    {
        if (score >= int.Parse(priceUpgrade.text))
        {
            score -= int.Parse(priceUpgrade.text);
            moneyInSecond.text = upgradeCoin.text;
            upgradeCoin.text = "+" + (int.Parse(upgradeCoin.text) + 1).ToString();
            priceUpgrade.text = (Math.Floor(int.Parse(priceUpgrade.text) * 1.4)).ToString();
            button.interactable = false;
        }
    }
}
