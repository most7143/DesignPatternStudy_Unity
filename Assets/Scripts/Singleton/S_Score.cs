using UnityEngine;

public class S_Score
{
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void ResetScore()
    {
        Score = 0;
    }


}
