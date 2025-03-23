using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private float ambientVolume;
    private float effectsVolume;
    private float musicVolume;
    private float masterVolume;

    private Slider ambientSlider;
    private Slider effectsSlider;
    private Slider musicSlider;
    private Slider materSlider;

    void Start()
    {
        GetVolumes();
        Transform layout = transform.Find("Content/SoundSection/Layout");
        ambientSlider = layout.Find("Ambient/Slider").GetComponent<Slider>();
        effectsSlider = layout.Find("Effects/Slider").GetComponent<Slider>();
        musicSlider = layout.Find("Music/Slider").GetComponent<Slider>();
        materSlider = layout.Find("Master/Slider").GetComponent<Slider>();

        ambientSlider.value = DbToValue(ambientVolume);
        effectsSlider.value = DbToValue(effectsVolume);
        musicSlider.value = DbToValue(musicVolume);
        materSlider.value = DbToValue(masterVolume);
    }

    private void GetVolumes()
    {
        if (PlayerPrefs.HasKey(nameof(ambientVolume)))
        {
            ambientVolume = PlayerPrefs.GetFloat(nameof(ambientVolume));
            audioMixer.SetFloat(nameof(ambientVolume), ambientVolume);
        }
        else if (!audioMixer.GetFloat(nameof(ambientVolume), out ambientVolume))
        {
            Debug.Log(nameof(ambientVolume) + " GetFloat failed");
            ambientVolume = 0f;
        }

        if (PlayerPrefs.HasKey(nameof(effectsVolume)))
        {
            effectsVolume = PlayerPrefs.GetFloat(nameof(effectsVolume));
            audioMixer.SetFloat(nameof(effectsVolume), effectsVolume);
        }
        else if (!audioMixer.GetFloat(nameof(effectsVolume), out effectsVolume))
        {
            Debug.Log(nameof(effectsVolume) + " GetFloat failed");
            effectsVolume = 0f;
        }

        if (PlayerPrefs.HasKey(nameof(musicVolume)))
        {
            musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume));
            audioMixer.SetFloat(nameof(musicVolume), musicVolume);
        }
        else if(!audioMixer.GetFloat(nameof(musicVolume),   out musicVolume))
        {
            Debug.Log(nameof(musicVolume) + " GetFloat failed");
            musicVolume = 0f;
        }

        if (PlayerPrefs.HasKey(nameof(masterVolume)))
        {
            masterVolume = PlayerPrefs.GetFloat(nameof(masterVolume));
            audioMixer.SetFloat(nameof(masterVolume), masterVolume);
        }
        else if (!audioMixer.GetFloat(nameof(masterVolume),  out masterVolume))
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

    public void OnAmbientSliderChanged(float value)
    {
        ambientVolume = ValueToDb(value);
        audioMixer.SetFloat(nameof(ambientVolume), ambientVolume);
        PlayerPrefs.SetFloat(nameof(ambientVolume), ambientVolume);
    }

    public void OnEffectsSliderChanged(float value)
    {
        effectsVolume = ValueToDb(value);
        audioMixer.SetFloat(nameof(effectsVolume), effectsVolume);
        PlayerPrefs.SetFloat(nameof(effectsVolume), effectsVolume);
    }

    public void OnMusicSliderChanged(float value)
    {
        musicVolume = ValueToDb(value);
        audioMixer.SetFloat(nameof(musicVolume), musicVolume);
        PlayerPrefs.SetFloat(nameof(musicVolume), musicVolume);
    }

    public void OnMasterSliderChanged(float value)
    {
        masterVolume = ValueToDb(value);
        audioMixer.SetFloat(nameof(masterVolume), masterVolume);
        PlayerPrefs.SetFloat(nameof(masterVolume), masterVolume);
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    private float DbToValue(float db)
    {
        // [-80..20] --> [0..1]
        float s = (db + 80f) / 100f;
        return s * s;
    }

    public float ValueToDb(float value)
    {
        //  [0..1] --> [-80..20]
        return Mathf.Sqrt(value) * 100 - 80f;
    }
}
