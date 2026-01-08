using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class O_SceneManager : MonoBehaviour
{
    [SerializeField] private O_Spawner spawner;
    [SerializeField] private List<O_SpawnPoint> spawnPoints;

    [SerializeField] private TextMeshProUGUI PointText;

    private O_Score score = new();


    private O_SpawnPoint recentlyReleasedPoint;
    void Start()
    {
        UpdatePointText();
        StartCoroutine(SpawnMonstersRoutine());
    }


    IEnumerator SpawnMonstersRoutine()
    {
        while (true)
        {
            O_SpawnPoint point = RandomSpawnPointExcept(recentlyReleasedPoint);

            if (point == null)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            point.Occupy();
            O_Monster monster = spawner.SpawnMonster(point.transform.position);

            monster.OnDeath += () =>
            {
                score.OnMonsterKilled();
                UpdatePointText();
                point.Release();
                recentlyReleasedPoint = point;
            };

            monster.OnRun += () =>
            {
                score.OnMonsterEscaped();
                UpdatePointText();
                point.Release();
                recentlyReleasedPoint = point;
            };

            yield return new WaitForSeconds(1f);
        }
    }


    private O_SpawnPoint RandomSpawnPointExcept(O_SpawnPoint except)
    {
        List<O_SpawnPoint> freePoints = new List<O_SpawnPoint>();

        foreach (var point in spawnPoints)
        {
            if (point.IsOccupied)
                continue;

            if (point == except)
                continue;

            freePoints.Add(point);
        }

        if (freePoints.Count == 0)
            return null;

        return freePoints[Random.Range(0, freePoints.Count)];
    }

    private void UpdatePointText()
    {
        PointText.text = $"Point : {score.Score}";
    }
}
