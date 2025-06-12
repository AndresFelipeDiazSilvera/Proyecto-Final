using UnityEngine;

public class ButtonSalir : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Exit()
    {
        Application.Quit();
        Debug.Log("salio del juego");

    }
}
