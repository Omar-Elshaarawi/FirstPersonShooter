using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // FEATURE 1: Fixing negative health, this will fix the player to no longer have negative health
    private int health;

    // References to components
    // If the player's health reaches zero, these should be turned off
    private IToggleable[] toggleableComponents;

    // ADDED: reference to the death screen
    [SerializeField] private DeathScreenController deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;

        // Get a reference to all toggleables
        toggleableComponents = GetComponentsInChildren<IToggleable>();

        // ADDED: hide death screen at start
        if (deathScreen != null)
        {
            deathScreen.gameObject.SetActive(false);
        }
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage)
    {
        health -= damage;

        health = Mathf.Clamp(health, 0, 10);

        Debug.Log($"Health: {health}");

        // If health is zero, turn off moving and shooting
        if (health == 0)
        {
            foreach (IToggleable component in toggleableComponents)
            {
                component.ToggleBehavior(false);
            }

            // ADDED: show death screen
            if (deathScreen != null)
            {
                deathScreen.ShowDeathScreen();
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return 10;
    }
}