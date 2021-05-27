public class SeekBehaviour : AgentBehaviour
{
    public override Steering GetSteering()
    {
        var targetPosition = target.transform.position;
        var position = transform.position;
        var targetDirection = targetPosition - position;

        var steer = new Steering
        {
            Linear = targetDirection
        };

        steer.Linear.Normalize();
        steer.Linear *= _Agent.maxAcceleration;
        return steer;
    }
}
