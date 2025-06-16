using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlmaBar : MonoBehaviour
{
    [SerializeField] Slider almaBar;
    public int almas;
    private SoulCollectorA soulCollector;
    private GameObject alma;
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
        if (SceneManager.GetActiveScene().name == "EscenaJefe")
        {
            almaBar.value = 2;
            soulCollector.AlmasAlmacenadas.Add(alma);
            soulCollector.AlmasAlmacenadas.Add(alma);
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
