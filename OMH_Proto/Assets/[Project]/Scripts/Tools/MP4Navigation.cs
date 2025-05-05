using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MP4Navigation : MonoBehaviour
{
    [SerializeField] private VideoPlayer _vodPlayer;

    [SerializeField] private List<Chapter> _chapterList;

    private bool _isSeekDone = false;

    [Serializable]
    public class Chapter
    {
        public int seconds;
        public Button button;
    }

    private void Start()
    {
        foreach (var item in _chapterList)
            item.button.onClick.AddListener(() => { GoTo(item.seconds); });
    }

    [ContextMenu("Set Time")]
    public void GoTo(int seconds)
    {
        print("GoTo : " + seconds);
        // if (!_isSeekDone) return;
        _vodPlayer.Pause();
        _vodPlayer.time = seconds;
        _vodPlayer.Play();

        // _vodPlayer.frame = (long)Mathf.Lerp(0, _vodPlayer.clip.frameCount, _time) - 1;
        // _vodPlayer.seekCompleted += SeekDone;
        // _isSeekDone = false;
    }

    // private void SeekDone(VideoPlayer v)
    // {
    //     StartCoroutine(WaitFrame());
    // }

    // private IEnumerator WaitFrame()
    // {
    //     yield return new WaitForEndOfFrame();
    //     _isSeekDone = true;
    // }
}
