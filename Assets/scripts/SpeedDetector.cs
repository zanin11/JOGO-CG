using UnityEngine;
using UnityEngine.UI;

public class SpeedRadarDetector : MonoBehaviour
{
    public float speedLimit = 15f; // Limite de velocidade em 15
    public Transform radarPosition; // Ponto de referência para a posição do radar
    private int dinheiro;
    public Text LoseText;
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public Animation animationComponent;  // Referência ao componente Animation
    public GameObject animatedObject;    // Referência ao GameObject que contém a animação
    public string animationName;
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
            bool movendoParaFrente = Vector3.Dot(movimentoJogador.normalized, radarPosition.forward) < 0;
            Debug.Log("operacao DOT: " + Vector3.Dot(movimentoJogador.normalized, radarPosition.forward));
            Debug.Log(movendoParaFrente);

            // Aplica a multa apenas se o jogador estiver na frente, se movendo para o radar e acima da velocidade
            if (estaDeFrente && movendoParaFrente && motoController.currentSpeed > speedLimit)
            {
                Debug.Log("MULTAAAAA!!!! VELOCIDADE ACIMA DE 15 KM/H !!!!!!!!!");
                Debug.Log("Velocidade: " + motoController.currentSpeed);
                // Reduz o valor da multa
                dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                dinheiro -= 200;
                AtualizarTexto(" " + dinheiro.ToString());
            }

            // Atualiza a última posição do jogador no MotoController
            motoController.lastPosition = other.transform.position;
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
        void AtualizarTexto(string dinheiro)
    {
        // Verifica se atualizaDinheiro não é null
        if (atualizaDinheiro != null)
        {
            atualizaDinheiro.AtualizarTexto(dinheiro);
            LoseText.text = "-R$200,00";
            PlayAnimation("LoseMoneyAnimation");
        }
        else
        {
            Debug.LogError("Referência a AtualizaDinheiro não está atribuída no Inspector!");
        }
    }
}
