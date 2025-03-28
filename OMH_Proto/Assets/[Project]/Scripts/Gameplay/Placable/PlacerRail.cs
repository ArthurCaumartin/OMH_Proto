using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlacerRail : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private LineRenderer _lineRenderer;
    private Vector3 _lastPosReturn = Vector3.zero;

    void Start()
    {
        if (!_lineRenderer) return;

        for (int i = 0; i < 50; i++)
        {
            _lineRenderer.SetPosition(i, Vector3.Lerp(_startPoint.position + new Vector3(0, .3f, 0)
                                        , _endPoint.position + new Vector3(0, .3f, 0), Mathf.InverseLerp(0, 50, i)));
        }
    }

    public Vector3 GetNearestPosition(Vector3 position)
    {
        float posTime = Vector3Utils.InverseLerp(_startPoint.position, _endPoint.position, position);
        _lastPosReturn = Vector3.Lerp(_startPoint.position, _endPoint.position, posTime);
        return _lastPosReturn;
    }

    public Vector3 GetDirection()
    {
        return (_startPoint.position - _endPoint.position).normalized;
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_startPoint.position, _endPoint.position);
        if (_lastPosReturn != Vector3.zero) Gizmos.DrawSphere(_lastPosReturn, .3f);
    }
}
