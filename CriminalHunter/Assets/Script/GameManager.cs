using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI characterStoryText;   // Karakter hikayesini gösteren UI elementi
    public Button arrestButton;       // Tutukla butonu
    public Button releaseButton;      // Serbest bırak butonu
    public TextMeshProUGUI resultText;           // Kararın sonucunu göstermek için
    public int playerScore = 0;       // Oyuncu puanı
    public int penalty = 10;          // Yanlış karar için ceza
    public Transform characterSpawnPoint;  // Karakterin sahnede duracağı hedef nokta
    public Transform characterStartPoint;  // Karakterin sağdan başlayacağı nokta

    public CharacterStory[] characterStories;  // Scriptable Object listesi
    private CharacterStory currentCharacterStory;
    private GameObject currentCharacterObject;

    public float moveSpeed = 2f; // Karakterin sağdan gelirkenki hareket hızı

    // Ses efektleri
    public AudioClip correctSound; // Doğru seçim ses efekti
    public AudioClip wrongSound;   // Yanlış seçim ses efekti
    private AudioSource audioSource;  // Ses efektlerini çalmak için AudioSource

    // Oyunu başlatma
    void Start()
    {
        // AudioSource referansını al
        audioSource = GetComponent<AudioSource>();
        SpawnNewCharacter();
    }

    // Yeni karakter oluştur
    void SpawnNewCharacter()
    {
        // Önceki karakteri sil
        if (currentCharacterObject != null)
        {
            Destroy(currentCharacterObject);
        }

        // Rastgele bir karakter hikayesi seç
        int randomIndex = Random.Range(0, characterStories.Length);
        currentCharacterStory = characterStories[randomIndex];

        // Karakterin prefab'ını sağdan başlat ve sahneye ekle
        currentCharacterObject = Instantiate(currentCharacterStory.characterPrefab, characterStartPoint.position, Quaternion.identity);

        // Karakteri 180 derece döndür (y ekseninde)
        currentCharacterObject.transform.Rotate(0, 180, 0);

        // Karakteri hareket ettir
        StartCoroutine(MoveCharacterToPosition(currentCharacterObject, characterSpawnPoint.position));

        // Karakter hikayesini UI'da göster
        characterStoryText.text = currentCharacterStory.characterName + "\n\n" + currentCharacterStory.story;
        resultText.text = ""; // Sonucu sıfırla
    }

    // Karakteri sağdan hedef noktaya hareket ettiren coroutine
    private IEnumerator MoveCharacterToPosition(GameObject character, Vector3 targetPosition)
    {
        while (Vector3.Distance(character.transform.position, targetPosition) > 0.1f)
        {
            // Karakteri hedefe doğru hareket ettir
            character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Tutukla butonuna basıldığında çalışacak metot
    public void OnArrestButtonClicked()
    {
        if (currentCharacterStory.isCriminal)
        {
            resultText.text = "Doğru! " + currentCharacterStory.characterName + " tutuklandı.";
            playerScore += 10; // Doğru karar için puan ekle

            // Doğru ses efektini çal
            PlaySound(correctSound);
        }
        else
        {
            resultText.text = "Yanlış! " + currentCharacterStory.characterName + " suçsuzdu.";
            playerScore -= penalty; // Yanlış karar için ceza ver

            // Yanlış ses efektini çal
            PlaySound(wrongSound);
        }

        // Yeni bir karakter yükle
        SpawnNewCharacter();
    }

    // Serbest bırak butonuna basıldığında çalışacak metot
    public void OnReleaseButtonClicked()
    {
        if (!currentCharacterStory.isCriminal)
        {
            resultText.text = "Doğru! " + currentCharacterStory.characterName + " serbest bırakıldı.";
            playerScore += 10; // Doğru karar için puan ekle

            // Doğru ses efektini çal
            PlaySound(correctSound);
        }
        else
        {
            resultText.text = "Yanlış! " + currentCharacterStory.characterName + " suçluydu.";
            playerScore -= penalty; // Yanlış karar için ceza ver

            // Yanlış ses efektini çal
            PlaySound(wrongSound);
        }

        // Yeni bir karakter yükle
        SpawnNewCharacter();
    }

    // Ses efektini çalma metodu
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
