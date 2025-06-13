using UnityEngine;

public class SimpleBossShooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject proyectil; // Arrastra aquí tu prefab de proyectil
    public Transform puntoDisparo; // Punto desde donde sale el proyectil
    public Transform jugador; // Arrastra aquí tu jugador
    
    [Header("Configuración")]
    public float velocidadProyectil = 10f;
    public float tiempoEntreDisparos = 2f;
    
    private float proximoDisparo = 0f;
    
    void Start()
    {
        // Si no tienes el jugador asignado, lo busca automáticamente
        if (jugador == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                jugador = player.transform;
        }
    }
    
    void Update()
    {
        // Solo dispara si tenemos jugador y proyectil
        if (jugador != null && proyectil != null)
        {
            // Revisa si es tiempo de disparar
            if (Time.time >= proximoDisparo)
            {
                Disparar();
                proximoDisparo = Time.time + tiempoEntreDisparos;
            }
        }
    }
    
    void Disparar()
    {
        // Usa el punto de disparo o la posición del jefe si no hay punto específico
        Vector3 posicionDisparo = puntoDisparo != null ? puntoDisparo.position : transform.position;
        
        // Calcula la dirección hacia el jugador
        Vector3 direccion = (jugador.position - posicionDisparo).normalized;
        
        // Crea el proyectil
        GameObject nuevoProyectil = Instantiate(proyectil, posicionDisparo, Quaternion.identity);
        
        // Le da velocidad al proyectil
        Rigidbody rb = nuevoProyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadProyectil;
        }
    }
}