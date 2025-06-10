using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    public bool blink = false;
    public float timeDelay;

    // Update is called once per frame
    void Update()
    {
        if (blink==false)
        {
            StartCoroutine(BlinkingLight());
        }
    }

    IEnumerator BlinkingLight()
    {
        blink = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f,1f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.01f, 1f);
        yield return new WaitForSeconds(timeDelay);
        blink = false;
    }
}
