using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private FloatVariable _floatScaleCondition;
    [SerializeField] private Transform _worldLink;
    [SerializeField] private RectTransform _background;
    [Space]
    [SerializeField] private LineRenderer _topLine;
    [SerializeField] private LineRenderer _botLine;
    private Transform _cameraTransform;
    private Vector3 _startLocalPosition;
    private Transform _oldParent;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _startLocalPosition = transform.localPosition;
        _oldParent = transform.parent;
        transform.parent = null;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _floatScaleCondition.Value > 4 ? Vector3.zero : Vector3.one, Time.deltaTime * _speed);
        SetLinePos();
        MoveStuff();
    }

    private void MoveStuff()
    {
        Vector3 offSet = new Vector3(0, Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time)));
        transform.position = Vector3.Lerp(transform.position, _oldParent.position + _startLocalPosition + offSet, Time.deltaTime * _speed);
        _background.forward = (transform.position - _cameraTransform.position).normalized;
    }

    private void SetLinePos()
    {
        Vector3 worldPosBackTop = _background.TransformPoint(new Vector3(_background.rect.width / 2
                                                                        , _background.rect.height / 2, 0));
        _topLine.SetPosition(0, worldPosBackTop);
        _topLine.SetPosition(1, _worldLink.position);

        Vector3 worldPosBackBop = _background.TransformPoint(new Vector3(_background.rect.width / 2
                                                                        , -_background.rect.height / 2, 0));
        _botLine.SetPosition(0, worldPosBackBop);
        _botLine.SetPosition(1, _worldLink.position);

        _topLine.enabled = !(_floatScaleCondition.Value > 4);
        _botLine.enabled = !(_floatScaleCondition.Value > 4);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(_background.position, _background.rotation, _background.localScale);
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(new Vector3(_background.rect.width / 2, _background.rect.height / 2, 0), .1f);
    }
}

