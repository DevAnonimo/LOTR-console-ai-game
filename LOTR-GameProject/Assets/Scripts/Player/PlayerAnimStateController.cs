using UnityEngine;

namespace Scripts.Player
{
    public class PlayerAnimStateController : MonoBehaviour
    {
        public GameObject sword;

        bool forwardPressed;

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

            if(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
                forwardPressed = true;
            else
                forwardPressed = false;

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

        public void AttackAnim(string attack)
        {
            //animator.SetTrigger("isAttacking");

            animator.Play(attack);
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
