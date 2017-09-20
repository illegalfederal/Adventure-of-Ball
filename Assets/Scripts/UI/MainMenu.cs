using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Panels")]
    public GameObject MainPanel;
    public GameObject SettingsPanel;

    [Header("Post Processing")]
    public PostProcessingProfile postProcessing;
    public Toggle BloomSet;
    public Toggle ColorGradSet;

    public bool checkBloom;
    public bool checkColor;

    void Start()
    {
        MainPanel = GameObject.Find("Main Panel");

        checkBloom = (PlayerPrefs.GetInt("Bloom") != 0);
        checkColor = (PlayerPrefs.GetInt("ColorGrad") != 0);

        if (checkBloom == true)
            BloomSet.isOn = true;
        else
            BloomSet.isOn = false;

        if (checkColor == true)
            ColorGradSet.isOn = true;
        else
            ColorGradSet.isOn = false;
    }

    void Update()
    {
        print(checkBloom + "bloom");
        print(checkColor + "color");
        SaveSettings();
    }

    public void StartGame(){
        SceneManager.LoadScene("Loading");
    }

	public void GoMainMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void GoHighScore(){
		SceneManager.LoadScene ("Database");
	}
	public void ExitButton(){
        PlayerPrefs.Save();
		Application.Quit ();
		print ("Cıkıs Yapıldı!");
	}

    public void MainPanelButton()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void SettingsPanelButton()
    {
        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void Bloom()
    {
        if(BloomSet.isOn == true)
        {
            postProcessing.bloom.enabled = true;
            checkBloom = postProcessing.bloom.enabled;
        }

        else if(BloomSet.isOn == false)
        {
            postProcessing.bloom.enabled = false;
            checkBloom = postProcessing.bloom.enabled;
        }
    }
    public void ColorGrading()
    {
        if (ColorGradSet.isOn == true)
        {
            postProcessing.colorGrading.enabled = true;
            checkColor = postProcessing.colorGrading.enabled;
        }

        else if (ColorGradSet.isOn == false)
        {
            postProcessing.colorGrading.enabled = false;
            checkColor = postProcessing.colorGrading.enabled;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Bloom", (checkBloom ? 1 : 0));
        PlayerPrefs.SetInt("ColorGrad", (checkColor ? 1 : 0));
    }

}
