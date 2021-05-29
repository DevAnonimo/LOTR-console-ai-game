using UnityEngine;

namespace Scripts.Player
{
    public class PlayerPickupItem : MonoBehaviour
    {
        public GameObject sword;

        public LayerMask pickupLayers;
        public float pickupRange = 1f;
        public Transform pickupPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Detect enemies in range
                Collider[] hitEnemies = Physics.OverlapSphere(pickupPoint.position, pickupRange, pickupLayers);

                //Give damage
                foreach (Collider enemy in hitEnemies)
                {
                    enemy.gameObject.SetActive(false);
                    sword.SetActive(true);
                }

            
            }


        }
    }
}
