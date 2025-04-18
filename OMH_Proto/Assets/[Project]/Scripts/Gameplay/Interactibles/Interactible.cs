using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] private QTE _qte;
    public QTE QTE { get => _qte; }
    public bool HaveQTE { get => _qte; }
    
    public string _textToInteract;
    [SerializeField] private Material _meshToOutline;
    [SerializeField] float _timeToVerifyOutline = 0.3f;
     
    private float _timer;
    private bool _isOutlined;

    public virtual void Start()
    {
        if (_qte)
        {
            _qte.OnInput.AddListener(OnQTEInput);
            _qte.OnWin.AddListener(OnQTEWin);
            _qte.OnKill.AddListener(OnQTEKill);
        }

    }

    public virtual void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = false;
        // print("Interact");
    }

    public virtual void OnQTEInput(bool isInputValide)
    {
        // print("INTERACTIBLE : QTE input : " + isInputValide);
    }

    public virtual void OnQTEWin()
    {
        // print("INTERACTIBLE : QTE win !");
    }

    public virtual void OnQTEKill()
    {
        // print("INTERACTIBLE : QTE kill !");
    }

    public void OutlineInteractible()
    {
        _isOutlined = true;
        _timer = 0;
        //Set Outline to 1
    }

    private void Update()
    {
        if (!_isOutlined) return;
        
        _timer += Time.deltaTime;
        if (_timer >= _timeToVerifyOutline)
        {
            _isOutlined = false;
            //Set Outline to 0
        }
    }
}
