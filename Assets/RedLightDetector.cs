using UnityEngine;
using UnityEngine.UI;

public class RedLightDetector : MonoBehaviour
{
    public TrafficLights trafficLight; // Referência ao script do semáforo
    public Transform semaforoFrente; // Ponto de referência que indica a frente do semáforo (adicionado manualmente na hierarquia)
    public AtualizaDinheiro atualizaDinheiro; 

    public Text TxtDinheiro;
    int dinheiro;

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Verifica se a luz vermelha está ativa
            if (trafficLight.activeLight == LightColor.Red)
            {
                // Calcula a direção entre o player e o ponto de referência à frente do semáforo
                Vector3 direcaoPlayer = (other.transform.position - semaforoFrente.position).normalized;

                // Verifica o ângulo entre a direção do player e a frente do semáforo
                float angulo = Vector3.Angle(semaforoFrente.forward, direcaoPlayer);

                // Detecta apenas se o ângulo for inferior a um valor, indicando que o player está na direção correta
                if (angulo < 90f)  // Ajuste o ângulo conforme necessário
                {
                    
                    Debug.Log("MULTAAAAA!!!! PLAYER PASSOU NO SINAL VERMELHO!");
                    dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                    dinheiro -= 300;
                    TxtDinheiro.text = dinheiro.ToString();
                }
            }
        }
    }

       
}
