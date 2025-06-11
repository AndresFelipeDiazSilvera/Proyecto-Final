using System.Collections.Generic;
using UnityEngine;

public class SoulCollectorA : MonoBehaviour
{
    [Header("Configuración de Recolección")]
    public float rangoRecoleccion = 2f;
    public bool alma = false;
    public AlmaSpawnPoint almaSpawnPointConsumida;
    public List<GameObject> AlmasAlmacenadas = new List<GameObject>();
    private SpanwManager spawnManager;
    private AudioManager audioManager;
    private AlmaBar almaBar;

    void Start()
    {
        spawnManager = FindFirstObjectByType<SpanwManager>(); // Obtener la referencia al SpawnManager
        audioManager = FindAnyObjectByType<AudioManager>(); // obtener la referencia al AudioManager
        almaBar = FindAnyObjectByType<AlmaBar>();
        if (spawnManager == null)
        {
            Debug.LogError("No se encontró un objeto de tipo SpanwManager en la escena.");
        }
    }
    void Update()
    {
        // Solo buscar y recoger cuando se presiona E
        if (Input.GetKeyDown(KeyCode.E))
        {
            RecogerAlmaCercana();
        }
    }

    void RecogerAlmaCercana()
    {
        // Buscar objetos cercanos
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, rangoRecoleccion);

        GameObject almaMasCercana = null;
        float distanciaMinima = rangoRecoleccion + 1f; // Iniciar con distancia mayor al rango

        // Encontrar el alma más cercana dentro del rango
        foreach (Collider col in objetosCercanos)
        {
            if (col.CompareTag("Soul"))
            {
                float distancia = Vector3.Distance(transform.position, col.transform.position);

                // Solo considerar si está dentro del rango y es la más cercana
                if (distancia <= rangoRecoleccion && distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    almaMasCercana = col.gameObject;
                }
            }
        }

        // Solo destruir si encontro un alma dentro del rango
        if (almaMasCercana != null)
        {
            AlmaSpawnPoint Alma = almaMasCercana.GetComponent<AlmaSpawnPoint>();//el alma que alamacena los puntos
            if (Alma != null)
            {
                // Desvincular los puntos de spawn antes de desactivar el alma
                foreach (Transform spawnPoint in Alma.spawnPoints)
                {
                    spawnPoint.SetParent(null); // Los desvincula del alma
                }
                almaSpawnPointConsumida = Alma;
            }
            if (spawnManager != null)
            {
                spawnManager.SetCurrentAlmaSpawnPoint(Alma);
            }
            audioManager.AlmaPlay();
            almaMasCercana.SetActive(false);
            AlmasAlmacenadas.Add(almaMasCercana);
            alma = true;
        }

    }

    // Visualizar el rango de recolección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoRecoleccion);
    }
}