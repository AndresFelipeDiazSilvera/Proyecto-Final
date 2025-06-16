using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFinal : MonoBehaviour
{
    private AlmaBar almaBar;

    private void Start()
    {
        almaBar = FindFirstObjectByType<AlmaBar>();
    }
    public void LoadScene()
    {
        if (almaBar.almas == 3)
        {
            StartCoroutine(Delay());
            SceneManager.LoadScene("CinematicaGanar");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
    }
}
