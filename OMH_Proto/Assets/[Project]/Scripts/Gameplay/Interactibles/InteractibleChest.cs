using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractibleChest : Interactible
{
    [SerializeField] private bool _canBeGolen = true;
    [Space]
    [SerializeField] private GameEvent _onOpenChestEvent, _onOpenGoldenChestEvent;
    [SerializeField] private GameObject _mapPin;
    [Space]
    [SerializeField] private float _delayToGetLoot = 1;
    [SerializeField] private float _animationDuration = 1;
    [SerializeField] private Transform _topPart;
    [SerializeField] private ParticleSystem _openParticle;
    [Space]
    [SerializeField] private GameObject _goldenChestPrefab;

    private bool _isOpen = false;

    public static bool _isGoldenChestPicked = false;

    public bool CanBeGolden { get => _canBeGolen; }

    void Awake()
    {
        _isGoldenChestPicked = false;
    }

    public override void Start()
    {
        base.Start();
        if (_isGoldenChestPicked) return;

        List<InteractibleChest> interactibleChestInScene = (FindObjectsOfType(typeof(InteractibleChest)) as InteractibleChest[]).ToList();
        interactibleChestInScene.RemoveAll(item => !item.CanBeGolden);
        if(interactibleChestInScene.Count == 0) return;
        
        interactibleChestInScene[Random.Range(0, interactibleChestInScene.Count)].TransformationGoldChest();
        _isGoldenChestPicked = true;
    }

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = _isOpen;
    }

    public override void OnQTEWin()
    {
        _isOpen = true;
        _topPart.DOLocalRotate(new Vector3(-195, 0, 0), _animationDuration);
        StartCoroutine(GetLoot(_delayToGetLoot));

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

    public void TransformationGoldChest()
    {
        Instantiate(_goldenChestPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator GetLoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        _onOpenChestEvent.Raise();
    }
}
