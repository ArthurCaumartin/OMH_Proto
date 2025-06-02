using AK.Wwise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AudioManagerScript : MonoBehaviour
{
    #region Integrity instance check 
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod]
    private static void VerifyAudioManagerInstanceInEditor()
    {
        AudioManagerScript instanceInScene = FindObjectOfType<AudioManagerScript>();

        if (instanceInScene == null)
        {
            Debug.LogWarning("From:AudioManagerScript. No AudioManager in scene !");
        }
        else
        {
            Debug.Log("From:AudioManagerScript. Instanciated.");
        }
    }
#endif
    #endregion
    public static AudioManagerScript Instance {  get; private set; }

    [SerializeField] private float _transitionTimeInSeconds = 1;
    [SerializeField] private AK.Wwise.RTPC RTPC_PerksPlayer;
    [SerializeField] private AK.Wwise.RTPC RTPC_ShieldState;
    [SerializeField] private AK.Wwise.RTPC RTPC_MusicPlayer;
    [SerializeField] private AK.Wwise.RTPC RTPC_Timer;

    #region Coroutine logic to update RTPCs over time variables
    // Has to be a singleton to centralize all the required datas for audioscripts
  
    private void Awake()
    {
    if (Instance != null && Instance != this)
        { Destroy(gameObject); return; }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    }
    public void SetRTPCOverTime (RTPC rtpc, GameObject target, float startValue, float endValue, float duration)
    {
        duration = _transitionTimeInSeconds;
        if (rtpc == null)
        { AudioDebugLog.LogAudio(this.GetType().ToString(), "AudioManager", "RTPC Null"); return; }

        StartCoroutine(RTPCInterpolationCoroutine(rtpc, target, startValue, endValue, duration));
    }

    private IEnumerator RTPCInterpolationCoroutine(RTPC rtpc, GameObject target, float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;
        rtpc.SetValue(target, startValue);

        while (elapsedTime < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            rtpc.SetValue(target, currentValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rtpc.SetValue(target, endValue);
    }
    #endregion
    /* Music manager
      Manage the music player, its transitions, the effects.
     */
    private void FadeInMusic()
    {
        AudioManagerScript.Instance.SetRTPCOverTime(RTPC_MusicPlayer, gameObject, 0f, 100f, _transitionTimeInSeconds);
    }

    private void FadeOutMusic()
    {
        RTPC_MusicPlayer.SetGlobalValue(0);
    }
    /* Gamestate sounds manager
    Manage the sound mix depending on the gamestate (menu opened, pause, etc)
   */
    private void TimerSound()
    {

    }
    /* Ambiences manager
     Manage transitions and states between differents 2D ambiences
    => They are affected by gamestates. 
     BE ADVISED : it does NOT include 3D ambients sounds,
     they'll be attached to gameObjects instead.
    */

   

   

}
