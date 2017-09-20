using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    private AsyncOperation async = null;
    //Always start this coroutine in the start function
    private void Start()
    {
        StartCoroutine(LoadLevel("Level3"));
    }
    //CoRoutine to return async progress, and trigger level load.
    private IEnumerator LoadLevel(string Level)
    {
        SceneManager.LoadSceneAsync(Level);
        yield return async;
    }
}
