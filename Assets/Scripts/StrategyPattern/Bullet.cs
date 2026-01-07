using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private int speed;

    public void Initialize(int damage, int speed)
    {
        this.damage = damage;
        this.speed = speed;
    }


    private void Start()
    {
        StartCoroutine(DestroyAfterTime(2f));
    }

    private void Update()
    {
        Debug.Log(speed);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}