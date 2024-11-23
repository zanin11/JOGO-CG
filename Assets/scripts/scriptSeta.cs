using UnityEngine;

public class DirecaoSeta : MonoBehaviour
{
    public Transform jogador; // Referência à posição e rotação do jogador (moto)
    public GerenciadorDePontos gerenciadorDePontos; // Referência ao Gerenciador de Pontos

    private Transform pontoDeEntregaAtual; // Ponto de entrega atual

    void Update()
    {
        // Verifica se existem pontos ativos no Gerenciador
        if (gerenciadorDePontos.pontosAtivos.Count > 0)
        {
            // Obtém o primeiro ponto ativo
            pontoDeEntregaAtual = gerenciadorDePontos.pontosAtivos[0].transform;

            // Calcula a direção do jogador ao ponto de entrega
            Vector3 direcao = pontoDeEntregaAtual.position - jogador.position;

            // Calcula o ângulo absoluto entre o jogador e o ponto de entrega
            float anguloParaPonto = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;

            // Obtém o ângulo atual da rotação do jogador
            float anguloJogador = jogador.eulerAngles.y;

            // Ajusta a seta para considerar o ângulo relativo ao jogador
            float anguloRelativo = anguloParaPonto - anguloJogador;

            // Aplica a rotação da seta no eixo Z
            transform.rotation = Quaternion.Euler(0, 0, -anguloRelativo);
        }
        else
        {
            // Caso não haja pontos ativos, pode desativar a seta ou manter sua posição
            pontoDeEntregaAtual = null;
            Debug.Log("Nenhum ponto de entrega ativo!");
        }
    }
}
