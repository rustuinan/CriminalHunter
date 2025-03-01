using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI characterStoryText;
    public Button arrestButton;
    public Button releaseButton;
    public TextMeshProUGUI resultText;

    public int playerScore = 0;
    public int penalty = 10;

    public Transform characterSpawnPoint;
    public Transform characterStartPoint;
    public Transform characterExitPoint;

    public CharacterStory[] characterStories;
    private CharacterStory currentCharacterStory;
    private GameObject currentCharacterObject;

    public float moveSpeed = 2f;
    public float delayBeforeNewCharacter = 2f;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    private AudioSource audioSource;

    public LevelManager levelManager;

    private List<int> usedIndices = new List<int>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SpawnNewCharacter();
    }

    void SpawnNewCharacter()
    {
        if (currentCharacterObject != null)
        {
            Destroy(currentCharacterObject);
        }

        if (usedIndices.Count == characterStories.Length)
        {
            usedIndices.Clear();
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, characterStories.Length);
        } while (usedIndices.Contains(randomIndex));

        usedIndices.Add(randomIndex);
        currentCharacterStory = characterStories[randomIndex];

        currentCharacterObject = Instantiate(currentCharacterStory.characterPrefab, characterStartPoint.position, Quaternion.identity);
        currentCharacterObject.transform.Rotate(0, 180, 0);

        StartCoroutine(MoveCharacterToPosition(currentCharacterObject, characterSpawnPoint.position));

        characterStoryText.text = currentCharacterStory.characterName + "\n\n" + currentCharacterStory.story;
        resultText.text = "";
    }

    private IEnumerator MoveCharacterToPosition(GameObject character, Vector3 targetPosition)
    {
        while (Vector3.Distance(character.transform.position, targetPosition) > 0.1f)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveCharacterToExit(GameObject character, Vector3 exitPosition)
    {
        while (Vector3.Distance(character.transform.position, exitPosition) > 0.1f)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, exitPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(WaitAndSpawnNewCharacter());
    }

    public void OnArrestButtonClicked()
    {
        if (currentCharacterStory.isCriminal)
        {
            resultText.text = "Doğru! " + currentCharacterStory.characterName + " tutuklandı.";
            playerScore += 10;
            PlaySound(correctSound);
        }
        else
        {
            resultText.text = "Yanlış! " + currentCharacterStory.characterName + " suçsuzdu.";
            playerScore -= penalty;
            PlaySound(wrongSound);
        }

        StartCoroutine(MoveCharacterToExit(currentCharacterObject, characterExitPoint.position));
        levelManager.OnCharacterJudged();
    }

    private IEnumerator WaitAndSpawnNewCharacter()
    {
        yield return new WaitForSeconds(delayBeforeNewCharacter);
        SpawnNewCharacter();
    }

    public void OnReleaseButtonClicked()
    {
        if (!currentCharacterStory.isCriminal)
        {
            resultText.text = "Doğru! " + currentCharacterStory.characterName + " serbest bırakıldı.";
            playerScore += 10;
            PlaySound(correctSound);
        }
        else
        {
            resultText.text = "Yanlış! " + currentCharacterStory.characterName + " suçluydu.";
            playerScore -= penalty;
            PlaySound(wrongSound);
        }

        StartCoroutine(MoveCharacterToExit(currentCharacterObject, characterExitPoint.position));
        levelManager.OnCharacterJudged();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
