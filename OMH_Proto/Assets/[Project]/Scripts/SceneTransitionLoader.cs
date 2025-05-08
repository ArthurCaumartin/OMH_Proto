using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private List<GameObject> _objToDisableOnSceneActivation;
    [Space]
    [SerializeField] private float _skipSpeed = .5f;
    [SerializeField] private Image _chargeImage;
    private AsyncOperation _asyncLoading;
    private bool _chargeSkip;
    private float _time;

    void Start()
    {
        _asyncLoading = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        _asyncLoading.allowSceneActivation = false;

    }

    void Update()
    {
        if (_chargeSkip)
        {
            _time += Time.deltaTime * _skipSpeed;
        }
        else
        {
            _time -= Time.deltaTime;
        }

        _time = Mathf.Clamp01(_time);
        _chargeImage.fillAmount = _time;

        if (_time >= 1)
        {
            ScreenHider.instance.HideScreenForDuration(2, .2f, () =>
            {
                foreach (var item in _objToDisableOnSceneActivation)
                    item.SetActive(false);
                _asyncLoading.allowSceneActivation = true;
            });
        }
    }

    private void OnSkipLoading(InputValue value)
    {
        _chargeSkip = value.Get<float>() > .5f;
    }
}
