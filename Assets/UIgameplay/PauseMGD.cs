using UnityEngine;
using UnityEngine.UI;

public class PauseMGD : MonoBehaviour
{
    public Button pauseButton;
    public Image pauseButtonImage;
    public Sprite pauseSprite; // Icono de pausa
    public Sprite playSprite;  // Icono de play

    public Button[] slotButtons;

    public bool isPaused = false;

    public void Reaunadar()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Debug.Log("Juego pausado: " + (Time.timeScale == 0));
        isPaused = true;
    }
    
}