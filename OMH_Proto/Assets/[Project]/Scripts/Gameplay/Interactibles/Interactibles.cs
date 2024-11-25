using System;
using System.Collections;
using System.Collections.Generic;
using OMH.QTE;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Interactibles : MonoBehaviour
{
    [SerializeField] private GameEvent _onEnterInteractibleRange, _onExitInteractibleRange;

    [SerializeField] protected InfosManager _infosManager;

    protected bool _isPlayerInRange;

    private void Reset()
    {
        BoxCollider interactRange = GetComponent<BoxCollider>();
        interactRange.size = new Vector3(7, 1, 7);
        interactRange.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        // print("InRange");
        _onEnterInteractibleRange.Raise();
        _isPlayerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        // print("NotInRange");
        _onExitInteractibleRange.Raise();
        _isPlayerInRange = false;
    }

    public virtual void Interact()
    {
        QTE.instance.Play(transform.position, QTESequence.RandomSequence(10), (isGoodAction) =>
        {
            print(isGoodAction ? "Good Action" : "Bad Action");
        },
        () =>
        {
            print("QTE done :)");
        });
    }

    public void A(bool value)
    {
        
    }
}
