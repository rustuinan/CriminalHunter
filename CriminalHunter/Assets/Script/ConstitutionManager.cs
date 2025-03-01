using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstitutionManager : MonoBehaviour
{
    public List<ConstitutionArticle> allArticles;
    public List<string> unlockedArticles = new List<string>();

    public TextMeshProUGUI constitutionText;
    public GameObject constitutionPanel;

    public int articlesPerPage = 3;
    private int currentPage = 0;

    public GameObject nextPageButton;
    public GameObject previousPageButton;

    void Start()
    {
        UpdateConstitutionUI();
    }

    public void UnlockArticlesForLevel(int level)
    {
        foreach (var article in allArticles)
        {
            if (article.unlockLevel == level && !unlockedArticles.Contains(article.articleText))
            {
                unlockedArticles.Add(article.articleText);
            }
        }

        UpdateConstitutionUI();
    }

    private void UpdateConstitutionUI()
    {
        if (constitutionText == null) return;

        int startIndex = currentPage * articlesPerPage;
        int endIndex = Mathf.Min(startIndex + articlesPerPage, unlockedArticles.Count);

        constitutionText.text = "Anayasa:\n\n\n";
        for (int i = startIndex; i < endIndex; i++)
        {
            constitutionText.text += unlockedArticles[i] + "\n\n";
        }

        previousPageButton.SetActive(currentPage > 0);
        nextPageButton.SetActive(endIndex < unlockedArticles.Count);
    }

    public void OpenConstitution()
    {
        if (constitutionPanel != null)
        {
            constitutionPanel.SetActive(true);
        }
    }

    public void CloseConstitution()
    {
        if (constitutionPanel != null)
        {
            constitutionPanel.SetActive(false);
        }
    }

    public void NextPage()
    {
        if ((currentPage + 1) * articlesPerPage < unlockedArticles.Count)
        {
            currentPage++;
            UpdateConstitutionUI();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateConstitutionUI();
        }
    }
}
