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
    [SerializeField] GameObject lifeBarDisable;
    [SerializeField] GameObject pausaButtonDisable;
    [SerializeField] GameObject almasBarDisable;
    private VideoPlayer video;
    public bool lose = false;

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
    //metodo para resivir daño 
    public void TakeDamage(int damege)
    {
        playerHealth -= damege;
        audioSource.PlayOneShot(damageSound);
        if (lifeBar != null)
        {
            lifeBar.value = playerHealth;
        }
        PlayerDie(playerHealth);
    }
    //metodo para manejar la muerte
    public void PlayerDie(int healt)
    {
        if (healt <= 0)
        {
            lose = true;
            lifeBarDisable.SetActive(false);
            pausaButtonDisable.SetActive(false);
            almasBarDisable.SetActive(false);
            gameOver.SetActive(true);
            video.Play();
            Time.timeScale = 0;
            buttonRestart.SetActive(true);
        }
    }
}
