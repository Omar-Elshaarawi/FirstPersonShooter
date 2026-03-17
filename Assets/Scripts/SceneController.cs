using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    // Private field for the score
    private int score;

    // Private field for the text object displaying the score
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject winScreen;

    // What prefab to spawn
    [SerializeField] GameObject enemyPrefab;

    // Private field to track a single instance of the enemy
    private GameObject enemy;

    private bool gameWon = false;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void OnEnemyHit()
    {
        score++;
        scoreText.text = $"Enemies hit: {score}";
    }


    // Start is called before the first frame update
    void Start()
    {
        // Set the player's score to 0, then update the text
        score = 0;
        scoreText.text = $"Enemies hit: {score}";

        // Spawn an enemy at the scene controller's position
        Vector3 enemySpawnLocation = new Vector3(0, 0.8f, 0) + gameObject.transform.position;
        enemy = SpawnNewEnemy(enemySpawnLocation);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameWon) return;

        // If there isn't an enemy, spawn one
        if (enemy == null)
        {
            Vector3 enemySpawnLocation = new Vector3(0, 0.8f, 0) + gameObject.transform.position;
            enemy = SpawnNewEnemy(enemySpawnLocation);
        }
    }

    // Method for spawning an enemy at a location
    // Returns a reference to the new enemy made this way
    public GameObject SpawnNewEnemy(Vector3 position)
    {
        GameObject newObject = Instantiate(enemyPrefab);

        newObject.transform.position = position;
        float angle = Random.Range(0, 360);
        newObject.transform.Rotate(0, angle, 0);

        return newObject;
    }

    public void WinGame()
    {
        gameWon = true;
        Debug.Log("YOU WIN");

        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}