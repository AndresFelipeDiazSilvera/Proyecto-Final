using Unity.VisualScripting;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float tiempoVida = 5f; // Segundos antes de desaparecer
    public int daño = 10; // Daño que hace al jugador
    public GameObject projectile;
    
    private HealtSystem healtSystem;
    
    private SimpleBossShooter shooter;
   
    void Start()
    {
        // Se destruye automáticamente después del tiempo especificado
        Destroy(projectile, tiempoVida);
        healtSystem = FindFirstObjectByType<HealtSystem>();
        shooter = FindFirstObjectByType<SimpleBossShooter>();
    }

    //void LateUpdate()
    //{
     //   shooter.Disparar();
   // } 
    
    void OnTriggerEnter(Collider other)
    {
        // Si toca al jugador
        if (other.CompareTag("Player"))
        {
            // Aquí puedes añadir lógica de daño si tienes sistema de vida
            Debug.Log("¡El proyectil golpeó al jugador!");
            healtSystem.TakeDamage(daño);
            // Destruye el proyectil
            Destroy(projectile);
        }
    }

 
}