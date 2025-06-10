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
    private bool isDeath = false;
    private SpanwManager spawnManager;
    private Animator animator;
    private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealt = FindFirstObjectByType<HealtSystem>();
        spawnManager = FindFirstObjectByType<SpanwManager>(); // Asigna la referencia
        animator = GetComponent<Animator>();
        audioManager = FindFirstObjectByType<AudioManager>();
        if (spawnManager == null)
        {
            Debug.LogError("No se encontró el SpawnManager en la escena.");
        }
    }
    void OnEnable()
    {
        isDeath = false; // Reinicia el estado de muerte al habilitar el objeto
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
        if (target != null && playerHealt != null && !isAttacking && !isDeath)
        {
            Debug.Log("esta atacando");
            audioManager.AttackPlaySound();
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

        // Activar animación
        animator.SetBool("isAttacking", true);
        agent.isStopped = true;

        yield return new WaitForSeconds(0.5f); // esperar parte inicial del salto
        playerHealt.TakeDamage(poitDamage); // hacer daño

        yield return new WaitForSeconds(attackDuration - 0.5f);

        // Terminar ataque
        isAttacking = false;
        agent.isStopped = false;
        animator.SetBool("isAttacking", false);

        Debug.Log("Ataque del enemigo terminado. isAttacking = false.");
    }
    //metodo para manejar la muerte de los enemigos 
    void Die()
    {
        if (!isDeath) // Asegúrate que solo muera una vez
        {
            isDeath = true;
            StartCoroutine(Death());
        }
    }
    public IEnumerator Death()
    {
        Debug.Log("El enemigo está muriendo...");

        // Activar animación de muerte
        animator.SetBool("isDeath",true);  // usa SetTrigger en lugar de SetBool
        agent.isStopped = true;

        // Sonido de muerte
        if (audioManager != null)
        {
            audioManager.DeadPlaySound();
        }

        // Esperar a que la animación termine
        yield return new WaitForSeconds(2.5f);

        // Notificar al spawnManager (si existe)
        if (spawnManager != null)
        {
            spawnManager.EnemyDied(gameObject);
        }
    }

}
