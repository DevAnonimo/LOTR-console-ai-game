using SimpleEnemy;
using UnityEngine;

namespace Player
{
    public class CharacterCombat : MonoBehaviour
    {
        public LayerMask enemyLayers;

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

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }
}
