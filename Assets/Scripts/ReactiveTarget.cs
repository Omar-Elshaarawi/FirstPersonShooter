using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private bool _alreadyHit;

    void Start()
    {
        if (_particles != null)
        {
            _particles.Stop();
        }

        _alreadyHit = false;
    }

    public IEnumerator Die()
    {
        Transform targetToKill = transform.root;

        // Rotate the whole zombie so it falls over
        targetToKill.Rotate(-75f, 0f, 0f);

        // Play particles
        if (_particles != null)
        {
            _particles.Play();
        }

        yield return new WaitForSeconds(1.5f);

        // Destroy the whole zombie object
        Destroy(targetToKill.gameObject);
    }

    public void ReactToHit()
    {
        if (_alreadyHit) return;

        _alreadyHit = true;
        Debug.Log("Enemy was hit: " + gameObject.name);
        Messenger.Broadcast(GameEvent.ENEMY_HIT);

        WanderingAI behavior = GetComponentInParent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
            behavior.enabled = false;
        }

        ZombieAI zombie = GetComponentInParent<ZombieAI>();
        if (zombie != null)
        {
            zombie.SetAlive(false);
            zombie.enabled = false;
        }

        Fireball shooter = GetComponentInParent<Fireball>();
        if (shooter != null)
        {
            shooter.ChangeFiringState(Fireball.FiringState.PAUSED);
        }

        Animator anim = GetComponentInParent<Animator>();
        if (anim != null)
        {
            anim.enabled = false;
        }

        ZombieAttack attack = GetComponentInParent<ZombieAttack>();
        if (attack != null)
        {
            attack.enabled = false;
        }

        StartCoroutine(Die());
    }
}