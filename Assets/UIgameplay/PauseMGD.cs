using UnityEngine;
using UnityEngine.UI;

public class PauseMGD : MonoBehaviour
{
    public Button pauseButton;
    public Image pauseButtonImage;
    public Sprite pauseSprite; // Icono de pausa
    public Sprite playSprite;  // Icono de play

    public Button[] slotButtons;

    private bool isPaused = false;

    private void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
        UpdateState();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        UpdateState();
    }

    private void UpdateState()
    {
        // Control del tiempo del juego
        Time.timeScale = isPaused ? 0f : 1f;

        // Cambiar imagen del botón
        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite = isPaused ? playSprite : pauseSprite;
        }

        // Activar/desactivar los botones de slot
        foreach (var button in slotButtons)
        {
            button.interactable = !isPaused;
        }

        Debug.Log(isPaused ? "Juego en pausa" : "Juego reanudado");
    }
}