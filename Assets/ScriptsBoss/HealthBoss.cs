using UnityEngine;
using UnityEngine.UI;

public class HealthBoss : MonoBehaviour
{
    public int live = 750;
    public int currentHealth;
    public Slider lifeBoss;

    void Start()
    {
        currentHealth = live;
        lifeBoss.maxValue = live;
        lifeBoss.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, live);
        lifeBoss.value = currentHealth;
        Debug.Log("VIDE RESTANTE BOSS"+live);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject); // o animación de muerte
    }
}
