using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //audios
    [SerializeField] AudioClip enemyAttackSound;
    [SerializeField] AudioClip enemyDeadSound;
    [SerializeField] AudioClip almaSound;
    //ambiente
    [SerializeField] AudioClip suspensoAmbiente;
    [SerializeField] AudioClip crunchAmbiente;
    [SerializeField] AudioClip suspensoMusicAmbiente;
    [SerializeField] AudioClip forestAmbiente;
    [SerializeField] AudioClip vientoAmbiente;
    //musica
    [SerializeField] AudioClip simplePianoMusic;
    [SerializeField] AudioClip esperanzaMusic;
    [SerializeField] AudioClip energeticaMusic;
    [SerializeField] AudioClip horrorPianoMusic;
    [SerializeField] AudioClip melancolicMusic;
    [SerializeField] AudioClip suspensoMusic;
    [SerializeField] AudioClip detectiveMusic;
    //audio sorce
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceAmbiente;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    private void Start()
    {
        string nombreEscena = SceneManager.GetActiveScene().name;
        if (nombreEscena == "UImenu")
        {
            MenuPlay();
        }
    }
    //metodos que reproducen audio de enemigo
    public void AttackPlaySound()
    {
        audioSource.PlayOneShot(enemyAttackSound);
    }
    public void DeadPlaySound()
    {
        audioSource.PlayOneShot(enemyDeadSound);
    }
    //metodo que reproducen audio de alma
    public void AlmaPlay()
    {
        audioSource.PlayOneShot(almaSound);
    }
    //metodos que reproducen audio de ambiente
    public void CrunchPlay()
    {
        audioSourceAmbiente.PlayOneShot(crunchAmbiente);
    }
    public void SuspensoMusicAmbientePlay()
    {
        audioSourceAmbiente.PlayOneShot(suspensoMusicAmbiente);
    }
    public void ForestPlay()
    {
        audioSourceAmbiente.PlayOneShot(forestAmbiente);
    }
    public void VientoPlay()
    {
        audioSourceAmbiente.PlayOneShot(vientoAmbiente);
    }

    //metodos que reproducen Musica
    public void MenuPlay()
    {
        audioSource.clip = horrorPianoMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    //metodo para no reproducir 
    public void StopSound()
    {
        audioSource.Stop();
    }
}

