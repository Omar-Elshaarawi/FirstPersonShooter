using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;

    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (cooldownTimer > 0f) return;

        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(damage);
            cooldownTimer = attackCooldown;
        }
    }
}