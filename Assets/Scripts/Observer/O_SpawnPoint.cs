using UnityEngine;

public class O_SpawnPoint : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    public void Occupy()
    {
        IsOccupied = true;
    }

    public void Release()
    {
        IsOccupied = false;
    }
}
