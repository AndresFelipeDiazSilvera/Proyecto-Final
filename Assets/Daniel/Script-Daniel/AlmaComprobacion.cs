using UnityEngine;

public class AlmaComprobacion : MonoBehaviour
{
    [SerializeField] GameObject alma;
    [SerializeField] GameObject alma2;
    private SpanwManager spanwManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spanwManager = FindAnyObjectByType<SpanwManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //activar alma por oleadas
    public void EnableAlma()
    {
        if (spanwManager.wave==2)
        {
            alma.SetActive(true);
        }else if (spanwManager.wave==3)
        {
            alma2.SetActive(true);
        }
    }
}
