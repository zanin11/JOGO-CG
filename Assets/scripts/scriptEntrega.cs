using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PontoEntrega : MonoBehaviour
{
    public bool entregaFeita = false;    

    GerenciadorDeEntregas  scriptGerenciadorEntrega;

    GerenciadorDePontos scriptGerenciadorDePontos;
    
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip dinheiroClip; // Referência ao Clip de áudio
    public bool estaAtivo = false;
    public float tempoDesdeAtivacao;
    int dinheiro;
    public Animation animationComponent;  // Referência ao componente Animation
    public GameObject animatedObject;    // Referência ao GameObject que contém a animação
    public string animationName = "MoneyAnimation"; // Nome da animação a ser executada
    //public int dinheiro = parseInt(TxtDinheiro.text);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!entregaFeita)  
            {
                entregaFeita = true;
                Debug.Log("Entrega realizada!");
                PlayAnimation();
                GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
                scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
                if(scriptGerenciadorEntrega != null)
                {
                    audioSource.PlayOneShot(dinheiroClip);
                    //scriptGerenciadorEntrega.RemoverPedido();
                }
                SetObjectVisibility(false);
                dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                dinheiro+=200;
                AtualizarTexto(" " + dinheiro.ToString());
                FindObjectOfType<GerenciadorDePontos>().Update();
                DesativarPonto();
                FindObjectOfType<GerenciadorDePontos>().AtualizaPontosAtivos();
            }
        }
    }

    public void PlayAnimation()
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

    public void AtivarPonto()
    {
        estaAtivo = true;
        entregaFeita = false;
        // Aqui você pode ativar um material, um collider ou qualquer outro objeto visual
        GetComponent<Renderer>().enabled = true; // Exemplo: ativando o renderer
        tempoDesdeAtivacao = 0;
        GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
        scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
        scriptGerenciadorEntrega.NovoPedido();
    }

    public void DesativarPonto()
    {
        estaAtivo = false;
        entregaFeita = true;
        // Aqui você pode desativar um material, um collider ou qualquer outro objeto visual
        GetComponent<Renderer>().enabled = false; // Exemplo: desativando o renderer
        GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
        scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
        //scriptGerenciadorEntrega.RemoverPedido(); //retira pedido


    }


    void SetObjectVisibility(bool isVisible)
    {
        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
    void AtualizarTexto(string dinheiro)
    {
        // Verifica se atualizaDinheiro não é null
    if (atualizaDinheiro != null)
        {
         atualizaDinheiro.AtualizarTexto(dinheiro);
         //dinheiroAnimator.Play("DinheiroSubindo");  // Toca a animação
        }
        else
        {
            Debug.LogError("Referência a AtualizaDinheiro não está atribuída no Inspector!");
        }
    }
}