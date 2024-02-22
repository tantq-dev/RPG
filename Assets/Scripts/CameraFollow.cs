using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    void LateUpdate()
    {
        this.transform.position = target.transform.position;
    }
}
