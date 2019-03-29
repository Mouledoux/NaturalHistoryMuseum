using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCacher : MonoBehaviour
{
    public Text currentLoading;
    public Slider loadingBar;

    public string cacheCompleteMessage = "";
    public UnityEngine.Events.UnityEvent onCacheComplete;

    private static bool complete = false;

    public int endScene;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        DontDestroyOnLoad(gameObject);
        AsyncOperation loadingNextScene;

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            loadingNextScene = SceneManager.LoadSceneAsync(i);
            MuteAllAudio();

            while (!loadingNextScene.isDone)
            {
                currentLoading.text = "Loading: " + SceneManager.GetSceneByBuildIndex(i).name + " " + ((int)(100 * loadingNextScene.progress)).ToString() + "%";
                loadingBar.value = loadingNextScene.progress;
                yield return null;
            }
        }

        SceneManager.LoadSceneAsync(endScene);
        complete = true;
    }

    private void Update()
    {
        if (complete)
        {
            StopAllCoroutines();
            Destroy(gameObject);
            Mouledoux.Components.Mediator.instance.NotifySubscribers(cacheCompleteMessage);
        }
    }

    public void MuteAllAudio()
    {
        foreach(AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.mute = true;
        }
    }
}
