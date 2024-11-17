using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AtualizaDinheiro : MonoBehaviour
{
    public Text TxtDinheiro;  // Referência ao Text na UI para exibir o dinheiro
    public int dinheiro;
    public int valorVitoria = 1200;
    public int valorDerrota = -1000;
    [SerializeField] private GameObject telaVitoria;        // Referência para a tela de vitória
    [SerializeField] private GameObject telaDerrota;        // Referência para a tela de derrota
    public void Start()
    {
        // Busca os GameObjects mesmo se estiverem inativos
    GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

    foreach (GameObject obj in allObjects)
    {
        if (obj.CompareTag("TelaVitoria"))
        {
            telaVitoria = obj;
        }
        else if (obj.CompareTag("TelaDerrota"))
        {
            telaDerrota = obj;
        }
        else if(obj.CompareTag("TxtDinheiro"))
        {
            TxtDinheiro = obj.GetComponent<Text>();
        }
    }

    if (telaVitoria == null)
        Debug.LogError("TelaVitoria não encontrada. Verifique se a tag está configurada corretamente.");
    if (telaDerrota == null)
        Debug.LogError("TelaDerrota não encontrada. Verifique se a tag está configurada corretamente.");
        AtualizarTexto(" 1000");
    }
    public void AtualizarTexto(string novo_valor)
    {
        //dinheiro = int.Parse(novo_valor.Replace("R$", "").Trim());
        TxtDinheiro.text = novo_valor;
        Debug.Log("Texto de dinheiro atualizado: " + TxtDinheiro.text); 
        VerificaEstadoDoJogo(novo_valor);
    }

    public void VerificaEstadoDoJogo(string novo_valor){
        //quantia de dinheiro atual
        dinheiro = int.Parse(novo_valor.Replace("R$", "").Trim());
        if(dinheiro >= valorVitoria){
            Debug.Log("Você venceu!");
            TerminarJogo(true);
        }
        else if(dinheiro <= valorDerrota){
            Debug.Log("Você perdeu!");
            TerminarJogo(false);
        }
    }
    // Finaliza o jogo e exibe a tela correspondente
    private void TerminarJogo(bool venceu)
    {
        Time.timeScale = 0f; // Pausa o jogo
        if (venceu)
        {
            telaVitoria.SetActive(true);
        }
        else
        {
            telaDerrota.SetActive(true);
        }
    }

    // Reinicia o jogo
    public void ReiniciarJogo()
    {
        Time.timeScale = 1f; // Restaura o tempo do jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sai do jogo
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

}
