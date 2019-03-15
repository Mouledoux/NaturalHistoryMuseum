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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        DontDestroyOnLoad(gameObject);
        AsyncOperation loadingNextScene;

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            loadingNextScene = SceneManager.LoadSceneAsync(i);
            currentLoading.text = SceneManager.GetSceneByBuildIndex(i).name;
            MuteAllAudio();

            while (!loadingNextScene.isDone)
            {
                loadingBar.value = loadingNextScene.progress;
                yield return null;
            }
        }

        SceneManager.LoadSceneAsync(0);
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
