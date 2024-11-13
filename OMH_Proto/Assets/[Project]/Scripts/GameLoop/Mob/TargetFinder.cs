using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private PhysicsAgent _agent;
    private MobTarget _currentTarget;
    private Transform _siphonTransform;

    private void Start()
    {
        _agent = GetComponent<PhysicsAgent>();
    }

    public void SetTarget(MobTarget toSet)
    {
        if (!_currentTarget || toSet.Priority > _currentTarget.Priority)
        {
            print("Set / " + toSet.name + " / to new target !");
            _currentTarget = toSet;
            _agent.SetTarget(_currentTarget.transform);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == transform) return;
        MobTarget t = other.GetComponent<MobTarget>();
        print("Trigger Target !");
        if (t) SetTarget(t);
    }
}
