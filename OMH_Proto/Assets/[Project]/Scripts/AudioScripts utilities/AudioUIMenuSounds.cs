using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioUIMenuSounds : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event _Confirm;
    [SerializeField] public AK.Wwise.Event _Back;
    [SerializeField] public AK.Wwise.Event _Switch_perks;
    [SerializeField] public AK.Wwise.Event _Error;
    [SerializeField] public AK.Wwise.Event _Hoover;
    [SerializeField] public AK.Wwise.Event _Select;
    [SerializeField] public AK.Wwise.Event _Start;

   public void OnConfirm()
    {
        _Confirm.Post(gameObject);
    }
    public void OnGoBack()
    {
        _Back.Post(gameObject);
    }
    public void OnSwitchPerks()
    {
        _Switch_perks.Post(gameObject);
    }
    public void OnError()
    { 
        _Error.Post(gameObject);
    }
    public void OnHoover()
    {
        _Hoover.Post(gameObject);
    }
    public void OnSelect()
    {
        _Select.Post(gameObject);
    }
    public void OnStartGame()
    {
        _Start.Post(gameObject);
    }
}
