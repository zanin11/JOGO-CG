using UnityEngine;
using System.Collections.Generic;

public class GerenciadorDePontos : MonoBehaviour
{
    public List<PontoEntrega> pontosEntrega = new List<PontoEntrega>();
    public float tempoParaProximoPonto = 10f; // Tempo em segundos
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
    }

    public void Update()
    {
        tempoDesdeUltimoPonto += Time.deltaTime;

        if (tempoDesdeUltimoPonto >= tempoParaProximoPonto)
        {
            // Ativa um novo ponto aleatório
            int indiceAleatorio;
            int cont = 0;
            do{
                cont++;
                indiceAleatorio = Random.Range(0, pontosEntrega.Count);
            }while(pontosEntrega[indiceAleatorio].estaAtivo == true && cont < 8);

            Debug.Log("Ponto ativo: " + (indiceAleatorio + 1));
            pontosEntrega[indiceAleatorio].AtivarPonto();

            tempoDesdeUltimoPonto = 0;
        }
    }
}