using UnityEngine;
using UnityEngine.UI;
public class PontoEntrega : MonoBehaviour
{
    public bool entregaFeita = false;
    private GerenciadorDeEntregas scriptGerenciadorEntrega;
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip dinheiroClip; // Referência ao Clip de áudio
    int dinheiro;
    //public int dinheiro = parseInt(TxtDinheiro.text);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!entregaFeita)  
            {
                entregaFeita = true;
                Debug.Log("Entrega realizada!");
                GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
                scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
                if(scriptGerenciadorEntrega != null)
                {
                    audioSource.PlayOneShot(dinheiroClip);
                    scriptGerenciadorEntrega.EntregaRealizada();
                }
                SetObjectVisibility(false);
                dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                dinheiro+=200;
                AtualizarTexto(" " + dinheiro.ToString());
            }
        }
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
        }
        else
        {
            Debug.LogError("Referência a AtualizaDinheiro não está atribuída no Inspector!");
        }
    }
}