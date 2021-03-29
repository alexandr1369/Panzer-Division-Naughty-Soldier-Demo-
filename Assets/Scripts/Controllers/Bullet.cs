using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private bool isShot; // shooting state

    [SerializeField] private float startLifeTime = 2f; // lifetime
    private float lifeTime;

    private int damageAmount; // current damage

    [SerializeField] private float movementSpeed; // amount of bullet movement speed

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeTime = startLifeTime;
    }
    private void Update()
    {
        // check for started movement
        if (!isShot) return;

        // continue bullet lifetime
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        // check for started movement
        if (!isShot) return;

        // move bullet forward
        Vector3 newPosition = transform.position + transform.forward * movementSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        // check for enemy hurting
        CoreStats enemyStats = other.GetComponentInParent<CoreStats>();
        if (enemyStats)
        {
            // damage enemy
            enemyStats.TakeDamage(damageAmount);

            // 'break' bullet
            Die();
        }
    }

    // bullet is shot
    public void Shoot(int damageAmount)
    {
        isShot = true;
        this.damageAmount = damageAmount;
    }
    // bullet 'dies'
    private void Die()
    {
        Destroy(gameObject);
    }
}
