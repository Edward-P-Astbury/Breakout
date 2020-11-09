using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Collection of GameObjects
    private List<GameObject> listOfEnemies = new List<GameObject>();

    private float delay = 4f;

    // Start is called before the first frame update
    void Start()
    {
        // Only has to keep track of the reference to the Enemy once
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        print("Number of enemies in the scene: " + listOfEnemies.Count);
    }

    public void KilledEnemy(GameObject enemy)
    {
        if (listOfEnemies.Contains(enemy))
        {
            listOfEnemies.Remove(enemy);

            // Checking if all enemies in the scene are dead
            Invoke("AreEnemiesDead", delay);
        }

        print("Number of enemies in the scene: " + listOfEnemies.Count);
    }

    public bool AreEnemiesDead()
    {
        if(listOfEnemies.Count <= 0)
        {
            print("All enemies are dead");

            FindObjectOfType<GameManager>().EndGame();
            return true;
        }
        else
        {
            return false;
        }
    }
}