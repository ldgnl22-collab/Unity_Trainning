using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : MonoBehaviour
{
    [SerializeField] private GameObject _boltPrefab;
    [SerializeField] private int _poolSize = 8;

    private GameObject[] _bolts;
    private int _count;
    
    public static BoltPool Instance { get; private set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        FillPool();
    }

    public GameObject Take()
    {
        if (_count > 0)
        {
            _count--;
            _bolts[_count].SetActive(true);
            return _bolts[_count];
        }
        else
        {
            Debug.Log("Bolt Pool: 남은 것이 없음");
            return null;
        }
    }

    public void Return(GameObject bolt)
    {
        bolt.SetActive(false);
        _bolts[_count] = bolt;
        _count++;
    }

    private void SetSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void FillPool()
    {
        _bolts = new GameObject[_poolSize];

        for (int i = 0; i < _poolSize; i++)
        {
            _bolts[i] = Instantiate(_boltPrefab, transform.position, transform.rotation);
            _bolts[i].SetActive(false);
        }

        _count = _poolSize;
    }
}
