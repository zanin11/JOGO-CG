using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
   [SerializeField] private string nomeSceneJogo;  
   [SerializeField] private GameObject painelMenuPrincipal;
   [SerializeField] private GameObject painelOpcoes;
   
    public void Jogar()
    {
        SceneManager.LoadScene(nomeSceneJogo);
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

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
