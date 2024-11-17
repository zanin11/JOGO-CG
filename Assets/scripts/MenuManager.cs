using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeSceneJogo;  
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelInfo;
    [SerializeField] private GameObject telaLoading; // Referência para a tela de loading
    [SerializeField] private UnityEngine.UI.Text textoLoading; // Referência ao texto do "Carregando"

    private string[] estadosCarregando = { "Carregando", "Carregando.", "Carregando..", "Carregando..." };
    private int indiceEstado = 0;

    public void Jogar()
    {
        // Desativa o menu principal e ativa a tela de loading
        painelMenuPrincipal.SetActive(false);
        telaLoading.SetActive(true);

        // Inicia a corrotina para gerenciar o carregamento
        StartCoroutine(ExibirLoading());
    }

    public void AbrirOpcoes()
    {
        painelMenuPrincipal.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenuPrincipal.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void AbrirInfo()
    {
        painelMenuPrincipal.SetActive(false);
        painelInfo.SetActive(true);
    }

    public void FecharInfo()
    {
        painelMenuPrincipal.SetActive(true);
        painelInfo.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

    private IEnumerator ExibirLoading()
    {
        float tempoLoading = 6f; // Duração do carregamento
        float tempoPassado = 0f;

        // Atualiza o texto de loading em loop
        while (tempoPassado < tempoLoading)
        {
            textoLoading.text = estadosCarregando[indiceEstado];
            indiceEstado = (indiceEstado + 1) % estadosCarregando.Length;

            yield return new WaitForSeconds(1f); // Espera 1 segundo
            tempoPassado += 1f;
        }

        // Após o carregamento, muda para a cena do jogo
        SceneManager.LoadScene(nomeSceneJogo);
    }
}
