using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BloonStats : MonoBehaviour, IDamageAble
{
    [SerializeField] private float _speed;

    [SerializeField] private int _health;

    [SerializeField] private bool _isCeramic, _isMoab, _isPurple, _isWhite, _isBlack, _isLead, _isFortified, _isBoss;

    [SerializeField] private Renderer _thisObject;

    private PathingDef _pathingDef;

    private int _currentHealth, _currentLayer;
    public WaveSpawner spawner;
    public LayerNames StartingLayer;

    private BloonLayers _layerDef = new BloonLayers();

    //temps
    public PathNode StartNode;
    public List<Color> colors;
    public Material _thisMaterial;
    private void Start()
    {
        _pathingDef = this.gameObject.AddComponent(typeof(PathingDef)) as PathingDef;

        // this might be temporary
        _thisMaterial.color = colors[(int)StartingLayer];

        SetIniStats();
    }

    private void SetIniStats()
    {
        _pathingDef.IniSetStats(_speed, StartNode, spawner);
        _currentHealth = _health;
        if(_layerDef.LayerDef.TryGetValue(StartingLayer, out int value))
        {
            _currentLayer = value;
        }
    }

    public IDamageAble Damage(int value)
    {
        if(_health > 1)
        {
            _currentHealth -= value;
        }
        else
        {
            _currentLayer -= value;
            Pop();
        }

        if(_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        return null;
    }

    private void Pop()
    {
        if(_currentLayer <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

       
        _thisObject.material.color = colors[_currentLayer - 1];
        


        //some code to spawn multiple bloons
    }
}
