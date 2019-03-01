using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [Range(1f, 10f)]
    public float rotSpeed;
    public Vector3 rotScale = Vector3.one;

    void Update()
    {
        Vector3 rot = new Vector3(Random.Range(0f, 1f) * rotScale.x, Random.Range(0f, 1f) * rotScale.y, Random.Range(0f, 1f) * rotScale.z) * rotSpeed * Time.deltaTime;
        transform.Rotate(rot, Space.Self);
    }
}
