using UnityEngine;
using UnityEngine.UI;

public class HealthBoss : MonoBehaviour
{
    public int maxLife = 1000;
    public int live = 1000;
    public Slider healthSlider;
    public GameObject healthBarCanvas;
    public Camera playerCamera;

    private Animator anim;
    [HideInInspector]
    public bool estaMuerto = false;
    public GameObject pruebaAlma;

    void Start()
    {
        live = maxLife;
        anim = GetComponent<Animator>(); // ← Obtener Animator

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxLife;
            healthSlider.value = live;
        }
    }

    void Update()
    {
        if (healthBarCanvas != null && playerCamera != null)
        {
            Vector3 direction = (playerCamera.transform.position - healthBarCanvas.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(-direction);
            healthBarCanvas.transform.rotation = Quaternion.Slerp(
                healthBarCanvas.transform.rotation,
                lookRotation,
                Time.deltaTime * 10f
            );
        }
    }

    public void TakeDamage(int damage)
    {
        live -= damage;
        live = Mathf.Clamp(live, 0, maxLife);
        Debug.Log("Vida Actual Moloch: " + live);

        if (healthSlider != null)
        {
            healthSlider.value = live;
        }

        if (live <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Moloch ha muerto");

        estaMuerto = true;

        if (anim != null)
        {
            anim.SetBool("Muerto", true); // ← Activar animación de muerte
        }

        if (pruebaAlma != null)
        {
            pruebaAlma.SetActive(true);
        }
    }
}