using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmisionColor : MonoBehaviour
{
    public Gradient m_colors;
    public float m_frequency;

    [SerializeField]
    private Material mat;
    private Color originalColor;
    private float currentColor = 0;

    void Start()
    {
        originalColor = mat.GetColor("_EmissionColor");
    }
    
    void Update()
    {
        currentColor = ((currentColor + (m_frequency * Time.deltaTime)) % 1f);
        Color nextColor = m_colors.Evaluate(currentColor);
        mat.SetColor("_EmissionColor", nextColor);
    }

    private void OnDestroy()
    {
        mat.SetColor("_EmissionColor", originalColor);
    }
}
