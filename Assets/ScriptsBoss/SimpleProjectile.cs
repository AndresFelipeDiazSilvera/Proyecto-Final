using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float tiempoVida = 5f; // Segundos antes de desaparecer
    public float daño = 10f; // Daño que hace al jugador
    
    void Start()
    {
        // Se destruye automáticamente después del tiempo especificado
        Destroy(gameObject, tiempoVida);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Si toca al jugador
        if (other.CompareTag("Player"))
        {
            // Aquí puedes añadir lógica de daño si tienes sistema de vida
            Debug.Log("¡El proyectil golpeó al jugador!");
            
            // Destruye el proyectil
            Destroy(gameObject);
        }
    }
}