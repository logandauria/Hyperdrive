/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Owen Sterling & Logan D'Auria
public class EnemyManager : MonoBehaviour
{
    public GameObject[] spawnpoints;

    public GameObject player;
    public GameObject enemyPrefab;

    public bool stopSpawning = false;
    public float spawnTime = 3f;

    public float timer = 0;

    public List<GameObject> enemies = new List<GameObject>();

    static public int score = 0;
    public TMPro.TMP_Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            SpawnCar();
            timer = 0;
        }

        if(enemies.Count > 20)
        {
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
        }

        scoreText.text = "Score: " + score;
    }

    public void SpawnCar()
    {
        if (stopSpawning)
        {
            return;
        }

        int lane = Random.Range(0, spawnpoints.Length);
        enemies.Add(Instantiate(enemyPrefab, spawnpoints[lane].transform.position, spawnpoints[lane].transform.rotation));
    }
        
        
    }
    public static void AvoidedCar()
    {
        score += 100;
    }
    public static void HitByCar()
    {
        score -= 100;
    }
}*/
