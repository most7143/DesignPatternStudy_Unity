using System.Collections.Generic;
using UnityEngine;

public class O_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;

    public O_Monster SpawnMonster(Vector3 position)
    {
        GameObject obj = Instantiate(monsterPrefab, position, Quaternion.identity);

        O_Monster monster = obj.GetComponent<O_Monster>();

        return monster;
    }
}
