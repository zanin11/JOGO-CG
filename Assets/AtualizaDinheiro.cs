using UnityEngine;
using UnityEngine.UI;

public class AtualizaDinheiro : MonoBehaviour
{
    public Text TxtDinheiro;  // Referência ao Text na UI para exibir o dinheiro
    public MotoCollision motoCollision;  // Referência ao script MotoCollision
    public int dinheiro = 1000;
    // Atualiza o texto do dinheiro na interface
    public void Start()
    {
        AtualizarTexto(dinheiro);
    }
    public void AtualizarTexto(int novo_valor)
    {
        Debug.Log("To na funcao atualizar");
        dinheiro = novo_valor;
        if (TxtDinheiro != null)
        {
            TxtDinheiro.text = "R$ " + dinheiro.ToString();
            Debug.Log("Texto de dinheiro atualizado: " + TxtDinheiro.text); 
        }
    }
}
