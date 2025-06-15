using UnityEngine;
using UnityEngine.SceneManagement;

public class SoulSceneLoaderOnPickup : MonoBehaviour
{
    public float rangoRecoleccion = 2f;
    private Transform jugador;
    private bool puedeRecoger = false;

    void OnEnable()
    {
        puedeRecoger = true;
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        Debug.Log("Alma ACTIVADA: puedeRecoger=" + puedeRecoger);
    }

    void Update()
    {
        if (!puedeRecoger || jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= rangoRecoleccion)
        {
            Debug.Log("Jugador en rango para recoger el alma");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Presionaste E, cargando siguiente escena...");
                puedeRecoger = false;

                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextScene < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextScene);
                else
                    Debug.LogWarning("No hay más escenas para cargar.");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangoRecoleccion);
    }
}