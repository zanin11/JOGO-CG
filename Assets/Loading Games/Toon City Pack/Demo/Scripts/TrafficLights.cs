using System.Collections;
using UnityEngine;

public enum LightColor { Red, Yellow, Green, None }

public class TrafficLights : MonoBehaviour
{
    public LightColor activeLight;      // Cor ativa atual do semáforo
    private MeshRenderer mr;
    private Shader defShader, unlitShader;
    public float greenLightDuration = 5f;    // Tempo que a luz verde ficará acesa
    public float yellowLightDuration = 2f;   // Tempo que a luz amarela ficará acesa
    public float redLightDuration = 5f;      // Tempo que a luz vermelha ficará acesa

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defShader = Shader.Find("Standard");
        unlitShader = Shader.Find("Unlit/Color");
        SetLight(activeLight);

        // Começa o ciclo de mudança automática de cor
        StartCoroutine(ChangeLightsAutomatically());
    }

    public void SetLight(LightColor color)
    {
        // Certifique-se de que o MeshRenderer tem os materiais corretos
        if (mr.materials.Length < 4)
        {
            Debug.LogError("Certifique-se de que o semáforo tem os materiais corretos configurados.");
            return;
        }

        // Mat 1: Green, Mat 2: Yellow, Mat 3: Red
        int activeIndex = 0;
        switch (color)
        {
            case LightColor.Green:
                activeIndex = 1;
                break;
            case LightColor.Yellow:
                activeIndex = 2;
                break;
            case LightColor.Red:
                activeIndex = 3;
                break;
            default:
                activeIndex = 0;
                break;
        }

        // Itera através dos materiais e ajusta os shaders
        for (int i = 1; i < 4; i++)
        {
            if (mr.materials.Length > i)
            {
                mr.materials[i].shader = (activeIndex == i) ? unlitShader : defShader;
            }
        }

        activeLight = color;  // Atualiza a cor ativa
    }

    // Método para mudar automaticamente as cores em um ciclo
    private IEnumerator ChangeLightsAutomatically()
    {
        while (true)
        {
            SetLight(LightColor.Green);
            yield return new WaitForSeconds(greenLightDuration);  // Espera pelo tempo da luz verde

            SetLight(LightColor.Yellow);
            yield return new WaitForSeconds(yellowLightDuration);  // Espera pelo tempo da luz amarela

            SetLight(LightColor.Red);
            yield return new WaitForSeconds(redLightDuration);  // Espera pelo tempo da luz vermelha
        }
    }
}
