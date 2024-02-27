using UnityEngine;
using UnityEngine.UI;

public class AudioSubscriber : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SoundManager.Instance.ChangeVolumeMusicSlider);
        SFXSlider.onValueChanged.AddListener(SoundManager.Instance.ChangeVolumeSFXSlider);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(SoundManager.Instance.ChangeVolumeMusicSlider);
        SFXSlider.onValueChanged.RemoveListener(SoundManager.Instance.ChangeVolumeSFXSlider);
    }
}
