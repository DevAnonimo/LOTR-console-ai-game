using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AgentBehaviour : MonoBehaviour
    {
        protected BaseAgent _Agent;

        public float weight = 1.0f;

        public GameObject target;
        public Vector3 destination;

        public float maxSpeed = 30.0f;
        public float maxAcceleration = 30.0f;
        public float maxRotation = 10.0f;
        public float maxAngularAcceleration = 10.0f;

        public virtual void Start()
        {
            _Agent = _Agent.gameObject.GetComponent<BaseAgent>();
        }

        public virtual void Update()
        {
            _Agent.SetSteering(GetSteering(), weight);
        }

        private float MapToRange(float rotation)
        {
            rotation %= 360.0f;
            if (!(Mathf.Abs(rotation) > 180.0f))
                return rotation;

            if (rotation < 0.0f)
                rotation += 360.0f;
            else
                rotation -= 360.0f;

            return rotation;
        }

        public virtual Steering GetSteering()
            => new Steering();
    }
}
