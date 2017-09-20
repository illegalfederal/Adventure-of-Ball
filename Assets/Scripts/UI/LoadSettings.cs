using UnityEngine;
using UnityEngine.PostProcessing;

public class LoadSettings : MonoBehaviour {

    public PostProcessingProfile postProcess;

    public bool bloomPrefs;
    public bool colorPrefs;

    void Start () {
        bloomPrefs = (PlayerPrefs.GetInt("Bloom") != 0);
        if(bloomPrefs == true)
        {
            postProcess.bloom.enabled = true;
        }

        else if(bloomPrefs == false)
        {
            postProcess.bloom.enabled = false;
            print(bloomPrefs);
        }
        print(bloomPrefs + " bloom");

        colorPrefs = (PlayerPrefs.GetInt("ColorGrad") != 0);
        if (colorPrefs == true)
        {
            postProcess.colorGrading.enabled = true;
        }

        else if (colorPrefs == false)
        {
            postProcess.colorGrading.enabled = false;
            print(colorPrefs);
        }
        print(colorPrefs + " color");
    }
}
