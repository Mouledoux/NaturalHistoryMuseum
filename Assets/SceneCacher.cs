using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCacher : MonoBehaviour
{
    public string cacheCompleteMessage = "";
    public UnityEngine.Events.UnityEvent onCacheComplete;

    private IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        //onCacheComplete.AddListener(() => Destroy(gameObject));

        yield return new WaitForEndOfFrame();
        AsyncOperation loadingNextScene;
        for (int i = 1; i < SceneManager.sceneCount; ++i)
        {
            loadingNextScene = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadingNextScene.isDone);
            print(i + "done");
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));

        onCacheComplete.Invoke();
        Mouledoux.Components.Mediator.instance.NotifySubscribers(cacheCompleteMessage);
    }
}
