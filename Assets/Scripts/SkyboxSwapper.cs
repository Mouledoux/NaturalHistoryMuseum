using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSwapper : MonoBehaviour
{
    public bool m_random = false;
    public Material[] m_skyboxes;
    private static int m_currentSkyboxIndex = 0;

    void Start()
    {
        RenderSettings.skybox =
            m_random ?
            m_skyboxes[Random.Range(0, m_skyboxes.Length)] :
            m_skyboxes[(++m_currentSkyboxIndex % m_skyboxes.Length)];
    }
}
