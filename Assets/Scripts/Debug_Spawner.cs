using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnObject = default;
    [SerializeField]
    private float _spawnTime = 1.0f;
    [SerializeField]
    private float _force = 100.0f;
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        while (_timer >= _spawnTime && _spawnTime > 0)
        {
            var obj = Instantiate(_spawnObject, transform.position, Quaternion.identity);
            var rgbd = obj.GetComponent<Rigidbody>();
            rgbd?.AddForce(Random.insideUnitSphere * _force);
            _timer -= _spawnTime;
        }
    }
}
