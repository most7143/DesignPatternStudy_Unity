using UnityEngine;


namespace Command
{
    public class Character : MonoBehaviour
    {
        [SerializeField] Animator anim;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private bool isJump;
        [SerializeField] private bool isWalking;

        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

        public void Move(Vector2 direction)
        {

            rigid.linearVelocityX = direction.normalized.x * moveSpeed;

            if (direction.x < 0f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x > 0f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            bool isWalking = Mathf.Abs(direction.x) > 0.01f;
            anim.SetBool(IsWalkingHash, isWalking);
        }

        public void Jump()
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
}

