using UnityEngine;
using UnityEngine.UI;

public class HealtSystem : MonoBehaviour
{
    [SerializeField] int playerHealth=100;
    public Slider healthSlider;
    void Start()
    {
        // Inicializar el Slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = playerHealth; // Establece el valor maximo de la barra
            healthSlider.value = playerHealth;    // Inicializa la barra con la vida completa
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damege)
    {
        playerHealth -= damege;
        if (playerHealth==0)
        {
            Debug.Log("player sin vida");
        }
    }
}
