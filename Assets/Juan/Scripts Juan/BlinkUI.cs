using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    public float blinkInterval = 0.5f; // medio segundo por defecto
    private Graphic uiElement;

    void Start()
    {
        // Puede ser RawImage, Text, TMP_Text, etc.
        uiElement = GetComponent<Graphic>();

        if (uiElement != null)
            StartCoroutine(Blink());
    }

    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            uiElement.enabled = !uiElement.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}