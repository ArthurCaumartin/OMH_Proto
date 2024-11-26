using UnityEngine;

public class PlayerUIControler : MonoBehaviour
{
    [SerializeField] private GameEvent _onSwitchPanel;
    
    public void OnPannel()
    {
        _onSwitchPanel.Raise();
    }
}