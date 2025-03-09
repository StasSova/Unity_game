using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private float ambientVolume;
    private float effectsVolume;
    private float musicVolume;
    private float masterVolume;

    void Start()
    {
        if (!audioMixer.GetFloat(nameof(ambientVolume), out ambientVolume))
        {
            Debug.Log(nameof(ambientVolume) + " GetFloat failed");
            ambientVolume = 0f;
        }

        if (!audioMixer.GetFloat(nameof(effectsVolume), out effectsVolume))
        {
            Debug.Log(nameof(effectsVolume) + " GetFloat failed");
            effectsVolume = 0f;
        }

        if (!audioMixer.GetFloat(nameof(musicVolume),   out musicVolume))
        {
            Debug.Log(nameof(musicVolume) + " GetFloat failed");
            musicVolume = 0f;
        }

        if (!audioMixer.GetFloat(nameof(masterVolume),  out masterVolume))
        {
            Debug.Log(nameof(masterVolume) + " GetFloat failed");
            masterVolume = 0f;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            masterVolume = Mathf.Clamp(masterVolume + 2 + Mathf.Abs(0.1f * masterVolume), -80, 20);
            audioMixer.SetFloat(nameof(masterVolume), masterVolume);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            masterVolume = Mathf.Clamp(masterVolume - 2 - Mathf.Abs(0.1f * masterVolume), -80, 20);
            audioMixer.SetFloat(nameof(masterVolume), masterVolume);
        }
    }
}
