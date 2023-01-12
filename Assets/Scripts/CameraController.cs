using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    
    private StarterAssetsInputs _inputs;
    private CinemachineBasicMultiChannelPerlin _noise;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _noise = _aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        _aimCamera.enabled = _inputs.aim;
        _noise.m_AmplitudeGain = _inputs.fire ? 0.5f : 0f;
    }
}
