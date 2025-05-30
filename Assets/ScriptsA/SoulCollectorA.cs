using UnityEngine;

public class SoulCollectorA : MonoBehaviour
{
    [Header("Configuración de Recolección")]
    public float rangoRecoleccion = 2f;
    
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
        
        // Solo destruir si encontró un alma dentro del rango
        if (almaMasCercana != null)
        {
            Destroy(almaMasCercana);
        }
    }
    
    // Visualizar el rango de recolección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoRecoleccion);
    }
}