using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int charactersToJudgePerLevel = 5;
    private int charactersJudgedInLevel = 0;
    public float levelTimeLimit = 60f;
    private float levelTimer;

    public TextMeshProUGUI levelText;
    public ConstitutionManager constitutionManager;
    void Start()
    {
        levelTimer = levelTimeLimit;
        UpdateLevelText();
    }

    void Update()
    {
        levelTimer -= Time.deltaTime;

        if (levelTimer <= 0 || charactersJudgedInLevel >= charactersToJudgePerLevel)
        {
            AdvanceToNextLevel();
        }
    }

    public void OnCharacterJudged()
    {
        charactersJudgedInLevel++;

        if (charactersJudgedInLevel >= charactersToJudgePerLevel)
        {
            AdvanceToNextLevel();
        }
    }

    private void AdvanceToNextLevel()
    {
        currentLevel++;
        charactersJudgedInLevel = 0;
        levelTimer = levelTimeLimit;
        UpdateLevelText();

        constitutionManager.UnlockArticlesForLevel(currentLevel);

    }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Seviye: " + currentLevel;
        }
    }
}
