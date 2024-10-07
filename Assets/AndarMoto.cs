using UnityEngine;

public class MotoController : MonoBehaviour
{
    public float speed = 10f;  // Velocidade da moto
    public float turnSpeed = 50f;  // Velocidade de rotação
    public float raycastDistance = 5f;  // Distância do Raycast para detectar o solo
    public float hoverHeight = 1.2f;  // Altura que a moto deve ficar em relação ao solo
    public LayerMask groundLayer;  // Camada que define o que é considerado terreno (ruas)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimentação para frente e para trás (W e S)
        float moveInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveInput * speed * Time.deltaTime;

        // Aplicar o movimento à moto
        rb.MovePosition(rb.position + move);

        // Rotação (A e D)
        float turnInput = Input.GetAxis("Horizontal");
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Ajustar a altura da moto com Raycast
        AdjustToTerrain();
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
