using UnityEngine;

namespace Scripts.Behavior
{
    public class BaseAgent : MonoBehaviour
    {
        public float speed = 1.0f;
        public float maxAcceleration = 30.0f;

        public Vector3 Velocity;
        public Steering Steer;

        private void Start()
        {
            Velocity = Vector3.zero;
            Steer = new Steering();
        }

        private void Update()
        {
            var displacement = Velocity * Time.deltaTime;
            displacement.y = 0;
            transform.Translate(displacement, Space.World);
        }

        private void LateUpdate()
        {
            Velocity += Steer.Linear * Time.deltaTime;

            Velocity = CapVelocity(Velocity);
            if (Steer.Linear.magnitude == 0.0f)
                Velocity = Vector3.zero;

            Steer = new Steering();
        }

        public void SetSteering(Steering steer, float weight)
        {
            Steer.Linear += weight * steer.Linear;
        }

        /// <summary>
        /// Limita a velocidade deste agente ao máximo configurado
        /// </summary>
        /// <param name="currentVelocity">o Vector3 que indica a velocidade atual</param>
        /// <returns>A velocidade corrigida</returns>
        private Vector3 CapVelocity(Vector3 currentVelocity)
        {
            if (!(currentVelocity.magnitude > speed))
                return currentVelocity;

            currentVelocity.Normalize();
            currentVelocity *= speed;

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
