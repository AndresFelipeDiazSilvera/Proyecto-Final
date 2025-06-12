using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour
{
    private SoulCollectorA soulCollector;
    private SpanwManager enemiesWave;
    public GameObject interactMessage;
    public GameObject soulIncomplete;
    public float timeDelayMessage = 3f;


    private void Start()
    {
        soulCollector = FindFirstObjectByType<SoulCollectorA>();
        enemiesWave = FindFirstObjectByType<SpanwManager>();
        //interactMessage.SetActive(false);
    }

    public void ActiveObject()
    {
        if (soulCollector.AlmasAlmacenadas.Count >= 2 && enemiesWave.enemiesPerWave == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            interactMessage.SetActive(false);
        }
        else
        {
            
            StartCoroutine(AlmasInsuficientes());
        }
    }

    IEnumerator AlmasInsuficientes()
    {
        soulIncomplete.SetActive(true);
        yield return new WaitForSeconds(timeDelayMessage);
        soulIncomplete.SetActive(false);
    }
}
