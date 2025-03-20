using UnityEngine;

public class MobTarget : MonoBehaviour
{
    [SerializeField] private FloatReference _priotity;
    public float Priority { get => _priotity.Value; set => _priotity.Value = value; }
    public bool enable = true;
}
