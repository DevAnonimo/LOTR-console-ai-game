using System;
using UnityEngine;

public class BossBaseBehaviour : MonoBehaviour
{
    public float maxSpeed;

    public SeekState seekState;

    public SeekBehaviour seekBehaviour;

    public BaseAgent agent;
    public BossPossibleState currentState;
    public GameObject target;

    private void Start()
    {
        agent = gameObject.AddComponent<BaseAgent>();
        agent.maxSpeed = maxSpeed;
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
}

public enum BossPossibleState
{
    Idle,
    Seek,
    Attack
}
