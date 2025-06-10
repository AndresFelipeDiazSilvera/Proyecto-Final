using System.Collections;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    int soundPlay = 0;//numero random
    float delayPlaySound = 60f;//tiempo de espera etre sonidos
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
    //metodo para calcular un nuemro random
    public int RandomSoundAmbiente()
    {
        soundPlay = Random.Range(1, 5);
        return soundPlay;
    }
    //corrutina para ejecutar los sonido ambiente de acuerdo al numero random
    IEnumerator PlayAmbientSoundCorrutine()
    {
        while (true)//repite la corrutina 
        {
            yield return new WaitForSeconds(delayPlaySound);//espera el tiempo establecido
            RandomSoundAmbiente();//traemos un numero random
            //si el juego aun no ha iniciado o ya perdimos no va a sonar de lo contrario ejecute el sonido de acuerdo al numero random
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
                audioManager.StopSound();//detine el audio
            }
        }
    }
}
