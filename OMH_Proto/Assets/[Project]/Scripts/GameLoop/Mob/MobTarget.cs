using UnityEngine;

public class MobTarget : MonoBehaviour
{
    [SerializeField] private FloatReference _proirity;
    public float Priority { get => _proirity.Value; }
}
