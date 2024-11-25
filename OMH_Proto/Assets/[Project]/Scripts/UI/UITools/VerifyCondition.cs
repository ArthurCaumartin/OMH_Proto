using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerifyCondition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _componentToVerify;
    
    public void Verify()
    {
        print(_componentToVerify.enabled);
        if (_componentToVerify.enabled)
        {
            _componentToVerify.enabled = false;
        }
    }
}
