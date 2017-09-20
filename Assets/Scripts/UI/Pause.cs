using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pausePanel;
    public bool checkPanel;
    private AudioSource audio;

    private void Start()
    {
        
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audio.Pause();
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            checkPanel = true;
            Cursor.visible = true;
        }
        else if (checkPanel==true && Input.GetKeyDown(KeyCode.Escape) )
        {
            Resume();
        }
    }
    
    public void Resume()
    {
        audio.UnPause();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        checkPanel = false;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
		SceneManager.LoadScene ("MainMenu");
    }

}
