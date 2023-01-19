using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject _aimPanel;
    [SerializeField] private GameObject _wavePanel;
    [SerializeField] private GameObject _waveText;
    [SerializeField] private GameObject _waveCountdownText;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private GameObject _gameOverText;

    private StarterAssetsInputs _inputs;
    private TMP_Text _waveTextMesh;
    private TMP_Text _waveCountdownTextMesh;
    private TMP_Text _scoreTextMesh;
    private TMP_Text _gameOverTextMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        
        _waveTextMesh = _waveText.GetComponent<TMP_Text>();
        _waveCountdownTextMesh = _waveCountdownText.GetComponent<TMP_Text>();
        _scoreTextMesh = _scoreText.GetComponent<TMP_Text>();
        _gameOverTextMesh = _gameOverText.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _aimPanel.SetActive(_inputs.aim);
    }
    
    public void ToggleWavePanel(bool state)
    {
        _wavePanel.SetActive(state);
    }
    
    public void SetWaveNumber(int number)
    {
        _waveTextMesh.text = $"Wave {number}";
    }
    
    public void SetWaveCountdown(string time)
    {
        _waveCountdownTextMesh.text = $"Next wave in {time}";
    }
    
    public void SetScore(int targetDestroyed, int targetTotal)
    {
        _scoreTextMesh.text = $"{targetDestroyed}/{targetTotal} targets";
    }
    
    public void DisplayGameOver(int targetDestroyed, int targetTotal)
    {
        _waveTextMesh.text = "Game Over";
        _gameOverTextMesh.text = $"You destroyed {targetDestroyed}/{targetTotal} targets";
        _waveCountdownTextMesh.text = "";
        _scoreTextMesh.text = "";
    }
}
