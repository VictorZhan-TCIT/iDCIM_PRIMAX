using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachineCamera;

    private Transform lookAtTarget;

    private void Awake()
    {
        lookAtTarget = cinemachineCamera.LookAt;
    }

    public void LookAtTarget(Transform target)
    {
        lookAtTarget.position = target.position;
    }

    private void OnValidate()
    {
        cinemachineCamera ??= transform.GetChild(0).GetComponent<CinemachineFreeLook>();
    }
}
