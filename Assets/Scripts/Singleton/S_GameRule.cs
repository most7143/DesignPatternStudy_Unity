using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

public class S_GameRule : MonoBehaviour
{
    [SerializeField] private float interval = 0.5f;

    public event Action OnSequenceShown;

    public void ShowSequence(List<Image> images, List<int> squence)
    {
        StartCoroutine(ShowSequenceCoroutine(images, squence));
    }

    IEnumerator ShowSequenceCoroutine(List<Image> images, List<int> squence)
    {
        foreach (int index in squence)
        {
            yield return new WaitForSeconds(interval);
            images[index].color = GetColor(index);
            yield return new WaitForSeconds(interval);
            images[index].color = Color.white;

        }

        OnSequenceShown?.Invoke();
    }

    private Color GetColor(int index)
    {
        switch (index)
        {
            case 0:
                return new Color(255f / 255f, 147f / 255f, 0f / 255f, 1f);
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
        }
        return Color.white;
    }


}