using System.Collections;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    int soundPlay = 0;
    float delayPlaySound = 60f;
    private AudioManager audioManager;
    private HealtSystem healtSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        healtSystem = FindAnyObjectByType<HealtSystem>();
        StartCoroutine(PlayAmbientSoundCorrutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int RandomSoundAmbiente()
    {
        soundPlay = Random.Range(1, 5);
        return soundPlay;
    }

    IEnumerator PlayAmbientSoundCorrutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayPlaySound);
            RandomSoundAmbiente();
            if (healtSystem.lose == false)
            {
                if (soundPlay == 1)
                {
                    audioManager.SuspensoMusicAmbientePlay();
                    Debug.Log("sonido play");
                }
                else if (soundPlay == 2)
                {
                    audioManager.CrunchPlay();
                    Debug.Log("sonido play");
                }
                else if (soundPlay == 3)
                {
                    audioManager.ForestPlay();
                    Debug.Log("sonido play");
                }
                else if (soundPlay == 4)
                {
                    audioManager.VientoPlay();
                    Debug.Log("sonido play");
                }
            }
            else
            {
                audioManager.StopSound();
            }
        }
    }
}
