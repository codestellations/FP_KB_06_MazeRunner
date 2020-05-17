using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    // public float smoothSpeed = 0.125f;
    private Vector3 smoothSpeed = Vector3.zero;
    public float smoothTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, player.position, ref smoothSpeed, smoothTime);

        transform.position = smoothPosition;
    }
}
