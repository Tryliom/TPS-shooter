using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private List<WaveController> _waveControllers;
    private int _currentWave = 0;
    private bool _isRunning = false;
    
    private BoxCollider _boxCollider;
    private UiController _uiController;

    // Start is called before the first frame update
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _uiController = player.GetComponent<UiController>();
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var wave = transform.GetChild(i);
            _waveControllers.Add(wave.GetComponent<WaveController>());
        }
        
        _uiController.ToggleWavePanel(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            //TODO: Display wave number, time and targets left / total
            _uiController.SetWaveNumber(_currentWave);
            

            if (_waveControllers[_currentWave].IsFinished())
            {
                //TODO: Start next wave
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isRunning)
        {
            _isRunning = true;
            _waveControllers[_currentWave].StartWave();
            _uiController.ToggleWavePanel(true);
        }
    }
}
