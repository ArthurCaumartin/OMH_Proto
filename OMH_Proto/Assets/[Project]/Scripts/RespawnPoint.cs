using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _countText;

    private void Start()
    {
        _countText.gameObject.SetActive(false);
    }

    public void StartVisualSequence(float duration)
    {
        _animator.Play("Respawn_sequence");
        _countText.gameObject.SetActive(true);
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
            _countText.gameObject.SetActive(false);
        });
    }
}
