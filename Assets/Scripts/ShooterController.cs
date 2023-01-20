using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DigitalRuby.LightningBolt;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class ShooterController : MonoBehaviour
{
    [FormerlySerializedAs("_shootingTarget")] [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _shootingOrigin;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _lightningObject;
    [SerializeField] private GameObject _hitEffect;

    private bool _canShoot = true;
    
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;

    private CameraController _cameraController;
    private CinemachineBrain _cinemachineBrain;
    private LightningBoltScript _lightningBoltScript;
    
    // Start is called before the first frame update
    private void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;
        _cameraController = _player.GetComponent<CameraController>();
        _cinemachineBrain = _mainCamera.GetComponent<CinemachineBrain>();
        _lightningBoltScript = _lightningObject.GetComponent<LightningBoltScript>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
        var ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }
        
        _targetPoint.position = Vector3.Lerp(_targetPoint.position, targetPosition, 0.05f);
        
        _lightningBoltScript.StartPosition = _shootingOrigin.position;
        _lightningBoltScript.EndPosition = _targetPoint.position;

        if (!_inputs.fire)
        {
            _canShoot = true;
        }

        if (_inputs.fire && _inputs.aim && _canShoot)
        {
            _shoot();
            
            if (hit.collider != null && hit.collider.CompareTag("Target"))
            {
                StartCoroutine(HitTarget(hit.collider.GetComponent<TargetController>()));
            }
            
            _canShoot = false;
            _cameraController.ToggleFireCamera(true);
        } 
    }

    private void _shoot()
    {
        _lightningBoltScript.Trigger();
        var gameObject = Instantiate(_hitEffect, _targetPoint.position, Quaternion.identity);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }
    
    private static IEnumerator HitTarget(TargetController targetController)
    {
        yield return new WaitForSeconds(0.1f);
        targetController.Toggle(false);
    }
}
