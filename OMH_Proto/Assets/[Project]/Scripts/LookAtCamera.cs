using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        //? look at camera
        Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
        // newOrientation.x = _mainCam.transform.forward.x;
        // newOrientation.x = .5f;
        // newOrientation.z = .5f;
        transform.forward = newOrientation;
    }
}
