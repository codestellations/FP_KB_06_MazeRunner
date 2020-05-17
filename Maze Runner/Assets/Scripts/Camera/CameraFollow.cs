using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    // public float smoothSpeed = 1f;
    private Vector3 smoothSpeed = Vector3.zero;
    public float smoothTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, player.position, ref smoothSpeed, smoothTime);
        // Vector3 smoothPosition = Vector3.Lerp(transform.position, player.position, smoothSpeed);
        transform.position = smoothPosition;
    }
}
