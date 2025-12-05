using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyDroneAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Movement Settings")]
    public float patrolSpeed = 5f;
    public float chaseSpeed = 8f;
    public float patrolRadius = 20f;
    public float minPatrolHeight = -6f;
    public float maxPatrolHeight = 6f;
    public float detectionRange = 25f;
    public float attackRange = 15f;
    public float obstacleAvoidanceDistance = 6f;
    public LayerMask obstacleMask;

    [Header("Attack Settings")]
    public float fireRate = 1f;
    public float projectileForce = 30f;

    [Header("Steering/Behavior")]
    public float avoidWeight = 6f;
    public float wanderAmount = 2f;

    private Vector3 patrolTarget;
    private float nextFireTime;
    private Rigidbody rb;

    enum State { Patrol, Chase, Attack }
    State currentState = State.Patrol;

    // ---------------- START ----------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }

        ChooseNewPatrolPoint();
    }

    // ---------------- UPDATE ----------------
    void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange) currentState = State.Attack;
        else if (dist <= detectionRange) currentState = State.Chase;
        else currentState = State.Patrol;

        switch (currentState)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: ChasePlayer(); break;
            case State.Attack: AttackPlayer(); break;
        }
    }

    // ---------------- PATROL ----------------
    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolTarget) < 2f)
            ChooseNewPatrolPoint();

        Vector3 direction = (patrolTarget - transform.position).normalized;

        direction += ObstacleAvoidanceVector() * avoidWeight;
        direction += Random.insideUnitSphere * wanderAmount * 0.2f;
        direction.Normalize();

        MoveDrone(direction, patrolSpeed);

        // 🔥 LookAt target point (Use LookAt like you requested)
        transform.LookAt(transform.position + direction);
    }

    void ChooseNewPatrolPoint()
    {
        Vector3 random = Random.insideUnitSphere * patrolRadius;
        random.y = Mathf.Clamp(random.y, minPatrolHeight, maxPatrolHeight);
        patrolTarget = transform.position + random;
    }

    // ---------------- CHASE ----------------
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position + new Vector3(0, 1.5f, 0)).normalized;

        direction += ObstacleAvoidanceVector() * avoidWeight;
        MoveDrone(direction, chaseSpeed);

        // 🔥 Direct LookAt Player
        transform.LookAt(player);
    }

    // ---------------- ATTACK ----------------
    void AttackPlayer()
    {
        // Rotation handled entirely by LookAt
        transform.LookAt(player);

        float distance = Vector3.Distance(transform.position, player.position);

        // keep small hover motion
        if (distance > attackRange * 0.7f)
            MoveDrone((player.position - transform.position).normalized, chaseSpeed);
        else
            MoveDrone(-transform.forward, 2f); // step back, keep distance

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    // ---------------- SHOOTING ----------------
    void Shoot()
    {
        if (!firePoint || !projectilePrefab) return;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    // ---------------- MOVEMENT ----------------
    void MoveDrone(Vector3 dir, float speed)
    {
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, dir * speed, Time.deltaTime * 3f);
    }

    // ---------------- AVOIDANCE ----------------
    Vector3 ObstacleAvoidanceVector()
    {
        Vector3 avoid = Vector3.zero;
        Vector3 origin = transform.position;

        Vector3[] checks =
        {
            transform.forward,
            (transform.forward + transform.right).normalized,
            (transform.forward - transform.right).normalized,
            (transform.forward + transform.up).normalized,
            (transform.forward - transform.up).normalized
        };

        foreach (var d in checks)
        {
            if (Physics.Raycast(origin, d, out RaycastHit hit, obstacleAvoidanceDistance, obstacleMask))
                avoid += hit.normal * (1f - hit.distance / obstacleAvoidanceDistance);
        }
        return avoid.normalized;
    }

    // ---------------- GIZMOS ----------------
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan; Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.magenta; Gizmos.DrawSphere(patrolTarget, 0.3f);
    }
}