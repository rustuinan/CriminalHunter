using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips; // 8 adet arka plan müziği
    private AudioSource audioSource;
    private int currentTrackIndex = -1; // Şu anki çalınan şarkının indeksi

    void Start()
    {
        // AudioSource bileşenini al
        audioSource = GetComponent<AudioSource>();

        // Arka plan müziğini başlat
        PlayNextRandomTrack();
    }

    // Rastgele bir sonraki şarkıyı çalma
    void PlayNextRandomTrack()
    {
        if (backgroundMusicClips.Length == 0)
        {
            Debug.LogError("Müzik dosyaları eksik.");
            return;
        }

        // Şarkıların rastgele sırayla çalınması için bir indeks seç
        int newTrackIndex;
        do
        {
            newTrackIndex = Random.Range(0, backgroundMusicClips.Length);
        } while (newTrackIndex == currentTrackIndex); // Aynı şarkıyı ardışık çalmaktan kaçınmak için

        currentTrackIndex = newTrackIndex;

        // Seçilen şarkıyı çal
        audioSource.clip = backgroundMusicClips[currentTrackIndex];
        audioSource.Play();

        // Şarkı süresi kadar bekledikten sonra bir sonraki şarkıyı çal
        StartCoroutine(WaitForTrackToEnd(audioSource.clip.length));
    }

    // Şarkı bitene kadar bekleyip bir sonrakini çal
    private IEnumerator WaitForTrackToEnd(float duration)
    {
        yield return new WaitForSeconds(duration); // Şarkı süresi kadar bekle
        PlayNextRandomTrack(); // Sonraki şarkıyı çal
    }
}
