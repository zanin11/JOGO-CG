using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Referência ao Slider


        void Start()
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(AdjustVolume);
        }

        public void AdjustVolume(float volume)
        {
            AudioListener.volume = volume;
        }
}
