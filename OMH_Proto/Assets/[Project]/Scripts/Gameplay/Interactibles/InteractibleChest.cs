using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Chest_Shader_Controller : Interactible
{
    [Space]
    [SerializeField] private GameEvent _onOpenChestEvent;
    [SerializeField] private GameObject _mapPin;
    [Space]
    [SerializeField] private float _delayToGetLoot = 1;
    [SerializeField] private float _animationDuration = 1;
    [SerializeField] private Transform _topPart;
    [SerializeField] private ParticleSystem _openParticle;
    // [SerializeField] private Light _light;
    // [SerializeField] private float _lightIntensity = 5;
    private bool _isOpen = false;

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = _isOpen;
    }

    public override void OnQTEWin()
    {
        _isOpen = true;
        _topPart.DOLocalRotate(new Vector3(-195, 0, 0), _animationDuration);
        StartCoroutine(GetLoot(_delayToGetLoot));
        // _light.DOIntensity(_lightIntensity, _animationDuration / 2)
        // .OnComplete(() => _light.DOIntensity(0, _animationDuration / 2));

        if (_openParticle)
        {
            ParticleSystem p = Instantiate(_openParticle, transform);
            Destroy(p.gameObject, p.main.duration + 1);
        }

        _mapPin.SetActive(false);
        
        gameObject.layer = LayerMask.NameToLayer("Default");
        // Destroy(gameObject);
        // GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator GetLoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        _onOpenChestEvent.Raise();
    }
}
