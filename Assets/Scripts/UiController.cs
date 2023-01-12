using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject _aimPanel;
    
    private StarterAssetsInputs _inputs;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        _aimPanel.SetActive(_inputs.aim);
    }
}
