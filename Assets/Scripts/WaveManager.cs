using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private List<WaveController> _waveControllers = new List<WaveController>();
    private int _currentWave = -1;
    private bool _isRunning = false;
    private float _timeBeforeNextWave = 10f;
    private int _totalTargetDestroyed = 0;
    
    private UiController _uiController;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("WaveManager Start");
        
        _uiController = player.GetComponent<UiController>();
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var wave = transform.GetChild(i);
            
            if (wave.GetComponent<WaveController>() != null)
            {
                _waveControllers.Add(wave.GetComponent<WaveController>());
            }
        }
        
        _uiController.ToggleWavePanel(true);
        _isRunning = true;
        
        Debug.Log("WaveManager Start Complete");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            _timeBeforeNextWave -= Time.deltaTime;
            
            _uiController.SetWaveCountdown(FormatTime(_timeBeforeNextWave));
            
            if (_currentWave > -1)
            {
                _uiController.SetWaveNumber(_currentWave + 1);
                _uiController.SetScore(_waveControllers[_currentWave].GetDestroyedTargets(), _waveControllers[_currentWave].GetTotalTargets());
            }

            if (_currentWave > -1 && _waveControllers[_currentWave].IsFinished() || _timeBeforeNextWave <= 0)
            {
                if ((_currentWave + 1) >= _waveControllers.Count)
                {
                    _isRunning = false;
                    _uiController.ToggleWavePanel(false);
                    //TODO: Show total targets destroyed on the total targets
                }
                else
                {
                    if (_currentWave > -1)
                    {
                        _totalTargetDestroyed += _waveControllers[_currentWave].GetDestroyedTargets();
                        _waveControllers[_currentWave].EndWave();
                    }
                    
                    _currentWave++;
                    _waveControllers[_currentWave].StartWave();
                    _timeBeforeNextWave += 30f;
                }
            }
        }
    }

    private static string FormatTime(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        
        return $"{minutes:00}:{seconds:00}";
    }
}
