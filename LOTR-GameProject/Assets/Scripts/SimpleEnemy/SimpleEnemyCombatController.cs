using UnityEngine;

namespace Scripts.SimpleEnemy
{
    public class SimpleEnemyCombatController : MonoBehaviour
    {
        public int maxHealth = 100;
        int currentHealth;

        public GameObject obj;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            Debug.Log(currentHealth);
            //Play hurt anim

            if(currentHealth <= 0)
            {
                GameManager x = obj.GetComponent<GameManager>();
                x.DeathCounter();

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
