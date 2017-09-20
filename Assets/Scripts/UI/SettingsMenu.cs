using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider AudioVolume;

    void Start () {
        AudioVolume.value = PlayerPrefs.GetFloat("AudioManage");
	}
	
	// Update is called once per frame
	void Update () {
        AudioSettings();
    }

    void AudioSettings()
    {
        PlayerPrefs.SetFloat("AudioManage", AudioVolume.value);
    }
}
