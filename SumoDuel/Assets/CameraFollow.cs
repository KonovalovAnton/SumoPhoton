using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]    private Transform _player;
    [SerializeField]    private float _speed;

    private float _cacheZ;

    void Start()
    {
        _cacheZ = transform.position.z;
    }

    void Update () {
        Vector3 pos = Vector3.Lerp(transform.position, _player.position, Time.deltaTime * _speed);
        pos.z = _cacheZ;
        transform.position = pos;
	}
}
