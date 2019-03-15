using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    public bool lookAway = false;

    void Update()
    {
        if (Camera.main == null) return;

        Vector3 camPos = Camera.main.transform.position;
        camPos = lookAway ? transform.position + (transform.position - camPos) : camPos;

        camPos.y = transform.position.y;

        transform.LookAt(camPos);
    }
}
