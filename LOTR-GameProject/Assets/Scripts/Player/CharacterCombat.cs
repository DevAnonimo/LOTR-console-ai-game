using System;
using Scripts.SimpleEnemy;
using UnityEngine;

namespace Scripts.Player
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
        public int maxHealth = 100;

        public float attackRate = 2f;
        float nextAttackTime = 0f;

        bool comboPossible;
        int comboStep = 0;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                SuperAttack();
                comboStep = 0;
            }

            if (Time.time >= nextAttackTime)
            {
            }


            Die();
        }

        public void ComboPossible()
        {
            comboPossible = true;
        }

        public void ComboReset()
        {
            comboPossible = false;
            comboStep = 0;
        }

        public void Combo()
        {
            if (comboStep == 2)
            {
                //Play animation
                gameObject.GetComponent<PlayerAnimStateController>().AttackAnim("Player_Attack 2");
                ApplyDamage();
            }

            if (comboStep == 3)
            {
                //Play animation
                gameObject.GetComponent<PlayerAnimStateController>().AttackAnim("Player_Attack 3");
                ApplyDamage();
            }

            //Die();
        }

        void Attack()
        {
            if (comboStep == 0)
            {
                //Play animation
                gameObject.GetComponent<PlayerAnimStateController>().AttackAnim("Player_Attack");
                comboStep = 1;
                return;
            }

            if (comboStep != 0)
            {
                if (comboPossible)
                {
                    comboPossible = false;
                    comboStep += 1;
                }
            }

            ApplyDamage();
        }

        void ApplyDamage()
        {
            //Detect enemies in range
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            //Give damage
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<SimpleEnemyCombatController>().TakeDamage(attackDamage);
            }
        }

        public void TakeDamage(int damage) => _currentHealth -= damage;

        public void Die()
        {
            if (_currentHealth <= 0)
                Destroy(gameObject);
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
