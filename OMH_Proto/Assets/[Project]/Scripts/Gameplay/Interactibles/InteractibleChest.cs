using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class InteractibleChest : Interactible
{
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

    public static bool _isGoldenChest; 
    private List<InteractibleChest> _chestControllers = new List<InteractibleChest>();

    void Awake()
    {
        _isGoldenChest = false;
    }
    
    void Start()
    {
        base.Start();
        Object[] chest = FindObjectsOfType(typeof(InteractibleChest));
        foreach (InteractibleChest chestShader in chest)
        {
            _chestControllers.Add(chestShader);
        }
        int tempInt = Random.Range(0, _chestControllers.Count);

        // print("Before Golden Chest");
        if (_isGoldenChest) return;
        // print("Golden Chest");
        _chestControllers[tempInt].TransformationGoldChest();
        _isGoldenChest = true;
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
