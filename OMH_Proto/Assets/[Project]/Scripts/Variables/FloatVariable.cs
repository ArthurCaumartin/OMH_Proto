using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NewFloat", menuName = "Variable/Float ✿ڿڰۣ——")]
public class FloatVariable : ScriptableObject
{
    public float Value;

    [HideInInspector] private UnityEvent<float> OnAddValue;

    public void Add(float toAdd)
    {
        Value += toAdd;
        OnAddValue.Invoke(toAdd);
    }
}

