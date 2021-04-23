using SimpleEnemy;
using UnityEngine;

namespace Player
{
    public class CharacterCombat : MonoBehaviour
    {
        public LayerMask enemyLayers;

        public Transform superAttackPoint;
        public float superAttackRange = 0.5f;
        public int superAttackDamage = 40;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public int attackDamage = 40;

        public float attackRate = 2f;
        float nextAttackTime = 0f;

        private void Update()
        {
            if(Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else if (Input.GetKeyDown(KeyCode.Q)){
                    SuperAttack();
                }
            }

        
        }

        void Attack()
        {
            //Play animation
            gameObject.GetComponent<PlayerAnimStateController>().AttackAnim();

            //Detect enemies in range
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            //Give damage
            foreach(Collider enemy in hitEnemies)
            {
                enemy.GetComponent<SimpleEnemyCombatController>().TakeDamage(attackDamage);
            }
        }

        void SuperAttack()
        {
            //Play animation
            gameObject.GetComponent<PlayerAnimStateController>().SuperAttackAnim();

            //Detect enemies in range
            Collider[] hitEnemies = Physics.OverlapSphere(superAttackPoint.position, superAttackRange, enemyLayers);

            //Give damage
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<SimpleEnemyCombatController>().TakeDamage(superAttackDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            Gizmos.DrawWireSphere(superAttackPoint.position, superAttackRange);
        }

    }
}
