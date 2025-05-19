using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    // [SerializeField] private string _sceneToUnLoad;
    [SerializeField] private GameObject _slideAnimation;
    [Space]
    [SerializeField] private float _skipSpeed = .5f;
    [SerializeField] private Image _chargeImage;
    [SerializeField] private TextSequence _loadingTextSequence;
    private AsyncOperation _asyncLoading;
    private bool _chargeSkip;
    private float _time;
    private bool flowControl = true;

    void Start()
    {
        // _asyncLoading = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        // _asyncLoading.allowSceneActivation = false;
    }

    void Update()
    {
        // print(_asyncLoading.progress);
        // if (_asyncLoading.progress >= .9f)
        _loadingTextSequence.IndexToPlay = 1;
        ChargeSkip();
    }

    private void ChargeSkip()
    {
        if (_chargeSkip)
            _time += Time.deltaTime * _skipSpeed;
        else
            _time -= Time.deltaTime;

        _time = Mathf.Clamp01(_time);
        _chargeImage.fillAmount = _time;

        if (_time >= 1 && flowControl)
        {
            flowControl = false;
            ScreenHider.instance.HideScreenForDuration(2, .2f, () =>
            {
                // _asyncLoading.allowSceneActivation = true;
                // _slideAnimation.SetActive(false);
                SceneManager.LoadScene(_sceneToLoad);
            });
        }
    }

    private void OnSkipLoading(InputValue value)
    {
        _chargeSkip = value.Get<float>() > .5f;
    }
}
