using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _stateName;
    [SerializeField] private TextMeshPro _countText;
    private Vector3 _startScale;

    private void Start()
    {
        _startScale = _animator.transform.localScale;
        _animator.gameObject.SetActive(false);
    }

    public void StartVisualSequence(float duration)
    {
        _animator.gameObject.SetActive(true);
        _animator.transform.localScale = _startScale;
        _animator.Play(_stateName);
        DOTween.To((time) =>
        {
            string[] s = time.ToString().Split(',');
            if (s.Count() > 1)
                _countText.text = s[0] + '.' + s[1][0];
            else
                _countText.text = s[0];
        }, duration, 0, duration)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            _animator.Play("Hide");
            StartCoroutine(Delais(.15f, () => _animator.transform.DOScale(Vector3.zero, .2f)
                                                .OnComplete(() => _animator.gameObject.SetActive(false))));
        });
    }

    private IEnumerator Delais(float delay, Action toDo)
    {
        yield return new WaitForSeconds(delay);
        toDo.Invoke();
    }
}
