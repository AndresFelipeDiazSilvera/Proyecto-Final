using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distance = 1f;
    public GameObject soulDetected;
    public GameObject interactDetected;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        soulDetected.SetActive(false);
        interactDetected.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
        {

            if (hit.collider.tag == "Soul")
            {
                soulDetected.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    soulDetected.SetActive(false);
                }
            }

        }
        else
        {
            soulDetected.SetActive(false);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
        {

            if (hit.collider.tag == "Interactive")
            {
                interactDetected.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<InteractiveObject>().ActiveObject();
                    interactDetected.SetActive(false);
                }
            }

        }
        else
        {
            interactDetected.SetActive(false);
        }
    } 
}
