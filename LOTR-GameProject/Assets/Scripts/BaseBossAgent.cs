using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BaseBossAgent : MonoBehaviour
    {
        public float maxSpeed = 10.0f;
        public float maxAcceleration;

        public float orientation;
        public float rotation;
        public Vector3 Velocity;
        public Steering Steer;

        public float maxRotation = 45.0f;
        public float maxAngularAcceleration = 45.0f;

        private void Start()
        {
            Velocity = Vector3.zero;
            Steer = new Steering();
        }

        private void Update()
        {
            var displacement = Velocity * Time.deltaTime;
            displacement.y = 0;
            orientation += rotation * Time.deltaTime;
            orientation = CapOrientation(orientation);

            Transform thisTransform;

            (thisTransform = transform).Translate(displacement, Space.World);
            thisTransform.rotation = new Quaternion();
            transform.Rotate(Vector3.up, orientation);
        }

        private void LateUpdate()
        {
            Velocity += Steer.Linear * Time.deltaTime;
            rotation += Steer.Angular * Time.deltaTime;

            Velocity = CapVelocity(Velocity);
            if (Steer.Linear.magnitude == 0.0f)
                Velocity = Vector3.zero;

            Steer = new Steering();
        }

        public void SetSteering(Steering steer, float weight)
        {
            Steer.Linear += weight * steer.Linear;
            Steer.Angular += weight * steer.Angular;
        }

        /// <summary>
        /// Limita a velocidade deste agente ao máximo configurado
        /// </summary>
        /// <param name="currentVelocity">o Vector3 que indica a velocidade atual</param>
        /// <returns>A velocidade corrigida</returns>
        private Vector3 CapVelocity(Vector3 currentVelocity)
        {
            if (!(currentVelocity.magnitude > maxSpeed))
                return currentVelocity;

            currentVelocity.Normalize();
            currentVelocity *= maxSpeed;

            return currentVelocity;
        }

        /// <summary>
        /// Limita a orientação entre 0 e 360 graus
        /// </summary>
        /// <param name="oldOrientation"></param>
        /// <returns>A orientação limitada</returns>
        private float CapOrientation(float oldOrientation)
        {
            if (oldOrientation < 0.0f)
                oldOrientation += 360.0f;
            else if (oldOrientation > 360.0f)
                oldOrientation -= 360.0f;

            return oldOrientation;
        }
    }
}
