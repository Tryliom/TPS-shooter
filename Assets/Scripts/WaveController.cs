using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private List<TargetController> _targets = new List<TargetController>();

    private void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TargetController>().Initialize();
            _targets.Add(transform.GetChild(i).GetComponent<TargetController>());
            
            _targets[i].Toggle(false);
        }
    }

    public void StartWave()
    {
        for (var i = 0; i < _targets.Count; i++)
        {
            _targets[i].Toggle(true);
        }
    }
    
    public void EndWave()
    {
        for (var i = 0; i < _targets.Count; i++)
        {
            _targets[i].Toggle(false);
        }
    }

    public bool IsFinished()
    {
        return GetLeftTargets() == 0;
    }

    public int GetLeftTargets()
    {
        var leftTargets = 0;
        
        for (var i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].IsEnabled())
            {
                leftTargets++;
            }
        }
        
        return leftTargets;
    }
    
    public int GetTotalTargets()
    {
        return _targets.Count;
    }
    
    public int GetDestroyedTargets()
    {
        return GetTotalTargets() - GetLeftTargets();
    }
}
