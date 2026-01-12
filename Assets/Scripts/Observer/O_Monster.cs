using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class O_Monster : MonoBehaviour
{
    private float Health = 4;
    private float MaxHealth = 4;


    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Image fillamount;

    public event Action OnDeath;

    public event Action OnRun;

    private void Start()
    {
        StartCoroutine(RunRoutine());
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        UpdateHP();

        if (Health <= 0)
        {
            animator.SetTrigger("Death");
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");

        }
    }

    private void UpdateHP()
    {
        fillamount.fillAmount = Health / MaxHealth;
    }

    private void Die()
    {
        boxCollider.enabled = false;
        OnDeath?.Invoke();
        Destroy(gameObject, 0.5f);
    }

    private void Run()
    {
        OnRun?.Invoke();
        Destroy(gameObject);
    }

    IEnumerator RunRoutine()
    {
        yield return new WaitForSeconds(3f);
        Run();
    }

}
