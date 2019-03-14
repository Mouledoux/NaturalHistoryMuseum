using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRestart : MonoBehaviour
{
    [SerializeField]
    float idleTime;
    float cIdleTime = 0;

    public UnityEngine.Events.UnityEvent OnIdle;

    bool hasIdled = true;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hasIdled = false;
            cIdleTime = 0f;
            return;
        }

        else
            cIdleTime += Time.deltaTime;

        if(!hasIdled && cIdleTime >= idleTime)
        {
            hasIdled = true;
            cIdleTime = 0;
            OnIdle.Invoke();
        }
    }
}
