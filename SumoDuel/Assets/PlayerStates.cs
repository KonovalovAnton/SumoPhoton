using System.Collections.Generic;
using UnityEngine;

class PlayerState
{
    public enum States
    {
        Idle,
        Walk,
        Hit
    }

    private Animator _animator;

    public PlayerState(Animator _animator)
    {
        this._animator = _animator;
    }

    private List<string> _triggers = new List<string> { "IDLE", "WALK", "HIT" };
    private States _state = States.Idle;
    private bool _emptyState = false;

    public States GetState()
    {
        return _state;
    }

    public void SetTrigger(States state)
    {
        if (_state == state)
        {
            return;
        }

        _state = state;
        ClearAllTriggers();
        _emptyState = false;
        _animator.SetTrigger(_triggers[(int)state]);
    }

    public void WaitForEndOfAnimation()
    {
        if (!_emptyState)
        {
            return;
        }
        SetTrigger(States.Idle);
    }

    public void FinishAnimation()
    {
        _emptyState = true;
    }

    public void ClearAllTriggers()
    {
        foreach (var value in _triggers)
        {
            _animator.ResetTrigger(value);
        }
    }
}
