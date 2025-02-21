using UnityEngine;

public class Translate : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private Vector3 _localDirection = Vector3.forward;


    private void Update()
    {
        transform.Translate(_localDirection * _speed.Value * Time.deltaTime);
    }
}
