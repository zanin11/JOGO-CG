using UnityEngine;
using UnityEngine.UI;

public class RedLightDetector : MonoBehaviour
{
    public TrafficLights trafficLight; // Referência ao script do semáforo
    public Transform semaforoFrente; // Ponto de referência que indica a frente do semáforo (adicionado manualmente na hierarquia)
    public AtualizaDinheiro atualizaDinheiro; 
    public Text LoseText;
    public Text Especification;
    public Animation animationComponent;  // Referência ao componente Animation
    public GameObject animatedObject;    // Referência ao GameObject que contém a animação
    public string animationName;
    public Animation animationComponent2;  // Referência ao componente Animation
    public GameObject animatedObject2;    // Referência ao GameObject que contém a animação
    public string animationName2;
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
                    LoseText.text = "-R$300,00";
                    Especification.text = "Sinal Vermelho!";
                    PlayAnimation2("EspecificationLoseMoney");
                    PlayAnimation("LoseMoneyAnimation");
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

    public void PlayAnimation2(string animationName2)
    {
        if (animatedObject2 != null && animationComponent2 != null)
        {
            animatedObject2.SetActive(true); // Torna o GameObject ativo
            animationComponent2.Play(animationName2); // Reproduz a animação
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
     
   private System.Collections.IEnumerator DisableAfterAnimation2()
    {
        // Aguarda a duração da animação (utilizando a duração do clip)
        yield return new WaitForSeconds(animationComponent2[animationName2].length);

        if (animatedObject2 != null)
        {
            animatedObject2.SetActive(false); // Desativa o GameObject após a animação
            Debug.Log("Fim da Animacao!");
        }
    }
       
}
