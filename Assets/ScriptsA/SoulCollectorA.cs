using UnityEngine;

public class SoulCollectorA : MonoBehaviour
{
    [Header("Configuración de Recolección")]
    public float rangoRecoleccion = 2f;
    
    private GameObject almaCercana = null;
    
    void Update()
    {
        // Buscar alma cercana constantemente
        BuscarAlmaCercana();
        
        // Solo permitir recoger si hay un alma cercana y se presiona E
        if (almaCercana != null && Input.GetKeyDown(KeyCode.E))
        {
            RecogerAlma();
        }
    }
    
    void BuscarAlmaCercana()
    {
        // Buscar todas las almas cercanas
        Collider[] soulsEnRango = Physics.OverlapSphere(transform.position, rangoRecoleccion);
        
        almaCercana = null; // Resetear
        float distanciaMinima = rangoRecoleccion;
        
        // Encontrar el alma más cercana
        foreach (Collider col in soulsEnRango)
        {
            if (col.CompareTag("Soul"))
            {
                float distancia = Vector3.Distance(transform.position, col.transform.position);
                if (distancia <= distanciaMinima)
                {
                    distanciaMinima = distancia;
                    almaCercana = col.gameObject;
                }
            }
        }
    }
    
    void RecogerAlma()
    {
        if (almaCercana != null)
        {
            Destroy(almaCercana);
            almaCercana = null;
        }
    }
    
    // Visualizar el rango de recolección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoRecoleccion);
        
        // Mostrar cuál alma está cerca
        if (almaCercana != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, almaCercana.transform.position);
        }
    }
}