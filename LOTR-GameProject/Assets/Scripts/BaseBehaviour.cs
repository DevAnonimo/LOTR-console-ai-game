using System;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public enum EnemyPossibleState
    {
        Idle,
        Seek,
        Attack
    }

    private float _currentSpeed;

    public float maxSpeed;
    public float distanceToAttack;
    public float distanceToBeginArrival;

    public SeekState seekState;

    public SeekBehaviour seekBehaviour;

    public BaseAgent agent;
    public EnemyPossibleState currentState;
    public GameObject target;

    private void Start()
    {
        agent = gameObject.AddComponent<BaseAgent>();
        agent.speed = maxSpeed;
        _currentSpeed = maxSpeed;
        _currentSpeed = maxSpeed;
    }

    private void FixedUpdate()
    {
        ControlMovingBehaviour();
    }

    public void ChangeState(EnemyPossibleState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyPossibleState.Idle:
                DestroyImmediate(seekState);
                break;
            case EnemyPossibleState.Seek:
                if (gameObject.GetComponent<SeekState>() == null)
                    seekState = gameObject.AddComponent<SeekState>();
                break;
        }
    }

    private void ControlMovingBehaviour()
    {
        var distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < distanceToBeginArrival)
        {
            _currentSpeed -= _currentSpeed * Time.deltaTime;

            if (distance < 1.5f)
                _currentSpeed = 0f;
        }

        if (distance > distanceToBeginArrival && _currentSpeed <= maxSpeed)
            _currentSpeed += maxSpeed * Time.deltaTime;

        agent.speed = _currentSpeed;
    }
}
