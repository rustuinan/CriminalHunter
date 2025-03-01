using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips;
    private AudioSource audioSource;
    private int currentTrackIndex = -1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayNextRandomTrack();
    }

    void PlayNextRandomTrack()
    {
        if (backgroundMusicClips.Length == 0)
        {
            Debug.LogError("Müzik dosyaları eksik.");
            return;
        }

        int newTrackIndex;
        do
        {
            newTrackIndex = Random.Range(0, backgroundMusicClips.Length);
        } while (newTrackIndex == currentTrackIndex);

        currentTrackIndex = newTrackIndex;

        audioSource.clip = backgroundMusicClips[currentTrackIndex];
        audioSource.Play();

        StartCoroutine(WaitForTrackToEnd(audioSource.clip.length));
    }

    private IEnumerator WaitForTrackToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayNextRandomTrack();
    }
}
