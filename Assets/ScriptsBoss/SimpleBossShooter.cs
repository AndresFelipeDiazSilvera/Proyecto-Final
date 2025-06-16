using UnityEngine;

public class SimpleBossShooter : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject proyectil;
    public Transform puntoDisparo;
    public Transform jugador;
    public Animator anim; // ← Asegúrate de arrastrar el Animator
    public AudioManager audioManager;

    [Header("Configuración")]
    public float velocidadProyectil = 50f;
    public float tiempoEntreDisparos = 2f;

    private float proximoDisparo = 0f;
    private bool puedeDisparar = false;
    private HealthBoss healthBoss;

    void Start()
    {
        healthBoss = GetComponent<HealthBoss>();
        if (jugador == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                jugador = player.transform;
        }

        if (anim != null)
        {
            anim.SetBool("Aullando", true); // ← Activa animación de aullido
            Invoke(nameof(ActivarDisparo), 2f); // Espera 2 segundos
            audioManager.StopSound();
            Invoke(nameof(ReproducirGruñido), 0.2f);
        }
        else
        {
            puedeDisparar = true;
        }
    }
    void ReproducirGruñido()
    {
        audioManager.Gruñido();
    }
    void ActivarDisparo()
    {
        puedeDisparar = true;
        if (anim != null)
        {
            anim.SetBool("Aullando", false); // ← Termina aullido
            audioManager.StopSound();
            audioManager.Amenza2();
        }
    }

    void Update()
    {
        if (!puedeDisparar || (healthBoss != null && healthBoss.estaMuerto)) return;
        // Hacer que el boss mire al jugador
        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0; // Mantiene la rotación solo en el eje Y (horizontal)
        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), Time.deltaTime * 5f);

        if (jugador != null && proyectil != null && Time.time >= proximoDisparo)
        {
            Disparar();
            proximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    public void Disparar()
    {
        if (anim != null)
        {
            anim.SetBool("Disparando", true); // ← Activar disparo
        }

        Vector3 posicionDisparo = puntoDisparo != null ? puntoDisparo.position : transform.position;
        Vector3 direccion = (jugador.position - posicionDisparo).normalized;

        GameObject nuevoProyectil = Instantiate(proyectil, posicionDisparo, Quaternion.identity);
        Rigidbody rb = nuevoProyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadProyectil;
        }

        // Detener animación de disparo después de un corto tiempo
        Invoke(nameof(DetenerAnimacionDisparo), 0.5f);
    }

    void DetenerAnimacionDisparo()
    {
        if (anim != null)
        {
            anim.SetBool("Disparando", false);
        }
    }
}