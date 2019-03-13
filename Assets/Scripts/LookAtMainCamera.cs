using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{    
    void Update()
    {
        if (Camera.main == null) return;

        Vector3 camPos = Camera.main.transform.position;
        camPos.y = transform.position.y;

        transform.LookAt(camPos);
    }
}
