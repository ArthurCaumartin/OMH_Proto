using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] private QTE _qte;
    // [SerializeField] private MeshRenderer _meshRenderer;
    protected bool _isPlayerInRange;
    protected Renderer _renderer;
    protected Material _outlineMaterial;
    [SerializeField] protected float _detectionRange = 3f;
    protected float _detectionPerSecond = 2;
    protected float _detectionTime;
    
    public QTE QTE { get => _qte; }
    public bool HaveQTE { get => _qte; }

    protected void Start()
    {
        if (_qte)
        {
            _qte.OnInput.AddListener(OnQTEInput);
            _qte.OnWin.AddListener(OnQTEWin);
            _qte.OnKill.AddListener(OnQTEKill);
        }
        
        _renderer = GetComponent<Renderer>();

        _outlineMaterial = _renderer.materials[_renderer.materials.Length - 1];
        _outlineMaterial.SetFloat("_OutlineThickness", 1f);
    }

    public virtual void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = false;
        print("Interact");
    }

    public virtual void OnQTEInput(bool isInputValide)
    {
        print("INTERACTIBLE : QTE input : " + isInputValide);
    }

    public virtual void OnQTEWin()
    {
        print("INTERACTIBLE : QTE win !");
    }

    public virtual void OnQTEKill()
    {
        print("INTERACTIBLE : QTE kill !");
    } 

    public void OnPlayerInRange()
    {
        _isPlayerInRange = true;
        _outlineMaterial.SetFloat("_OutlineThickness", 1.05f);
    }
    public virtual void Update()
    {
        if (!_isPlayerInRange) return;
        
        _detectionTime += Time.deltaTime;
        if (_detectionTime > 1 / _detectionPerSecond)
        {
            _detectionTime = 0;
            GetPlayerInRange();
        }
    }
    private void GetPlayerInRange()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, _detectionRange);
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].CompareTag("Player"))
            {
                return;
            }
        }
        print("NoMorePlayer");
        _outlineMaterial.SetFloat("_OutlineThickness", 1f);
        _isPlayerInRange = false;
    }
}
