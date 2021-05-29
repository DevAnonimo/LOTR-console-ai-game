using Scripts.Player;
using UnityEngine;

namespace Scripts
{
    public class CombatAreaDoorTrigger : MonoBehaviour
    {
        public delegate void PlayerCrossBattleTrigger();

        public event PlayerCrossBattleTrigger OnPlayerCrossBattleTrigger = () => { };

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CameraPrive") == false) return;
            OnPlayerCrossBattleTrigger();
        }
    }
}
