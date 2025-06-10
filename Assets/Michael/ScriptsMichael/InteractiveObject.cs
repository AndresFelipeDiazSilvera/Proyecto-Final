using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObject : MonoBehaviour
{
    private SoulCollectorA soulCollector;

    private void Start()
    {
        soulCollector = FindFirstObjectByType<SoulCollectorA>();
    }

    public void ActiveObject()
    {
        if(soulCollector.AlmasAlmacenadas.Count>=2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
