using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 1;

    //New BUG FIX to pause fireball when hit
    public enum FiringState { ACTIVE, PAUSED }
    private FiringState _state = FiringState.ACTIVE;

    public void ChangeFiringState(FiringState state)
    {
        _state = state;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == FiringState.ACTIVE)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get a reference to the PlayerCharacter component, if there is one
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        // If player is not null, then the fireball has hit the player
        if (player != null) {
            Debug.Log("Player hit!");
            player.Hurt(damage);
        }

        // Destroy game object
        Destroy(gameObject);
    }

        
}
