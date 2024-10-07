using UnityEngine;
using System.Collections.Generic;

public class VehicleAI : MonoBehaviour
{
    public List<Transform> waypoints; // Lista de waypoints disponíveis
    public float speed = 10f;         // Velocidade do veículo
    public float stopDistance = 5f;   // Distância mínima para parar atrás de outro veículo

    private Transform targetWaypoint;  // Próximo waypoint a seguir
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChooseNewWaypoint(); // Escolhe um waypoint aleatório no início
    }

    void Update()
    {
        if (targetWaypoint == null)
            return;

        // Move o veículo em direção ao waypoint
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0; // Mantém o veículo no plano do mapa, sem subir ou descer

        // Move o veículo em direção ao próximo waypoint
        if (direction.magnitude > stopDistance)
        {
            rb.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
        }
        else
        {
            // Quando o veículo atinge o waypoint, escolhe um novo waypoint
            ChooseNewWaypoint();
        }

        // Rotaciona o veículo para apontar na direção do próximo waypoint
        Quaternion rotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f));
    }

    void ChooseNewWaypoint()
    {
        if (waypoints.Count > 0)
        {
            // Escolhe um waypoint aleatório
            int randomIndex = Random.Range(0, waypoints.Count);
            targetWaypoint = waypoints[randomIndex];
            Debug.Log("New target waypoint: " + targetWaypoint.name);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            // Ajusta a velocidade se um veículo estiver muito perto
            if (Vector3.Distance(transform.position, other.transform.position) < stopDistance)
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * 2f); // Diminui a velocidade gradualmente
                Debug.Log("Speed reduced due to nearby vehicle.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            speed = 10f; // Restaura a velocidade original
            Debug.Log("Speed restored.");
        }
    }
}
