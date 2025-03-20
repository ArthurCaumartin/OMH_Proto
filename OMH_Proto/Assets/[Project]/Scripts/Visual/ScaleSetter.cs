using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    [SerializeField] private Vector3 _overrideAxes;
    [SerializeField] private FloatReference _scaleToSet;

    private void Update()
    {
        Vector3 newScale = _overrideAxes;
        newScale.x = newScale.x == 0 ? _scaleToSet.Value : _overrideAxes.x;
        newScale.y = newScale.y == 0 ? _scaleToSet.Value : _overrideAxes.y;
        newScale.z = newScale.z == 0 ? _scaleToSet.Value : _overrideAxes.z;
        transform.localScale = newScale;
    }
}
