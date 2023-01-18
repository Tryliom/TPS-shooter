using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private bool _enabled = true;
    
    private CapsuleCollider _capsuleCollider;
    
    public void Initialize()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Toggle(bool state)
    {
        ToggleAllChildrenMeshRenderers(gameObject, state);
        _capsuleCollider.enabled = state;
        _enabled = state;
    }
    
    public bool IsEnabled()
    {
        return _enabled;
    }
    
    private void ToggleAllChildrenMeshRenderers(GameObject node, bool state)
    {
        if (node.GetComponent<MeshRenderer>() != null)
        {
            node.GetComponent<MeshRenderer>().enabled = state;
        }
        
        for (var i = 0; i < node.transform.childCount; i++)
        {
            ToggleAllChildrenMeshRenderers(node.transform.GetChild(i).gameObject, state);
        }
    }
}
