using UnityEngine;

namespace DefaultNamespace
{
    public class SeekBehaviour : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            var steer = new Steering {Linear = target.transform.position - transform.position};
            steer.Linear.Normalize();
            steer.Linear *= _Agent.maxAcceleration;
            Debug.DrawRay(transform.position, steer.Linear, Color.green);
            return steer;
        }
    }
}
