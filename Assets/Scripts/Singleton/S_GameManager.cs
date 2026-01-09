using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    public int StageLevel { get; set; } = 1;

    public static S_GameManager Instance { get; private set; }

    public List<string> stageNames = new List<string>()
    {
        "SingletonScene1",
        "SingletonScene2",
        "SingletonScene3",
    };

    public S_Score ScoreManager { get; private set; } = new S_Score();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void NextStage()
    {
        StageLevel++;
        SceneManager.LoadScene(stageNames[StageLevel - 1]);
    }


}
