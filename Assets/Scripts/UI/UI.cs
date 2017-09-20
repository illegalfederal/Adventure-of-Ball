using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UI : MonoBehaviour {

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private bool checkPanel;

    [Header("Timer")]
    [SerializeField] private float time;
    [SerializeField] private Text TimerText;
    public GameObject TimePanel;
    public Image fillImg;
    float timeAmt = 180;

    [HideInInspector] public AudioSource audio;
    private GameObject ballCharacter;

    private float startSensX;
    private float startSensY;

    void Start()
    {
        ballCharacter = GameObject.Find("Character");
        pausePanel = transform.Find("Canvas/Panel Pause").gameObject;
        TimePanel = transform.Find("Canvas/Panel Fail").gameObject;
        fillImg = GameObject.Find("Canvas/Timer").GetComponent<Image>();

        audio = this.GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("AudioManage");
        print("Volume: " + audio.volume);

        audio.Play();

        // Mouse cursor not visible
        Cursor.visible = false;

        Time.timeScale = 1;

        // Timer
        time = timeAmt;


        startSensX = ballCharacter.GetComponent<BallCamera>().sensivityX;
        startSensY = ballCharacter.GetComponent<BallCamera>().sensivityY;
    }

    void Update()
    {
        // Timer
        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt; // 9/ 10, 8/10.......0/10
        }
        TimeIsOver();

        /// Stop and Cursor visible
        if (checkPanel == false && Input.GetKeyDown(KeyCode.Escape))
        {
            //audio.Pause();
            pausePanel.gameObject.SetActive(true);
            StopTime();
            //Time.timeScale = 0;
            //checkPanel = true;
            //Cursor.visible = true;

            //ballCharacter.GetComponent<BallCamera>().sensivityX = 0.0f;
            //ballCharacter.GetComponent<BallCamera>().sensivityY = 0.0f;
        }

        // Continue and Mouse cursor not visible
        else if (checkPanel == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    private void TimeIsOver()
    {
        if (time <= 0)
        {
            TimePanel.SetActive(true);
            StopTime();
        }
    }

    private void StopTime()
    {
        audio.Pause();
        Time.timeScale = 0;
        checkPanel = true;
        Cursor.visible = true;
        ballCharacter.GetComponent<BallCamera>().sensivityX = 0.0f;
        ballCharacter.GetComponent<BallCamera>().sensivityY = 0.0f;
    }

    public void Resume()
    {
        audio.UnPause();
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        checkPanel = false;
        Cursor.visible = false;
        ballCharacter.GetComponent<BallCamera>().sensivityX = startSensX;
        ballCharacter.GetComponent<BallCamera>().sensivityY = startSensY;
    }
    
    // Level Restart
    public void RestartLevel()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active scene: " + scene.name);
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
