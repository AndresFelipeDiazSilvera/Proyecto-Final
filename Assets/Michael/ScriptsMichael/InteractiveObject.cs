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
    public GameObject waveMessage;
    public float timeDelayMessage = 3f;


    private void Start()
    {
        soulCollector = FindFirstObjectByType<SoulCollectorA>();
        enemiesWave = FindFirstObjectByType<SpanwManager>();
        //interactMessage.SetActive(false);
    }

    public void ActiveObject()
    {
        Debug.Log("enemigos per wave"+enemiesWave.enemiesPerWave);
        if (soulCollector.AlmasAlmacenadas.Count >= 2 && enemiesWave.EnemysEnable() == 0)
        {
            Debug.Log("cargar scena");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            interactMessage.SetActive(false);
        }
        else if (enemiesWave.enemiesPerWave > 0 && enemiesWave.EnemysEnable() > 0)
        {
            StartCoroutine(WaveIncomplete());
        }
        else if (soulCollector.AlmasAlmacenadas.Count < 2 && enemiesWave.EnemysEnable() == 0)
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

    IEnumerator WaveIncomplete()
    {
        waveMessage.SetActive(true);
        yield return new WaitForSeconds(timeDelayMessage);
        waveMessage.SetActive(false);
    }
}
