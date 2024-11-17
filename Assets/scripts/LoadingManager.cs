using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen; // Tela de loading
    public Text loadingText;         // Texto do "Carregando"

    private string[] loadingStates = { "Carregando", "Carregando.", "Carregando..", "Carregando..." };
    private int currentStateIndex = 0;

    public void StartGame()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen()
    {
        loadingScreen.SetActive(true); // Ativa a tela de loading
        float loadingTime = 6f;        // Duração do loading
        float elapsedTime = 0f;

        while (elapsedTime < loadingTime)
        {
            loadingText.text = loadingStates[currentStateIndex];
            currentStateIndex = (currentStateIndex + 1) % loadingStates.Length;

            yield return new WaitForSeconds(1f); // Muda o texto a cada 1 segundo
            elapsedTime += 1f;
        }

        // Carrega a próxima cena (substitua "GameScene" pelo nome da sua cena de jogo)
        SceneManager.LoadScene("demo");
    }
}
