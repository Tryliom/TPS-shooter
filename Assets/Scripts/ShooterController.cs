using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class ShooterController : MonoBehaviour
{
    [FormerlySerializedAs("_shootingTarget")] [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _shootingOrigin;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private GameObject _player;

    private bool _canShoot = true;
    
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;

    private CameraController _cameraController;
    private CinemachineBrain _cinemachineBrain;
    
    // Start is called before the first frame update
    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;
        _cameraController = _player.GetComponent<CameraController>();
        _cinemachineBrain = _mainCamera.GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
        var ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
        
        if (Physics.Raycast(ray, out var hit))
        {
            targetPosition = hit.point;
        }
        
        _targetPoint.position = Vector3.Lerp(_targetPoint.position, targetPosition, 0.05f);

        if (!_inputs.fire)
        {
            _canShoot = true;
        }

        if (_inputs.fire && _inputs.aim && _canShoot)
        {
            _shoot();
            _canShoot = false;
            _cameraController.ToggleFireCamera(true);
        }
    }

    private void _shoot()
    {
        var position = _shootingOrigin.position;
        Instantiate(_projectile, position, Quaternion.LookRotation(_targetPoint.position - position));
    }
}
