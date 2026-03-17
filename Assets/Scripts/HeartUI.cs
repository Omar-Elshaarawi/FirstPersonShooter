using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] PlayerCharacter playerCharacter;
    [SerializeField] GameObject[] hearts;

    void Update()
    {
        int currentHealth = playerCharacter.GetHealth();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}