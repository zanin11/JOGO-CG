using UnityEngine;
using UnityEngine.UI;
public class MotoCollision : MonoBehaviour
{
    Rigidbody rb;
    Vector3 initialPosition;
    public Text TxtDinheiro;    // Referência ao Text na UI para exibir o dinheiro
    public AtualizaDinheiro atualizaDinheiro; 
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip collisonClip; // Referência ao Clip de áudio
    int dinheiro;
    public Animator dinheiroAnimator;
    // Inicializa o Rigidbody da moto e salva a posição inicial
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Congela todas as rotações (X, Y e Z) para evitar que a moto vire ou gire de forma inesperada
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            initialPosition = transform.position; // Guarda a posição inicial da moto
        }

      
        
    }

    // Método chamado quando a moto colide com outro objeto
    void OnCollisionEnter(Collision collision)
    {
        // Verifica se a moto colidiu com um obstáculo
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Untagged")   // Suponha que o obstáculo tenha a tag 'Obstacle'
        {
            Debug.Log("Moto colidiu com um obstáculo!");
            
            if(collision.gameObject.tag == "Obstacle"){
                // Atualiza o texto na interface
                audioSource.PlayOneShot(collisonClip);
                dinheiro = int.Parse(TxtDinheiro.text.Replace(" ", "").Trim());
                dinheiro-=500;
                AtualizarTexto(" " + dinheiro.ToString());
            }

            if (rb != null)
            {
                // Para o movimento da moto e zera as rotações
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = true; // Garante que a gravidade esteja ativa

                // Congela o movimento no eixo Y durante a colisão
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
        }
        else if(collision.gameObject.tag == "PontoEntrega"){
            Debug.Log("Moto passou por um ponto de entrega!");

        }
    }

    // Método chamado enquanto a moto está em colisão com outro objeto
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Moto ainda está colidindo com: " + collision.gameObject.name);
    }

    // Método chamado quando a colisão termina
    void OnCollisionExit(Collision collision)
    {
        // Verifica se a colisão foi com um obstáculo
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Moto saiu da colisão com: " + collision.gameObject.name);
            if (rb != null)
            {
                // Libera o movimento no eixo Y após a colisão
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                // Corrige a posição da moto para evitar que ela fique em uma altura incorreta
                Vector3 correctedPosition = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
                transform.position = correctedPosition;
            }
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
