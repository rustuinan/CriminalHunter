using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConstitutionManager : MonoBehaviour
{
    public GameObject constitutionPanel;  // Anayasa paneli (kitap)
    public TextMeshProUGUI constitutionText; // Anayasa maddelerini gösterecek TextMeshPro
    public Button nextPageButton;  // İleri butonu
    public Button prevPageButton;  // Geri butonu
    public Button closeButton;     // Kapat butonu
    public List<string> constitutionArticles; // Anayasa maddelerini tutacak liste

    private int currentPage = 0;   // Mevcut sayfa
    [SerializeField] private int articlesPerPage = 3; // Her sayfada kaç madde göstereceğimiz

    // Oyun başlarken çalışacak
    void Start()
    {
        // İlk başta anayasa paneli kapalı
        constitutionPanel.SetActive(false);

        // İlk sayfa yüklensin
        DisplayPage(currentPage);

        // Butonlara fonksiyonları bağlama
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);
        closeButton.onClick.AddListener(CloseConstitution);
    }

    // Anayasa panelini açma
    public void OpenConstitution()
    {
        constitutionPanel.SetActive(true);
        DisplayPage(currentPage); // Mevcut sayfayı göster
    }

    // Anayasa panelini kapatma
    public void CloseConstitution()
    {
        constitutionPanel.SetActive(false);
    }

    // Mevcut sayfadaki anayasa maddelerini gösterme
    void DisplayPage(int pageIndex)
    {
        // Her sayfada gösterilecek madde grubunu hesapla
        int startArticleIndex = pageIndex * articlesPerPage;
        int endArticleIndex = Mathf.Min(startArticleIndex + articlesPerPage, constitutionArticles.Count);

        // Sayfadaki maddeleri birleştir
        string pageContent = "";
        for (int i = startArticleIndex; i < endArticleIndex; i++)
        {
            pageContent += (i + 1) + ". " + constitutionArticles[i] + "\n\n"; // Madde numarası ve içerik
        }

        // Anayasa metnini sayfa sayfa göster
        constitutionText.text = pageContent;

        // Sayfa geçiş butonlarının durumunu kontrol et
        prevPageButton.gameObject.SetActive(pageIndex > 0); // İlk sayfadaysa geri butonunu gizle
        nextPageButton.gameObject.SetActive(endArticleIndex < constitutionArticles.Count); // Son sayfadaysa ileri butonunu gizle
    }

    // İleri sayfa fonksiyonu
    void NextPage()
    {
        if ((currentPage + 1) * articlesPerPage < constitutionArticles.Count)
        {
            currentPage++;
            DisplayPage(currentPage);
        }
    }

    // Geri sayfa fonksiyonu
    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayPage(currentPage);
        }
    }

    // Anayasaya yeni maddeler ekleme (seviye ilerledikçe kullanılacak)
    public void AddArticle(string newArticle)
    {
        constitutionArticles.Add(newArticle);
    }
}
