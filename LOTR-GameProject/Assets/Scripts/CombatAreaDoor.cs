using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAreaDoor : MonoBehaviour
{
    public GameObject[] LinkedEnemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;

        foreach (var enemy in LinkedEnemies)
        {
            var enemyBehaviour = enemy.GetComponent<BaseBehaviour>();

            //Se não possuir o script de behaviour, então a lógica de ativação deste inimigo é diferente
            if (enemyBehaviour == null) continue;

            Debug.Log("Crossed");

            enemyBehaviour.target = other.gameObject;
            enemyBehaviour.ChangeState(BaseBehaviour.EnemyPossibleState.Seek);
        }
    }
}
