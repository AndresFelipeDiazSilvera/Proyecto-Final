using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    [Tooltip("Tiempo en segundos para cargar la siguiente escena automáticamente")]
    public float waitTime = 60f; // 60s por defecto

    private bool sceneLoaded = false;

    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    void Update()
    {
        if (!sceneLoaded && Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene();
        }
    }

    private System.Collections.IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        if (!sceneLoaded)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        sceneLoaded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}