using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeEntregas : MonoBehaviour
{
    public int totalDeEntregas = 0;
    public Text TxtQtdPedidos; // Arraste o Text da UI para este campo

    public void DefinirTotalDeEntregas(int novoTotal)
    {
        totalDeEntregas = Mathf.Max(0, novoTotal); // Garante que o valor seja sempre positivo ou zero
        AtualizarTextoEntregas();
    }

    public void RemoverPedido()
    {
        if (totalDeEntregas > 0)
        {
            totalDeEntregas--;
            AtualizarTextoEntregas();
        }
    }

    public void NovoPedido()
    {
        if(totalDeEntregas < 8){
            totalDeEntregas++;
            AtualizarTextoEntregas();
        }
        
    }

    public void AtualizarTextoEntregas()
    {
        string texto = string.Format("{0}", totalDeEntregas);
        TxtQtdPedidos.text = texto;
    }

    void Start()
    {
        DefinirTotalDeEntregas(0);
    }
}