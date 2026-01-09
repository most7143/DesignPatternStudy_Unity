using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class S_HUD : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI stageText;

    public TMPro.TextMeshProUGUI resultText;

    [SerializeField] private CanvasGroup fadeCanvasGroup;

    public event Action OnFadeInComplete;

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateStage(int stageLevel)
    {
        stageText.text = $"Stage: {stageLevel}";
    }

    IEnumerator FadeInCoroutine()
    {
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - (elapsed / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;

        OnFadeInComplete?.Invoke();
    }

    public void UpdateResultText(bool isSuccess)
    {
        resultText.text = isSuccess ? "Success!" : "Failed!";

        StartCoroutine(ShowResultTextCoroutine());
    }

    IEnumerator ShowResultTextCoroutine()
    {
        resultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        resultText.gameObject.SetActive(false);
    }



}