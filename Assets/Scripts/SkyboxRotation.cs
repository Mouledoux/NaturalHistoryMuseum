using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", (Time.time % 360f) * speed);
    }
}
