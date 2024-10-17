using UnityEngine;

public class PontoEntrega : MonoBehaviour
{
    public bool entregaFeita = false;
    private GerenciadorDeEntregas scriptGerenciadorEntrega;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!entregaFeita)
            {
                entregaFeita = true;
                // Aqui você pode adicionar o código para dar o feedback ao jogador
                Debug.Log("Entrega realizada!");
                GameObject otherGameObject = GameObject.Find("TxtQtdPedidos");
                scriptGerenciadorEntrega = otherGameObject.GetComponent<GerenciadorDeEntregas>();
                if(scriptGerenciadorEntrega != null)
                {
                    scriptGerenciadorEntrega.EntregaRealizada();
                }
                // Exibir uma mensagem na tela, tocar um som, etc.
                SetObjectVisibility(false);
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
}