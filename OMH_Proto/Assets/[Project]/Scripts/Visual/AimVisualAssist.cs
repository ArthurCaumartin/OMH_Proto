using UnityEngine;

public class AimVisualAssist : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private WeaponControler _weaponControler;
    [SerializeField] private float _lenght = 5;
    [SerializeField, Range(2, 100)] private int _pointCount = 5;
    private LineRenderer _line;
    private float _lenghtBackup;



    private void Start()
    {
        //! dsl j'ai la flem de faire mieux
        _playerMovement = transform.parent.parent.parent.parent.GetComponent<PlayerMovement>();
        _weaponControler = transform.parent.parent.parent.parent.GetComponent<WeaponControler>();

        _line = GetComponent<LineRenderer>();
        _lenghtBackup = _lenght;
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            float target =
            (!_playerMovement.IsMoving || _weaponControler.IsShooting()) ? _lenghtBackup : 0;

            _lenght = Mathf.Lerp(_lenght, target, Time.deltaTime * 15);
            _line.enabled = _lenght > 0.1;
        }

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
