using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public string text;
        public float startTime;
        public float duration;
    }

    public SubtitleLine[] subtitles;
    public TextMeshProUGUI subtitleText;
    private float timer;
    private int index = 0;
    private bool isPlaying = false;
    public SubtitleManager subtitleManager;

    void Start()
    {
        subtitleManager.StartSubtitles();
    }

    public void StartSubtitles()
    {
        timer = 0f;
        index = 0;
        subtitleText.text = "";
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying || index >= subtitles.Length) return;

        timer += Time.deltaTime;

        SubtitleLine current = subtitles[index];

        if (timer >= current.startTime)
        {
            subtitleText.text = current.text;
        }

        if (timer >= current.startTime + current.duration)
        {
            subtitleText.text = "";
            index++;
        }
    }
}
