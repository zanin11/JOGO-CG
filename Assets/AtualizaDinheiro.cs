using UnityEngine;
using UnityEngine.UI;

public class AtualizaDinheiro : MonoBehaviour
{
    public Text TxtDinheiro;  // Referência ao Text na UI para exibir o dinheiro
    public MotoCollision motoCollision;  // Referência ao script MotoCollision
    int dinheiro;
    public void Start()
    {
        AtualizarTexto(" 1000");
    }
    public void AtualizarTexto(string novo_valor)
    {
        //dinheiro = int.Parse(novo_valor.Replace("R$", "").Trim());
        TxtDinheiro.text = novo_valor;
        Debug.Log("Texto de dinheiro atualizado: " + TxtDinheiro.text); 
        
    }
}
