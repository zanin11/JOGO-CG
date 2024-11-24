using UnityEngine;
using System.Collections.Generic;

public class GerenciadorDePontos : MonoBehaviour
{
    public List<PontoEntrega> pontosEntrega = new List<PontoEntrega>();

    public List<PontoEntrega> pontosAtivos = new List<PontoEntrega>();
    public GerenciadorDeEntregas scriptGerenciadorEntrega;
    public float tempoParaProximoPonto = 10f; // Tempo em segundos

    public float tempoMaximoPontoAtivo = 30f; // Tempo em segundos
    private float tempoDesdeUltimoPonto;
    public Animation animationComponent;  // Referência ao componente Animation
    public GameObject animatedObject;    // Referência ao GameObject que contém a animação
    public string animationName;
    //public string animationName = "ShowNovoPedidoAnimation"; // Nome da animação a ser executada
    //public string animationName2 = "hiddenNovoPedidoAnimation";
    void Start()
    {
        // Adicione todos os pontos de entrega à lista
        pontosEntrega.AddRange(FindObjectsOfType<PontoEntrega>());

        // Desativa todos os pontos
        foreach (PontoEntrega ponto in pontosEntrega)
        {
            ponto.DesativarPonto();
        }

        // Ativa um ponto aleatório no início
        int indiceAleatorio = Random.Range(0, pontosEntrega.Count);
        pontosEntrega[indiceAleatorio].AtivarPonto();
        pontosAtivos.Add(pontosEntrega[indiceAleatorio]);
        PlayAnimation("ShowNovoPedidoAnimation");
    }

    public void Update()
    {
        tempoDesdeUltimoPonto += Time.deltaTime;
        GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
        scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
        //Debug.Log("Total de entregas: " + scriptGerenciadorEntrega.totalDeEntregas);

        if (tempoDesdeUltimoPonto >= tempoParaProximoPonto && scriptGerenciadorEntrega.totalDeEntregas < 5)
        {
            // Ativa um novo ponto aleatório
            int indiceAleatorio;
            int cont = 0;
            do{
                cont++;
                PlayAnimation("ShowNovoPedidoAnimation");
                indiceAleatorio = Random.Range(0, pontosEntrega.Count);
            }while(pontosEntrega[indiceAleatorio].estaAtivo == true && cont < 8);

            pontosEntrega[indiceAleatorio].AtivarPonto();
            AtualizaPontosAtivos();
            tempoDesdeUltimoPonto = 0;
        }

        // Verifica o tempo de cada ponto ativo e desativa se necessário
        foreach (PontoEntrega ponto in pontosEntrega)
        {
            if (ponto.estaAtivo)
            {
                ponto.tempoDesdeAtivacao += Time.deltaTime;
                if (ponto.tempoDesdeAtivacao >= tempoMaximoPontoAtivo)
                {
                    ponto.DesativarPonto();
                    AtualizaPontosAtivos();
                }
            }
        }
    }
    public void PlayAnimation(string animationName)
    {
        if (animatedObject != null && animationComponent != null)
        {
            animatedObject.SetActive(true); // Torna o GameObject ativo
            animationComponent.Play(animationName); // Reproduz a animação
            Debug.Log("Animação Iniciada!");

            // Chama a corrotina para desativar o objeto após a animação
            StartCoroutine(DisableAfterAnimation());
        }
    }


    
   private System.Collections.IEnumerator DisableAfterAnimation()
    {
        // Aguarda a duração da animação (utilizando a duração do clip)
        yield return new WaitForSeconds(animationComponent[animationName].length);

        if (animatedObject != null)
        {
            animatedObject.SetActive(false); // Desativa o GameObject após a animação
            Debug.Log("Fim da Animacao!");
        }
    }

    public void AtualizaPontosAtivos(){
        foreach (PontoEntrega ponto in pontosEntrega)
        {
            if (ponto.estaAtivo && !pontosAtivos.Contains(ponto))
            {
                pontosAtivos.Add(ponto);
            }
            else if (!ponto.estaAtivo && pontosAtivos.Contains(ponto))
            {
                pontosAtivos.Remove(ponto);
            }
        }
        Debug.Log("Lista de pontos ativos: ");
        foreach (PontoEntrega ponto in pontosAtivos)
        {
            Debug.Log(ponto.gameObject.name);
        }
        Debug.Log("Apontando para ponto: " + pontosAtivos[0].gameObject.name);
    }
}