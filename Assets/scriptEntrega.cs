using UnityEngine;
using UnityEngine.UI;
public class PontoEntrega : MonoBehaviour
{
    public bool entregaFeita = false;
    private GerenciadorDeEntregas scriptGerenciadorEntrega;
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public int dinheiro = 1000;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!entregaFeita)  
            {
                dinheiro+=100;
                entregaFeita = true;
                Debug.Log("Entrega realizada!");
                GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
                scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
                if(scriptGerenciadorEntrega != null)
                {
                    scriptGerenciadorEntrega.EntregaRealizada();
                }
                SetObjectVisibility(false);
                AtualizarTexto(dinheiro);
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
    void AtualizarTexto(int dinheiro)
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