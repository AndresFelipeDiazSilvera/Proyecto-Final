using UnityEngine;

public class AudioManager : MonoBehaviour
{
     //audios
    [SerializeField] AudioClip enemyAttackSound;
    [SerializeField] AudioClip enemyDeadSound;
    //audio sorce
    [SerializeField] AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    //metodos que reproducen audio
    public void AttackPlaySound()
    {
        audioSource.PlayOneShot(enemyAttackSound);
    }
    public void DeadPlaySound()
    {
        audioSource.PlayOneShot(enemyDeadSound);
    }

}

