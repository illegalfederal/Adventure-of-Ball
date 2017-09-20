using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeUp : MonoBehaviour {

    public void EndTime()
    {
        SceneManager.LoadScene("Level1");
    }
}
