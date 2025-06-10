using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour
{
    private SoulCollectorA soulCollector;
    public GameObject soulMessage;

    private void Start()
    {
        soulCollector = FindFirstObjectByType<SoulCollectorA>();
        soulMessage.SetActive(false);
    }

    public void ActiveObject()
    {
        if(soulCollector.AlmasAlmacenadas.Count>=2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            soulMessage.SetActive(false);
        }
        else
        {
            soulMessage.SetActive(true);
        }
        
    }
}
