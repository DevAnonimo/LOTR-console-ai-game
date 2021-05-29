using UnityEngine;

namespace Scripts.Behavior
{
    public class AgentBehaviour : MonoBehaviour
    {
        protected BaseAgent _Agent;

        public float weight = 1.0f;

        public GameObject target;

        public virtual void Start()
        {
            _Agent = gameObject.GetComponent<BaseAgent>();
        }

        public virtual void Update()
        {
            _Agent.SetSteering(GetSteering(), weight);
            _Agent.transform.LookAt(target.transform);
        }

        public virtual Steering GetSteering()
            => new Steering();
    }
}
