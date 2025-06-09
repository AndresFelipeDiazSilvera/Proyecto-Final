using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HealtSystem : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] Slider lifeBar;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageSound;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject player;
    [SerializeField] GameObject buttonRestart;
    private VideoPlayer video;
    public bool lose=false;

    void Awake()
    {
        video = gameOver.GetComponent<VideoPlayer>();
        //video.Stop();
    }

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
            lose = true;
            gameOver.SetActive(true);
            video.Play();
            Time.timeScale = 0;
            buttonRestart.SetActive(true);
        }
    }
}
