using UnityEngine;

public class RedLightDetector : MonoBehaviour
{
    public TrafficLights trafficLight; // Referência ao script do semáforo
    
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido tem a tag "Player" (ou a tag que você atribuiu à moto)
        if (other.CompareTag("Player"))
        {
            // Verifica se a luz vermelha está ativa
            if (trafficLight.activeLight == LightColor.Red)
            {
                Debug.Log("A moto passou no sinal vermelho!");
                // Aqui você pode adicionar uma punição, som, etc.
            }
        }
    }
}
