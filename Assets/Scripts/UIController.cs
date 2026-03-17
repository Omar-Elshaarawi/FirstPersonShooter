using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    private int _score;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit()
    {
        _score += 1;

        if (scoreLabel != null)
        {
            scoreLabel.text = "Enemies Hit: " + _score;
        }
        else
        {
            Debug.LogWarning("UIController: scoreLabel is not assigned.");
        }
    }

    private void Start()
    {
        _score = 0;

        if (scoreLabel != null)
        {
            scoreLabel.text = "Enemies Hit: " + _score;
        }
        else
        {
            Debug.LogWarning("UIController: scoreLabel is not assigned.");
        }

        if (settingsPopup != null)
        {
            settingsPopup.Close();
        }
        else
        {
            Debug.LogWarning("UIController: settingsPopup is not assigned.");
        }
    }

    void Update()
    {
        // scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSettings()
    {
        Debug.Log("Opening settings ... ");

        if (settingsPopup != null)
        {
            settingsPopup.Open();
        }
        else
        {
            Debug.LogWarning("UIController: settingsPopup is not assigned.");
        }
    }
}