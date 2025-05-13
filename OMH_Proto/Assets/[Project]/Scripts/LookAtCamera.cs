using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool _alignToCamera = false;
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {

        if (_alignToCamera)
        {
            transform.forward = _mainCam.transform.forward;
        }
        else
        {
            Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
            transform.forward = newOrientation;
        }
    }
}
