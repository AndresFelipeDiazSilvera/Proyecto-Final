using UnityEngine;

public class HealthBoss : MonoBehaviour
{
    public int live = 750;

    public void TakeDamage(int damage)
    {
        live -= damage;
        Debug.Log("Vida Actual Moloch" + live);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
