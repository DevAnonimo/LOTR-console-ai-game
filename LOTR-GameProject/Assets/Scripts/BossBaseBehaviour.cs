using System;
using UnityEngine;

public class BossBaseBehaviour : MonoBehaviour
{
    private float _currentSpeed;

    public float maxSpeed;
    public float distanceToAttack;
    public float distanceToBeginArrival;

    public SeekState seekState;

    public SeekBehaviour seekBehaviour;

    public BaseAgent agent;
    public BossPossibleState currentState;
    public GameObject target;

    private void Start()
    {
        agent = gameObject.AddComponent<BaseAgent>();
        agent.speed = maxSpeed;
        _currentSpeed = maxSpeed;
        _currentSpeed = maxSpeed;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (currentState == BossPossibleState.Idle)
                ChangeState(BossPossibleState.Seek);
            else
                ChangeState(BossPossibleState.Idle);
        }
    }

    private void FixedUpdate()
    {
        BeginArrival();
    }

    private void ChangeState(BossPossibleState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case BossPossibleState.Idle:
                DestroyImmediate(seekState);
                break;
            case BossPossibleState.Seek:
                if (gameObject.GetComponent<SeekState>() == null)
                    seekState = gameObject.AddComponent<SeekState>();
                break;
        }
    }

    private void BeginArrival()
    {
        var distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < distanceToBeginArrival)
        {
            _currentSpeed -= _currentSpeed * Time.deltaTime;

            if (distance < 0.5f)
                _currentSpeed = 0f;
        }

        if (distance > distanceToBeginArrival && _currentSpeed <= maxSpeed)
            _currentSpeed += maxSpeed * Time.deltaTime;

        agent.speed = _currentSpeed;
    }

    public enum BossPossibleState
    {
        Idle,
        Seek,
        Attack
    }
}
