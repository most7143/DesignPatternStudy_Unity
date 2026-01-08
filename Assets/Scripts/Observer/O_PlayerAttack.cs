using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    private void Attack()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
            return;

        O_Monster monster = hit.collider.GetComponent<O_Monster>();
        if (monster == null)
            return;

        monster.TakeDamage(1);

    }
}
