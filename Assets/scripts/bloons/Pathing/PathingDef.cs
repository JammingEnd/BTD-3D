using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingDef : MonoBehaviour
{
    public float OverallProgress;
    private float _distanceMoved;

    private float _baseSpeed, _currentSpeed;

    private PathNode _targetNode;
    private WaveSpawner _spawner;
    public void IniSetStats(float baseStat, PathNode node, WaveSpawner spawner)
    {
        _baseSpeed = baseStat;
        _currentSpeed = _baseSpeed;
        _targetNode = node.NextNode;
        _spawner = spawner;
        this.transform.LookAt(_targetNode.transform);
    }

    public void SetSpeed(float multiplier)
    {
        _currentSpeed -= _baseSpeed * multiplier;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if(_targetNode.NextNode == null)
        {
            kill();
        }
        if(_targetNode != null)
        {
            var distance = Vector3.Distance(this.transform.position, _targetNode.transform.position);

            if (distance < 0.2)
            {
                if (_targetNode._IsEnd)
                {
                    Destroy(this.gameObject);
                    // lose lives
                }

                _targetNode = _targetNode.NextNode;
               
                Vector3 nodePos = new Vector3(_targetNode.transform.position.x, 0, _targetNode.transform.position.z);
                this.transform.LookAt(nodePos);
            }
        }
        
    }
    private void Move()
    {
        float speed = _currentSpeed * 0.05f;
        this.transform.position = Vector3.MoveTowards(this.transform.position, _targetNode.transform.position, speed);
        _distanceMoved += speed;
        OverallProgress = (_distanceMoved / _spawner.pathContainers[_targetNode.PathLayer].TrackLength ) * 100;
    }

    private void kill()
    {
        Destroy(this.gameObject);
    }
}
