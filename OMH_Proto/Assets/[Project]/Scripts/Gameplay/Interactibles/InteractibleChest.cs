using UnityEngine;
using DG.Tweening;

public class Chest_Shader_Controller : Interactible
{
    [Space]
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private GameEvent _onOpenChestEvent;
    [SerializeField] private Transform _topPart;
    [SerializeField] private GameObject _mapPin;

    public override void OnQTEWin()
    {
        _onOpenChestEvent.Raise();
        
        _topPart.DOLocalRotate(new Vector3(-195, 0, 0), .7f);
        if(_openParticle)
        {
            ParticleSystem p = Instantiate(_openParticle, transform);
            Destroy(p.gameObject, p.main.duration + 1);
        }

        _mapPin.SetActive(false);

        Destroy(this);
        // BoxCollider boxCollider = GetComponent<BoxCollider>();
        // boxCollider.enabled = false;
    }
}
