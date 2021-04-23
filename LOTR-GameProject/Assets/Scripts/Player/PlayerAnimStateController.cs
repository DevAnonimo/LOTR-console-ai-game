using UnityEngine;

namespace Player
{
    public class PlayerAnimStateController : MonoBehaviour
    {
        public GameObject sword;

        Animator animator;
        int isRunningHash;
        int hasSwordHash;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            isRunningHash = Animator.StringToHash("isRunning");
            hasSwordHash = Animator.StringToHash("hasSword");

        }

        // Update is called once per frame
        void Update()
        {
            bool isRunning = animator.GetBool(isRunningHash);
            bool hasSword = animator.GetBool(hasSwordHash);
            bool forwardPressed = Input.GetKey("w");

            //w key is pressed
            if (!isRunning && forwardPressed)
            {
                animator.SetBool(isRunningHash, true);
            }

            //w key is not pressed
            if (isRunning && !forwardPressed)
            {
                animator.SetBool(isRunningHash, false);
            }

            //has sword?
            if (sword.activeSelf)
                animator.SetBool(hasSwordHash, true);
        }

        public void AttackAnim()
        {
            animator.SetTrigger("isAttacking");
        }

        public void JumpAnim()
        {
            animator.Play("ArmatureSoldier_Jump");
        }

        public void SuperAttackAnim()
        {
            animator.Play("ArmatureSoldier_SpecialAttack");
        }
    }
}
