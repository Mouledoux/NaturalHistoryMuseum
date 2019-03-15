using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVideoPlayer : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.Video.VideoPlayer videoPlayer;

    public UnityEngine.Events.UnityEvent OnVideoFinish;

    private void Start()
    {
        GetComponent<Animator>().SetFloat("RandomSpeed", Random.Range(0.5f, 0.7f));  
    }

    public void PlayVideo(float delay = 0f)
    {
        StartCoroutine(iPlayVideo(delay));
    }
    
    public IEnumerator iPlayVideo(float delay)
    {
        yield return new WaitForSeconds(delay);
        videoPlayer.Play();
        
        //while(videoPlayer.transform)

        yield return new WaitUntil(() => videoPlayer.isPlaying);
        yield return new WaitWhile(() => videoPlayer.isPlaying);
        OnVideoFinish.Invoke();
    }

    public void StopVideo()
    {
        if (!videoPlayer.isPlaying) return;

        videoPlayer.Stop();
        OnVideoFinish.Invoke();
    }

}
