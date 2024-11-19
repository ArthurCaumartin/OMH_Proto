using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OMH.QTE
{
    public class QTE : MonoBehaviour
    {
        public static QTE instance;
        void Awake() { if (instance) Destroy(gameObject); instance = this; }

        private Action<bool> _toDoInInput;
        private Action _inComplete;

        private int _currentIndex = 0;
        private List<Vector2> _currentDirectionSequence = new List<Vector2>();

        private QTEUI _QTEui;


        private void Start()
        {
            _QTEui = GetComponent<QTEUI>();
        }

        public void Play(Vector3 worldPosition, List<Vector2> directionSequence, Action<bool> toDoInInput = null, Action toDoInComplete = null)
        {
            _toDoInInput = toDoInInput;
            _inComplete = toDoInComplete;

            _currentDirectionSequence = directionSequence;

            _QTEui.ActivateUI(_currentDirectionSequence, worldPosition);

        }

        private void PlayInput(Vector2 inputDirection)
        {
            _toDoInInput.Invoke(true);
        }

        private void OnInputDirection(InputValue value)
        {
            Vector2 valueVector = value.Get<Vector2>();
            if (valueVector == Vector2.zero) return;
            PlayInput(valueVector);
        }
    }
}
