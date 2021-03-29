using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CoreStats))]
public abstract class Enemy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private CoreStats coreStats;

    private void Start()
    {
        target = PlayerManager.instance.Player;
        agent = GetComponent<NavMeshAgent>();
        coreStats = GetComponent<CoreStats>();
        coreStats.onHealthReachedZero += Die;
    }
    private void Update()
    {
        // just follow the player after spawning
        agent.SetDestination(target.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // check for attacking the player
        PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
        if (playerStats)
        {
            Attack(playerStats);
        }
    }

    // attacking action
    private void Attack(PlayerStats playerStats)
    {
        // attack the player
        playerStats.TakeDamage(coreStats.GetDamageAmount());

        // do unique action (die)
        Destroy(gameObject);
    }
    // enemy dies
    protected abstract void Die(IEnemyDiesVisitor visiter);
}
