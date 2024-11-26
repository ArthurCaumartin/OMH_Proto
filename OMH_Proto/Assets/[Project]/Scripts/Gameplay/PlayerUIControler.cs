using UnityEngine;

public class PlayerUIControler : MonoBehaviour
{
    [SerializeField] private GameEvent _onSwitchPanel;
    [SerializeField] private GameObject _pauseMenu;
    
    public void OnPannel()
    {
        _onSwitchPanel.Raise();
    }

    public void OnOpenMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}