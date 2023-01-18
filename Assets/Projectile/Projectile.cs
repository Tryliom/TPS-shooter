using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _lifeTime = 1f;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
        
        StartCoroutine(DestroyProjectile());
    }

    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Projectile")) return;
        
        if (other.gameObject.CompareTag("Target"))
        {
            var target = other.gameObject;
            target.GetComponent<TargetController>().Toggle(false);
        }
        
        Destroy(gameObject);
    }
}
