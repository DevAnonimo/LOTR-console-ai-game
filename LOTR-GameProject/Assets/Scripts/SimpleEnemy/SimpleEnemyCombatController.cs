using UnityEngine;

namespace SimpleEnemy
{
    public class SimpleEnemyCombatController : MonoBehaviour
    {
        public int maxHealth = 100;
        int currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            //Play hurt anim

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("enemy died");
            //Die animation

            //Disable enemy
            Destroy(gameObject);
        }
    }
}
