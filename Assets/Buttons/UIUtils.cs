using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils : MonoBehaviour
{
    private string sceneLoadMessage = "";
    public void SetSceneLoadMessage(string message)
    {
        sceneLoadMessage = message;
    }
    public string GetSceneLoadMessage()
    {
        return sceneLoadMessage;
    }


    public void GoToScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void GoToScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void ReloadCurrentScene()
    {
        GoToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }



    public void GoToSceneAsync(int index)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == index) return;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
    }

    public void GoToSceneAsync(string name)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == name) return;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
    }

    public void ReloadCurrentSceneAsync()
    {
        GoToSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    private void OnLevelWasLoaded(int level)
    {
        NotifySubscribers(sceneLoadMessage);
    }


    public static void StaticReloadCurrentSceneAsync()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ToggleGameobjectEnable(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void NotifySubscribers(string subscription)
    {
        Mouledoux.Components.Mediator.instance.NotifySubscribers(subscription);
    }



    public void ResetPosition(GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
    }

    public void ResetRotation(GameObject go)
    {
        go.transform.localRotation = Quaternion.identity;
    }
}