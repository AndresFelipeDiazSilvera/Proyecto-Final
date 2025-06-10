using UnityEngine;
using UnityEngine.UI;

public class AlmaBar : MonoBehaviour
{
    [SerializeField] Slider almaBar;
    public int alams;
    private SoulCollectorA soulCollector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soulCollector = FindAnyObjectByType<SoulCollectorA>();
        if (almaBar != null)
        {
            almaBar.maxValue = 2; // Establece el valor maximo de la barra
            almaBar.value = 0;   // Inicializa la barra bacia
        }
    }

    private void Update()
    {
        
    }

    public void AumentarBarra()
    {
        for (int i = 0; i < soulCollector.AlmasAlmacenadas.Count; i++)
        {
            alams = i;
            almaBar.value = alams;
        }
    }
}
