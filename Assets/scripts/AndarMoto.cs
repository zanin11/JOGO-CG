using UnityEngine;
using UnityEngine.UI;
public class MotoController : MonoBehaviour
{
    private float accelerationInput; // Entrada do usuário (W ou S)
    public float accelerationForward = 5f; // Aceleração para frente
    public float accelerationBackward = 22f; // Aceleração para trás
    public float decelerationRate = 12f; // Taxa de desaceleração
    public float maxForwardSpeed = 17f;  // Velocidade máxima para frente
    public float maxBackwardSpeed = -10f; // Velocidade máxima para trás
    public float turnSpeed = 155f;  // Velocidade de rotação
    public float raycastDistance = 5f;  // Distância do Raycast para detectar o solo
    public float hoverHeight = 1.2f;  // Altura que a moto deve ficar em relação ao solo
    public float currentSpeed; // Velocidade atual da moto

    private float compensacao;
    public LayerMask groundLayer;  // Camada que define o que é considerado terreno (ruas)
    public Text TxtVelocimetro;
    public Vector3 lastPosition;
    float velocidade;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TxtVelocimetro.text = "0 km/h";
    }

    void Update()
    {
        // Movimentação para frente e para trás (W e S)
        /// Movimentação com aceleração variável e desaceleração
        accelerationInput = Input.GetAxis("Vertical");

        if(currentSpeed < 0) compensacao = 2f;
        else compensacao = 1f;

        // Acelerar ou desacelerar com base na entrada
        if (accelerationInput > 0) // Acelerando para frente
        {
            currentSpeed += accelerationForward * accelerationInput * Time.deltaTime * compensacao; 
        }
        else if (accelerationInput < 0) // Acelerando para trás
        {
            currentSpeed += accelerationBackward * accelerationInput * Time.deltaTime;
        }
        else // Desacelerando
        {
            currentSpeed -= decelerationRate * Mathf.Sign(currentSpeed) * Time.deltaTime;
        }

        // Limitar a velocidade máxima
        currentSpeed = Mathf.Clamp(currentSpeed, maxBackwardSpeed, maxForwardSpeed);

        TxtVelocimetro.text = (Mathf.Abs(currentSpeed)).ToString("F1") + " km/h";

        Vector3 move = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // Rotação (A e D)
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Ajustar a altura da moto com Raycast
        AdjustToTerrain();
        lastPosition = transform.position;
    }

    void AdjustToTerrain()
    {
        RaycastHit hit;

        // Realiza o Raycast para baixo, a partir da moto, para detectar a rua (solo)
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            // Se atingir o solo (rua), ajustar a altura da moto para ficar acima do solo
            Vector3 targetPosition = rb.position;
            targetPosition.y = hit.point.y + hoverHeight;  // Ajusta a altura com base na colisão detectada
            rb.MovePosition(targetPosition);
        }
    }
}
