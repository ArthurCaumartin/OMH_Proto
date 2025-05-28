using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interactible : MonoBehaviour
{
    [SerializeField] private QTE _qte;
    public QTE QTE { get => _qte; }
    public bool HaveQTE { get => _qte; }

    public string _textToInteract;
    [SerializeField] private Canvas _interactText;
    [SerializeField] private List<MeshRenderer> _meshRenderersToOutline;
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

        EnableFeedback(false);
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

    public void EnableInteractFeedback()
    {
        _isOutlined = true;
        _timer = 0;
        EnableFeedback(true);
        // for (int i = 0; i < _meshRenderersToOutline.Count; i++)
        // {
        //     _interactText.transform.DOScale(Vector3.one, .2f).SetEase(Ease.InOutBounce);
        //     _meshRenderersToOutline[i].materials[_meshRenderersToOutline[i].materials.Length - 1].SetFloat("_outlineAlpha", 1);
        // }
    }

    public void Update()
    {
        if (!_isOutlined) return;

        _timer += Time.deltaTime;
        if (_timer >= _timeToVerifyOutline)
        {
            _isOutlined = false;
            EnableFeedback(false);
            // for (int i = 0; i < _meshRenderersToOutline.Count; i++)
            // {
            //     _interactText.transform.DOScale(Vector3.zero, .2f).SetEase(Ease.InOutBounce);
            //     _meshRenderersToOutline[i].materials[_meshRenderersToOutline[i].materials.Length - 1].SetFloat("_outlineAlpha", 0);
            // }
        }
    }

    private void EnableFeedback(bool value)
    {
        if (_interactText)
            _interactText.transform.DOScale(value ? Vector3.one : Vector3.zero, .2f).SetEase(Ease.Linear);
        for (int i = 0; i < _meshRenderersToOutline.Count; i++)
            _meshRenderersToOutline[i].materials[_meshRenderersToOutline[i].materials.Length - 1].SetFloat("_outlineAlpha", value ? 1 : 0);
    }
}
