using System;
using UnityEngine;
using UnityEngine.UI;

public class AlmaBar : MonoBehaviour
{
    [SerializeField] Slider almaBar;
    public int almas;
    private SoulCollectorA soulCollector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soulCollector = FindAnyObjectByType<SoulCollectorA>();
        if (soulCollector == null)
        {
            Debug.Log("soulcolletor nulo");
        }
        if (almaBar != null)
        {
            almaBar.maxValue = 3; // Establece el valor maximo de la barra
            almaBar.value = 0;   // Inicializa la barra bacia
        }
    }

    private void Update()
    {

    }

    public void AumentarBarra()
    {
        Debug.Log("almas:" + soulCollector.AlmasAlmacenadas.Count);
        almas = soulCollector.AlmasAlmacenadas.Count;
        almaBar.value = almas;
    }
}
