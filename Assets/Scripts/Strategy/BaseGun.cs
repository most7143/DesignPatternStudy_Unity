
using System.Collections;
using UnityEngine;
public abstract class BaseGun : MonoBehaviour
{
    public int Damage = 1;
    public int Speed = 1;
    public int ShotCount = 1;
    public float ShotDelay = 0.2f;
    public Vector3 ShotPointOffset;
    public GameObject Bullet;
    public Sprite GunSprite;

    private bool canShoot = true;

    public virtual void Shoot(Transform shotPoint)
    {
        if (!canShoot)
            return;

        Fire(shotPoint);

        StartCoroutine(ShootWithDelay());
    }


    public abstract void Fire(Transform shotPoint);


    IEnumerator ShootWithDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(ShotDelay);
        canShoot = true;
    }
}
