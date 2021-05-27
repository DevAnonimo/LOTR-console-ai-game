﻿using LOTR_LowPoly;
using UnityEngine;

public class SeekState : MonoBehaviour
{
    private BaseBehaviour _behaviour;
    private GameObject _target;

    private void Start()
    {
        _behaviour = gameObject.GetComponent<BaseBehaviour>();
        _target = _behaviour.target;

        if (_behaviour.seekBehaviour == null)
        {
            _behaviour.seekBehaviour = gameObject.AddComponent<SeekBehaviour>();
            _behaviour.seekBehaviour.target = _target;
            _behaviour.seekBehaviour.weight = 1.0f;
            _behaviour.seekBehaviour.enabled = true;
        }
    }

    private void OnDestroy()
    {
        Destroy(_behaviour.seekBehaviour, 0f);
    }
}
