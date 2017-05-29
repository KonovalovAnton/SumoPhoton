using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField]    private float _rotationSpeed;
    [SerializeField]    private float _movementSpeed;
    private Vector3 _rotation;

	void Start () {
		
	}


    void Update () {

        float x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        float y = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            FixRotation(x,y);
            MoveForward();
        }   
	}

    private void MoveForward()
    {
        transform.position = transform.position + transform.up * Time.deltaTime * _movementSpeed; 
    }

    private void FixRotation(float x, float y)
    {
        _rotation.x = x;
        _rotation.y = y;
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _rotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}
