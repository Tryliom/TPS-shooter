using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject _aimPanel;
    [SerializeField] private GameObject _wavePanel;
    
    private TextMeshPro _waveText;
    
    private StarterAssetsInputs _inputs;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        //_waveText = _wavePanel.GetComponentInChildren<TextMeshPro>();
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
        //_waveText.text = "Wave " + number;
    }
}
