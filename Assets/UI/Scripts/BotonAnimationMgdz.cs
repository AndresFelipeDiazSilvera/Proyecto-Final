using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAnimationMgdz : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public GameObject borde;

    private void OnMouseEnter()
    {
        if (borde != null)
            borde.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (borde != null)
            borde.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Aquí puedes poner lo que pasa al hacer clic
        Debug.Log("Botón clickeado");
    }
}
