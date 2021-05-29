using Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int LevelEnemies = 0;
    private int currentEnemies = 0;

    public void DeathCounter()
    {
        Debug.Log("Chamou");

        currentEnemies++;

        if(LevelEnemies == currentEnemies)
        {
            gameObject.GetComponent<levelLoader>().LoadNextLevel();
        }
    }
}
