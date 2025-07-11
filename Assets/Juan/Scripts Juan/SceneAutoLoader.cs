using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneAutoLoader : MonoBehaviour
{
    [Tooltip("Tiempo en segundos para cargar la siguiente escena automáticamente")]
    public float waitTime = 60f;

    [Header("Pantalla de carga")]
    public GameObject loadingScreen; // Asigna tu canvas de pantalla de carga en el Inspector
    public UnityEngine.UI.Image loadingImage; // Imagen donde mostrarás los frames
    public Sprite[] loadingFrames; // Tus sprites para la animación de carga

    [Tooltip("Duración de cada frame en segundos (ej: 0.1)")]
    public float frameDuration = 0.1f;
    [Tooltip("Duración total de la pantalla de carga en segundos (ej: 5)")]
    public float loadingDuration = 5f;

    [Header("Timeline (opcional)")]
    public PlayableDirector timeline; // Asigna el PlayableDirector de tu Timeline en el Inspector

    private bool sceneLoaded = false;

    void Start()
    {
         //ponesmos el cursor visible y desbloqueado al cargar la escena
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    void Update()
    {
        if (!sceneLoaded && Input.GetKeyDown(KeyCode.Escape))
        {
            if (timeline != null)
                timeline.Pause();
            StartCoroutine(ShowLoadingAndLoadScene());
        }
    }

    private System.Collections.IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (!sceneLoaded)
        {
            if (timeline != null)
                timeline.Pause();
            StartCoroutine(ShowLoadingAndLoadScene());
        }
    }

    private System.Collections.IEnumerator ShowLoadingAndLoadScene()
    {
        sceneLoaded = true;

        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Loop los frames durante loadingDuration
        float elapsed = 0f;
        int frameCount = loadingFrames.Length;
        int frameIndex = 0;

        while (elapsed < loadingDuration)
        {
            if (loadingImage != null && loadingFrames.Length > 0)
                loadingImage.sprite = loadingFrames[frameIndex % frameCount];

            yield return new WaitForSeconds(frameDuration);

            elapsed += frameDuration;
            frameIndex++;
        }

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (currentIndex >= sceneCount - 1)
        {
            SceneManager.LoadScene(0); // Menú principal
        }
        else
        {
            SceneManager.LoadScene(currentIndex + 1); // Siguiente escena
        }
    }
}