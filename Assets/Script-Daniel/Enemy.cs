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
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Si hay un objetivo asignado
        if (target != null)
        {
            // Mueve al enemigo hacia el objetivo
            agent.SetDestination(target.transform.position);
            // Calcula la distancia actual al objetivo
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            // Si el enemigo está lo suficientemente cerca del objetivo y no está atacando actualmente
            if (distanceToTarget < agent.stoppingDistance && !isAttacking)
            {
                AttackTarget(); // Inicia el ataque
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
        //Debug.Log("impacto en enemigo. Vida restante: " + pointLife);
    }
    //metodo para que ataque el obgetivo 
    public void AttackTarget()
    {
        Debug.Log("Entro al metodo de atacar");
        if (target != null && playerHealt != null && !isAttacking)
        {
            Debug.Log("esta atacando");
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
            //Debug.Log("el enemigo choco con el player");
             if (!isAttacking)
            {
                //Debug.Log("El enemigo puede atacar al jugador (desde OnCollisionEnter).");
                AttackTarget(); // Llama al método de ataque
            }
        }
    }
    //metodo para manejar el ataque de los enemigos 
    public IEnumerator Attack()
    {
       Debug.Log("EL enemigo esta atacando al jugador (coroutine).");
        playerHealt.TakeDamage(poitDamage); // El jugador toma daño
        yield return new WaitForSeconds(attackDuration); // Espera la duración del ataque
        isAttacking = false; // Después de la espera, el enemigo puede volver a atacar
        Debug.Log("Ataque del enemigo terminado. isAttacking = false.");
    }
    //metodo para manejar la muerte de los enemigos 
    void Die()
    {
        //Debug.Log(gameObject.name + " ha muerto.");
        if (spawnManager != null)
        {
            spawnManager.EnemyDied(gameObject); // Llama a una nueva funcion en el SpawnManager
        }

    }

}
