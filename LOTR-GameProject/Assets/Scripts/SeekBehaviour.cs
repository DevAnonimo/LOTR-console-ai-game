namespace DefaultNamespace
{
    public class SeekBehaviour : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            var steer = new Steering {Linear = target.transform.position - transform.position};
            steer.Linear.Normalize();
            steer.Linear *= _Agent.maxAcceleration;
            return steer;
        }
    }
}
