using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VFX_OnHit : MonoBehaviour
{
    public Material Material;

    private int _segmentCount;
    private float _radius;
    private float _expandSpeed;

    private LineRenderer _lineRenderer;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        if (TryGetComponent(out LineRenderer rend))
        {
            _lineRenderer = rend;
        }
        if (TryGetComponent(out MeshRenderer mesh))
        {
            _meshRenderer = mesh;
        }
    }

    public void SetExplosion(int segmentCount, float radius, float speed, bool hasNormalPierce)
    {
        _lineRenderer.enabled = true;
        _segmentCount = segmentCount;
        _radius = radius;   
        _expandSpeed = speed * radius;
        _meshRenderer.enabled = hasNormalPierce;
        _lineRenderer.positionCount = segmentCount + 1;
        _lineRenderer.material = Material;
        _lineRenderer.startWidth = 0.15f;
        _lineRenderer.endWidth = 0.15f;
        StartCoroutine(Blast(hasNormalPierce));
    }

    private IEnumerator Blast(bool hasPierce)
    {
        float currentRadius = 0f;

        while(currentRadius < _radius)
        {
           
            currentRadius += Time.deltaTime * _expandSpeed;
            Draw(currentRadius);
            yield return null;  
        }
        if(currentRadius >= _radius)
        {
            if(!hasPierce)
            {
                Destroy(this.gameObject);

            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }

    private void Draw(float currectRadius)
    {
     
        float angleBetweenPoints = 360 / _segmentCount;

        for (int i = 0; i <= _segmentCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            Vector3 position = this.gameObject.transform.position + (direction * currectRadius);

            _lineRenderer.SetPosition(i, position);
        }
    }

    public void RenderSwitch(bool state)
    {
        _lineRenderer.enabled = state;
    }
}
