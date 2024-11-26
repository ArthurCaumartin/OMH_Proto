using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] private QTE _qte;
    public QTE QTE { get => _qte; }
    public bool HaveQTE { get => _qte; }

    void Start()
    {
        if (_qte)
        {
            _qte.OnInput.AddListener(OnQTEInput);
            _qte.OnWin.AddListener(OnQTEWin);
            _qte.OnKill.AddListener(OnQTEKill);
        }
    }

    public virtual void Interact()
    {
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
}
