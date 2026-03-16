using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 1.2f;

    private Transform player;
    private float cooldownTimer;
    private bool isAlive = true;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        cooldownTimer = 0f;
    }

    void Update()
    {
        if (!isAlive) return;
        if (player == null) return;

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(targetPosition);

            if (distance > attackRange)
            {
                transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            }
            else
            {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        if (cooldownTimer > 0f) return;

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            playerCharacter.Hurt(damage);
        }

        cooldownTimer = attackCooldown;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
