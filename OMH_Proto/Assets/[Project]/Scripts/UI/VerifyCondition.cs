using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerifyCondition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _componentToVerify;
    
    public void Verify()
    {
        if (_componentToVerify.enabled)
        {
            _componentToVerify.enabled = false;
        }
    }
}
