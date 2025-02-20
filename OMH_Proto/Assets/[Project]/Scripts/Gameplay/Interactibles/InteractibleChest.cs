using UnityEngine;
using DG.Tweening;

public class Chest_Shader_Controller : Interactible
{
    [Space]
    [SerializeField] private GameEvent _onOpenChestEvent;
    [SerializeField] private GameObject _mapPin;
    [Space]
    [SerializeField] private float _animationDuration = 1;
    [SerializeField] private Transform _topPart;
    [SerializeField] private ParticleSystem _openParticle;
    // [SerializeField] private Light _light;
    // [SerializeField] private float _lightIntensity = 5;

    public override void OnQTEWin()
    {
        _onOpenChestEvent.Raise();

        _topPart.DOLocalRotate(new Vector3(-195, 0, 0), _animationDuration);
        // _light.DOIntensity(_lightIntensity, _animationDuration / 2)
        // .OnComplete(() => _light.DOIntensity(0, _animationDuration / 2));

        if (_openParticle)
        {
            ParticleSystem p = Instantiate(_openParticle, transform);
            Destroy(p.gameObject, p.main.duration + 1);
        }

        _mapPin.SetActive(false);
        Destroy(this);
    }
}
