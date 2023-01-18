using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private CinemachineVirtualCamera _fireCamera;
    
    private StarterAssetsInputs _inputs;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        _aimCamera.enabled = _inputs.aim;
        
        if (_inputs.aim)
        {
            _fireCamera.enabled = _inputs.fire;
        }

        if (_fireCamera.enabled)
        {
            _aimCamera.enabled = false;
        }
    }
    
    public void ToggleFireCamera(bool toggle)
    {
        _fireCamera.enabled = toggle;
    }
}
