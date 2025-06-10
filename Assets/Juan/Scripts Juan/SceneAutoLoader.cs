using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    [Tooltip("Tiempo en segundos para cargar la siguiente escena automáticamente")]
    public float waitTime = 60f; // 60s por defecto

    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private System.Collections.IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
