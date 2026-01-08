using UnityEngine;

public class O_Score : MonoBehaviour
{
    public int Score { get; private set; }

    private int KillScore = 2;
    private int EscapeScore = -1;

    public void OnMonsterKilled()
    {
        Score += KillScore;
    }

    public void OnMonsterEscaped()
    {
        Score += EscapeScore;
    }
}
