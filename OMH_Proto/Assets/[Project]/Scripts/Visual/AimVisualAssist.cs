using UnityEngine;

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
        //! fonctione pas comme je voudrait je sais pas pourquoi... aled :/

        if (!_line) Start();

        Vector3[] posArray = new Vector3[_pointCount];

        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + (transform.forward * _lenght);

        // print(transform.forward);
        // Debug.DrawLine(startPoint, endPoint, Color.red);
        Debug.DrawRay(transform.position, transform.forward * _lenght, Color.red);

        for (int i = 0; i < _pointCount; i++)
        {
            posArray[i] = Vector3.Lerp(startPoint, endPoint, Mathf.InverseLerp(0, _pointCount, i));
        }

        // print(Vector3.Distance(posArray[0], posArray[posArray.Length - 1]));

        _line.SetPositions(posArray);
    }
}
