using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int pointLife = 100;
    [SerializeField] int poitDamage = 10;
    [SerializeField] float attackDuration = 2f;
    private GameObject target;
    private NavMeshAgent agent;
    private HealtSystem playerHealt;
    private bool isAttacking = false;
    private SpanwManager spawnManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealt = FindFirstObjectByType<HealtSystem>();
        spawnManager = FindFirstObjectByType<SpanwManager>(); // Asigna la referencia
        if (spawnManager == null)
        {
            Debug.LogError("No se encontró el SpawnManager en la escena.");
        }
    }
    void OnEnable()
    {
        pointLife = 100;
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < agent.stoppingDistance && !isAttacking)
            {
                AttackTarget();
            }
        }

        if (pointLife <= 0)
        {
            Die();
        }
    }
    //metodo para asignarle un objetivo
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    //metodo para que resiva daño
    public void TakeDamage(int damage)
    {
        pointLife -= damage;
        Debug.Log("impacto");
    }
    //metodo para que ataque el obgetivo 
    public void AttackTarget()
    {
        if (target != null && playerHealt != null && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }
    //metodo para manegar las coliciones 
    void OnCollisionEnter(Collision collision)
    {
        //si coliciona con el palyer ataque
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isAttacking)
            {
                AttackTarget();
            }
        }
    }
    //metodo para manejar el ataque de los enemigos 
    public IEnumerator Attack()
    {
        playerHealt.TakeDamage(poitDamage);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }
    //metodo para manejar la muerte de los enemigos 
    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        if (spawnManager != null)
        {
            spawnManager.EnemyDied(gameObject); // Llama a una nueva funcion en el SpawnManager
        }

    }

}
