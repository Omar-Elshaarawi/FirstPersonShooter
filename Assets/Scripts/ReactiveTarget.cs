using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private bool _alreadyHit;

    // Start is called before the first frame update
    void Start()
    {
        if (_particles != null)
        {
            var emission = _particles.emission;
            emission.enabled = false;
        }
        _alreadyHit = false;

    }

    // Death animation coroutine
    public IEnumerator Die() {
        // Rotate the game object as if it fell over
        this.transform.Rotate(-75, 0, 0);

        // Turn on particles
        if (_particles != null)
        {
            //_particles. enableEmission = true;buggggggggggggggggggggggggggggg
            var emission = _particles.emission;
            emission.enabled = true;
        }

        // Wait for a few seconds
        yield return new WaitForSeconds(1.5f);

        // Destroy game object
        Destroy(this.gameObject);
    }

    public void ReactToHit() {
        if (_alreadyHit) return;

        _alreadyHit = true;
        Messenger.Broadcast(GameEvent.ENEMY_HIT);


        // Get reference to wandering AI script
        // Pass in FALSE if such a script is attached
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }

        ZombieAI zombie = GetComponent<ZombieAI>();
        if (zombie != null)
        {
            zombie.SetAlive(false);
        }

        
        // Do the same for the FireballShooter script
        Fireball shooter  = GetComponent<Fireball>();
        if (shooter != null) shooter.ChangeFiringState(Fireball.FiringState.PAUSED);


        // Die
        StartCoroutine(Die());
    }
}
