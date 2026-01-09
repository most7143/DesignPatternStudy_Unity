using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;




public class S_GameController : MonoBehaviour
{
    enum GameState
    {
        Fade,

        Idle,

        ShowingSequence,
        WaitingForInput,

        Clear,
    }

    [SerializeField] private S_HUD Hud;
    [SerializeField] private S_GameRule Rule;

    [SerializeField] private List<Image> ShowImages;
    [SerializeField] private List<Button> InputButtons;

    [SerializeField] private List<int> squence = new();
    [SerializeField] private List<int> inputSequence = new();

    [SerializeField] private GameState State = GameState.Fade;

    [SerializeField] private int maxTries = 3;
    private int currentTries = 0;
    [SerializeField] private int sequenceLength = 3;

    void Start()
    {

        Hud.UpdateScore(S_GameManager.Instance.ScoreManager.Score);
        Hud.UpdateStage(S_GameManager.Instance.StageLevel);



        Rule.OnSequenceShown += () =>
        {
            State = GameState.WaitingForInput;
            inputSequence.Clear();
        };

        Hud.OnFadeInComplete += () =>
        {
            State = GameState.Idle;
        };

        Hud.FadeIn();

        for (int index = 0; index < InputButtons.Count; index++)
        {
            int buttonIndex = index;
            InputButtons[index].onClick.RemoveAllListeners();
            InputButtons[index].onClick.AddListener(() => OnInputButtonClicked(buttonIndex));
        }

    }

    private void Update()
    {


        if (State == GameState.Idle && currentTries < maxTries + 1)
        {
            State = GameState.ShowingSequence;
            StartGame();
        }
        else if (IsLastTry() && State != GameState.Clear)
        {
            State = GameState.Clear;
            S_GameManager.Instance.NextStage();
        }


    }




    private void StartGame()
    {
        currentTries++;
        SetSquence(sequenceLength);
        Rule.ShowSequence(ShowImages, squence);
    }


    private void SetSquence(int sequenceLength)
    {
        squence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, ShowImages.Count);
            squence.Add(randomIndex);
        }
    }

    private void OnInputButtonClicked(int index)
    {
        if (State != GameState.WaitingForInput)
            return;

        Debug.Log($"Input Button Clicked: {index}");

        inputSequence.Add(index);

        for (int i = 0; i < inputSequence.Count; i++)
        {
            if (inputSequence[i] != squence[i])
            {
                Fail();
                return;
            }
        }

        if (inputSequence.Count == squence.Count)
        {
            Success();
        }





    }
    private void Success()
    {
        S_GameManager.Instance.ScoreManager.AddScore(10);
        Hud.UpdateScore(S_GameManager.Instance.ScoreManager.Score);
        Hud.UpdateResultText(true);

        if (!IsLastTry())
        {
            State = GameState.Idle;
        }
        else
        {
            State = GameState.Clear;
        }
    }

    private void Fail()
    {
        if (!IsLastTry())
        {
            State = GameState.Idle;
        }
        else
        {
            State = GameState.Clear;
        }

        Hud.UpdateResultText(false);
    }


    private bool IsLastTry()
    {
        return currentTries > maxTries;
    }




}
