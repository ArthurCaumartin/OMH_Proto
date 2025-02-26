using UnityEngine;

[ExecuteInEditMode]
public class AimVisualAssist : MonoBehaviour
{
    [SerializeField] private float _lenght = 5;
    [SerializeField, Range(2, 100)] private int _pointCount = 5;
    private LineRenderer _line;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3[] pointsArray = new Vector3[_pointCount];
        for (int i = 0; i < _pointCount; i++)
        {
            float loopTime = Mathf.InverseLerp(1, _pointCount, i + 1);
            pointsArray[i] = Vector3.Lerp(Vector3.zero, Vector3.forward * _lenght, loopTime);
        }

        _line.positionCount = _pointCount;
        _line.SetPositions(pointsArray);
    }
}
