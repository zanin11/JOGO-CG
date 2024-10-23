using UnityEngine;
using System.Collections.Generic;

public class GerenciadorDePontos : MonoBehaviour
{
    public List<PontoEntrega> pontosEntrega = new List<PontoEntrega>();

    public GerenciadorDeEntregas scriptGerenciadorEntrega;
    public float tempoParaProximoPonto = 10f; // Tempo em segundos

    public float tempoMaximoPontoAtivo = 30f; // Tempo em segundos
    private float tempoDesdeUltimoPonto;

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
        Debug.Log("Ponto inicial: " + pontosEntrega[indiceAleatorio].name);
    }

    public void Update()
    {
        tempoDesdeUltimoPonto += Time.deltaTime;
        GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
        scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
        Debug.Log("Total de entregas: " + scriptGerenciadorEntrega.totalDeEntregas);

        if (tempoDesdeUltimoPonto >= tempoParaProximoPonto && scriptGerenciadorEntrega.totalDeEntregas < 5)
        {
            // Ativa um novo ponto aleatório
            int indiceAleatorio;
            int cont = 0;
            do{
                cont++;
                indiceAleatorio = Random.Range(0, pontosEntrega.Count);
            }while(pontosEntrega[indiceAleatorio].estaAtivo == true && cont < 8);

            Debug.Log("Ponto ativo: " + pontosEntrega[indiceAleatorio].name);
            pontosEntrega[indiceAleatorio].AtivarPonto();

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
                    Debug.Log("Desativando ponto " + ponto.name);
                    ponto.DesativarPonto();
                }
            }
        }
    }
}