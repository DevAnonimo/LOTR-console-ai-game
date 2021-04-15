using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BossBaseBehaviour : MonoBehaviour
    {
        public float weight = 1.0f;

        public GameObject target;
        protected BaseBossAgent agent;
        public Vector3 destination;

        public float maxSpeed = 30.0f;
        public float maxAcceleration = 30.0f;
        public float maxRotation = 10.0f;
        public float maxAngularAcceleration = 10.0f;

        public virtual void Start()
        {
            agent.gameObject.GetComponent<BaseBossAgent>();
        }

        public virtual void Update()
        {
            agent.SetSteering(GetSteering(), weight);
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
