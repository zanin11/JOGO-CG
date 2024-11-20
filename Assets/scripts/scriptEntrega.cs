using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PontoEntrega : MonoBehaviour
{
    public bool entregaFeita = false;    

    GerenciadorDeEntregas  scriptGerenciadorEntrega;
    
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip dinheiroClip; // Referência ao Clip de áudio
    public bool estaAtivo = false;
    //public Animator dinheiroAnimator;
    public float tempoDesdeAtivacao;
    int dinheiro;
    public Animator animator;
    public GameObject animatedObject;
    //public int dinheiro = parseInt(TxtDinheiro.text);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!entregaFeita)  
            {
                entregaFeita = true;
                Debug.Log("Entrega realizada!");
                // Ativar animação e esperar finalização (corrotina necessária)
                /*StartCoroutine(PlayMoneyAnimation());
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Aguarda o tempo da animação
                animatedObject.SetActive(false); // Desativa novamente*/
                GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
                scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
                if(scriptGerenciadorEntrega != null)
                {
                    audioSource.PlayOneShot(dinheiroClip);
                    scriptGerenciadorEntrega.RemoverPedido();
                }
                SetObjectVisibility(false);
                dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                dinheiro+=200;
                AtualizarTexto(" " + dinheiro.ToString());
                FindObjectOfType<GerenciadorDePontos>().Update();
            }
        }
    }

    /*private IEnumerator PlayMoneyAnimation()
    {
        // Ativa o objeto e toca a animação
        animatedObject.SetActive(true);
        animator.Play("MoneyAnimation");

        // Aguarda o fim da animação
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        // Desativa o objeto novamente
        animatedObject.SetActive(false);
    }*/

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
        scriptGerenciadorEntrega.RemoverPedido(); //retira pedido
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