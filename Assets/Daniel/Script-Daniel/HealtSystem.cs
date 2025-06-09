using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HealtSystem : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] Slider lifeBar;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageSound;
    [SerializeField] GameObject GameOver;

    void Start()
    {
        // Inicializar el Slider
        if (lifeBar != null)
        {
            lifeBar.maxValue = playerHealth; // Establece el valor maximo de la barra
            lifeBar.value = playerHealth;   // Inicializa la barra con la vida completa
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damege)
    {
        Debug.Log("EL JUGADOR TOMO DAÑO");
        playerHealth -= damege;
        audioSource.PlayOneShot(damageSound);
        if (lifeBar != null)
        {
            lifeBar.value = playerHealth;
            Debug.Log("Vida actual del jugador: " + playerHealth);
            Debug.Log("Valor del Slider: " + lifeBar.value);
        }
        if (playerHealth <= 0)
        {
            
        }
    }
}
