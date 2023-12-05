using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform _target;
    public float smoothTime = 0.15f;
    public Vector3 _offset;
    private Vector3 _velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        
        if (_target != null)
        {
            Vector3 targetPosition = _target.position + _offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }

    }
}
