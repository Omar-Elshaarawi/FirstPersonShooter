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

    // Start is called before the first frame update
    void Start()
    {
        health = 5;

        // Get a reference to all toggleables
        toggleableComponents = GetComponentsInChildren<IToggleable>();
    }

    // Method to call to deal damage to the player
    public void Hurt(int damage)
    {
        health -= damage;

        health = Mathf.Clamp(health, 0, 5);

        Debug.Log($"Health: {health}");

        // If health is zero, turn off moving and shooting
        if (health == 0) {
            foreach (IToggleable component in toggleableComponents) {
                component.ToggleBehavior(false);
            }
        }
    }
}
