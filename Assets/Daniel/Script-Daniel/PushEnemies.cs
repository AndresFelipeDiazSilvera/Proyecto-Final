using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI; // Necesario si usas NavMeshAgent

public class PushEnemies : MonoBehaviour
{
    [SerializeField] float attackDuration;
    [SerializeField] float pushDistance; // Distancia que el enemigo sera empujado
    [SerializeField] float pushTime;   // Tiempo que tomara el empuje
    [SerializeField] float pushRadius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackCooldown;
    private bool canAttack = true;

    public void StartAttack(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed&&canAttack)
        {
            Debug.Log("Iniciando ataque de empuje para cinematicos.");
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        //TO DO AGREGAR ANIMACION DEL ATAQUE
        yield return new WaitForSeconds(0.1f); // Pequeña demora para la pre-animacion

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius, enemyLayer);

        foreach (var hitCollider in hitColliders)
        {
            NavMeshAgent navMeshAgent = hitCollider.GetComponent<NavMeshAgent>();
            MonoBehaviour enemyMovementScript = hitCollider.GetComponent<Enemy>();

            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
            else if (enemyMovementScript != null)
            {
                enemyMovementScript.enabled = false;
            }

            // Calcular la direccion del empuje
            Vector3 directionToEnemy = (hitCollider.transform.position - transform.position).normalized;
            directionToEnemy.y = 0;
            directionToEnemy.Normalize();

            // Iniciar la corrutina para mover al enemigo cinematicamente
            StartCoroutine(MoveKinematicEnemy(hitCollider.transform, directionToEnemy, pushDistance, pushTime, navMeshAgent, enemyMovementScript));
        }

        yield return new WaitForSeconds(attackDuration);
        Debug.Log("Ataque de empuje finalizado.");
         yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // volver a atacar
        Debug.Log("Cooldown del ataque terminado. Puedes atacar de nuevo.");
    }

    private IEnumerator MoveKinematicEnemy(Transform enemyTransform, Vector3 pushDirection, float distance, float time, NavMeshAgent navAgent, MonoBehaviour movementScript)
    {
        Vector3 startPosition = enemyTransform.position;
        Vector3 targetPosition = startPosition + pushDirection * distance;
        float elapsed = 0f;
        while (elapsed < time)
        {
            float t = elapsed / time;
            // Usamos una curva para suavizar el empuje si quieres
            enemyTransform.position = Vector3.Lerp(startPosition, targetPosition, t); 
            elapsed += Time.deltaTime;
            yield return null; // Esperar un frame
        }

        // Asegura de que el enemigo llegue a la posicion final o lo mas cerca posible
        enemyTransform.position = targetPosition; 
        // Reactiva el movimiento del enemigo
        if (navAgent != null)
        {
            navAgent.enabled = true;
            if (navAgent.isOnNavMesh)
            {
                // Si el enemigo fue movido a una nueva posicion, actualiza el destino del NavMeshAgent
                navAgent.SetDestination(enemyTransform.position);
            }
        }
        else if (movementScript != null)
        {
            movementScript.enabled = true;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
}
