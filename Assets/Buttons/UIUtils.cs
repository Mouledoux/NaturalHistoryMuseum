using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(index);
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ReloadCurrentScene()
    {
        GoToScene(SceneManager.GetActiveScene().name);
    }



    public void GoToSceneAsync(int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == index) return;
        SceneManager.LoadSceneAsync(index);
    }

    public void GoToSceneAsync(string name)
    {
        if (SceneManager.GetActiveScene().name == name) return;

        else if (SceneManager.GetSceneByName(name).IsValid())
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));

        SceneManager.LoadSceneAsync(name);
    }

    public void ReloadCurrentSceneAsync()
    {
        GoToSceneAsync(SceneManager.GetActiveScene().name);
    }


    private void OnLevelWasLoaded(int level)
    {
        NotifySubscribers(sceneLoadMessage);
    }


    public static void StaticReloadCurrentSceneAsync()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
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