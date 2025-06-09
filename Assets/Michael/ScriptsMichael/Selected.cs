using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1f;
    public GameObject TextDetected;
    
    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        TextDetected.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            
            if (hit.collider.tag == "Interactive")
            {
                TextDetected.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<InteractiveObject>().ActiveObject();
                    TextDetected.SetActive(false);
                }
            }
            
        }
        else
        {
            TextDetected.SetActive(false);
        }
    } 
}
