using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField]    private Animator _animator;

    [SerializeField]    private float _rotationSpeed;
    [SerializeField]    private float _movementSpeed;

    private PlayerState _ps;

    private Vector3 _rotation;

	void Start ()
    {
        _ps = new PlayerState(_animator);
    }

    void Update ()
    {
        switch(_ps.GetState())
        {
            case PlayerState.States.Idle:
            case PlayerState.States.Walk:
                CheckTransitions();
                break;
            case PlayerState.States.Hit:
                _ps.WaitForEndOfAnimation();
                break;
        }
	}

    public void FinishAnimation()
    {
        _ps.FinishAnimation();
    }

    private void CheckTransitions()
    {        
        if(CheckHit())
        {
            return;
        }

        CheckMove();  
    }

    private bool CheckHit()
    {
        bool result = false;

        if(Input.GetKeyDown(KeyCode.O))
        {
            result = true;           
            _ps.SetTrigger(PlayerState.States.Hit);
        }

        return result;
    }

    private bool CheckMove()
    {
        float x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        float y = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        bool result;

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            _ps.SetTrigger(PlayerState.States.Walk);
            FixRotation(x, y);
            MoveForward();
            result = true;
        }
        else
        {
            _ps.SetTrigger(PlayerState.States.Idle);
            result = false;
        }

        return result;
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