using UnityEngine;
using UnityEngine.UI;

public class SpeedRadarDetector : MonoBehaviour
{
    public float speedLimit = 15f; // Limite de velocidade em 15
    public Transform radarPosition; // Ponto de referência para a posição do radar
    public Text TxtDinheiro;
    private int dinheiro;
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            MotoController motoController = other.GetComponent<MotoController>();

            if (motoController == null) return;

            // A direção entre o jogador e o radar
            Vector3 direcaoRadarParaPlayer = (other.transform.position - radarPosition.position).normalized;

            // Verifica se o jogador está se aproximando da frente do radar
            bool estaDeFrente = Vector3.Dot(radarPosition.forward, direcaoRadarParaPlayer) > 0;

            // Verifica a direção de movimento do jogador. 
            // Usamos a diferença na posição para determinar a direção de movimento.
            Vector3 movimentoJogador = other.transform.position - motoController.lastPosition; // 'lastPosition' é uma variável que deve ser atualizada no MotoController
            bool movendoParaFrente = Vector3.Dot(movimentoJogador.normalized, radarPosition.forward) > 0;
            Debug.Log(movendoParaFrente);

            // Aplica a multa apenas se o jogador estiver na frente, se movendo para o radar e acima da velocidade
            if (estaDeFrente && movendoParaFrente && motoController.currentSpeed > speedLimit)
            {
                Debug.Log("MULTAAAAA!!!! VELOCIDADE ACIMA DE 15 KM/H !!!!!!!!!");
                Debug.Log("Velocidade: " + motoController.currentSpeed);
                // Deduz o valor da multa
                //dinheiro -= 200;
                //TxtDinheiro.text = dinheiro.ToString();
            }

            // Atualiza a última posição do jogador no MotoController
            motoController.lastPosition = other.transform.position;
        }
    }
}
