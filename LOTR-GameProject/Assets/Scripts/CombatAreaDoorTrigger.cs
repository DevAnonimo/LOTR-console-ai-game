using UnityEngine;

public class CombatAreaDoorTrigger : MonoBehaviour
{
    public delegate void PlayerCrossBattleTrigger(GameObject player);

    public event PlayerCrossBattleTrigger OnPlayerCrossBattleTrigger = player => { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;

        OnPlayerCrossBattleTrigger(other.gameObject);
    }
}
