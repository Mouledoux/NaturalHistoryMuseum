using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCacher : MonoBehaviour
{
    public string cacheCompleteMessage = "";
    public UnityEngine.Events.UnityEvent onCacheComplete;

    private static bool complete = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        if (complete) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        AsyncOperation loadingNextScene;

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            loadingNextScene = SceneManager.LoadSceneAsync(i);
            yield return new WaitUntil(() => loadingNextScene.isDone);
        }

        SceneManager.LoadSceneAsync(0);

        Mouledoux.Components.Mediator.instance.NotifySubscribers(cacheCompleteMessage);
        onCacheComplete.AddListener(() => Destroy(gameObject));
        onCacheComplete.Invoke();

        complete = true;
    }
}
