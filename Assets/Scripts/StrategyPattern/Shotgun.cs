using UnityEngine;

public class Shotgun : BaseGun
{
    [Header("Shotgun Spread")]
    [Range(10f, 120f)]
    public float SpreadAngle = 40f;

    public override void Fire(Transform shotPoint)
    {
        if (ShotCount < 3)
        {
            Debug.LogWarning("Shotgun ShotCount는 최소 3 이상이어야 한다.");
            return;
        }

        float angleStep = SpreadAngle / (ShotCount - 1);
        float startAngle = -SpreadAngle * 0.5f;

        for (int i = 0; i < ShotCount; i++)
        {
            float angleOffset = startAngle + angleStep * i;

            Quaternion bulletRotation =
                shotPoint.rotation * Quaternion.Euler(0f, 0f, angleOffset);

            Vector3 spawnPos = shotPoint.TransformPoint(ShotPointOffset);

            GameObject bulletObject = Instantiate(
                Bullet,
                spawnPos,
                bulletRotation
            );

            bulletObject.GetComponent<Bullet>()
                .Initialize(Damage, Speed);
        }
    }
}
