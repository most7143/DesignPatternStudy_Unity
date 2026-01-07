using UnityEngine;


public class Handgun : BaseGun
{
    public override void Fire(Transform shotPoint)
    {
        for (int i = 0; i < ShotCount; i++)
        {
            Vector3 spawnPos = shotPoint.TransformPoint(ShotPointOffset);

            GameObject bulletObject = Instantiate(
                Bullet,
                spawnPos,
                shotPoint.rotation
            );

            bulletObject.GetComponent<Bullet>()
                .Initialize(Damage, Speed);
        }
    }
}