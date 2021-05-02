using System;
using UnityEngine;

public class BossBaseBehaviour : MonoBehaviour
{
    private float arrivingSpeed;
    private float currentSpeed;

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
        arrivingSpeed = maxSpeed;
        currentSpeed = maxSpeed;
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
        CheckTargetDistance();
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

    private void CheckTargetDistance()
    {
        var distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < distanceToBeginArrival)
        {
            arrivingSpeed -= arrivingSpeed * Time.deltaTime;

            if (distance < 0.5f)
                arrivingSpeed = 0f;
        }

        if (distance > distanceToBeginArrival && arrivingSpeed <= maxSpeed)
        {
            arrivingSpeed += maxSpeed * Time.deltaTime;
        }


        agent.speed = arrivingSpeed;

        Debug.Log($"Distancia: {distance}");
        Debug.Log($"Velocidade: {arrivingSpeed}");
    }

    public enum BossPossibleState
    {
        Idle,
        Seek,
        Attack
    }
}
