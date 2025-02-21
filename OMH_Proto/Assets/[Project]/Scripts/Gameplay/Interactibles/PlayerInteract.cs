using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private QTEControler _qteControler;
    [SerializeField] private LayerMask _interactibleLayer;
    [SerializeField] private float _detectionRange = 5;
    [SerializeField] private float _detectionPerSecond = 2;
    [SerializeField] private GameEvent _inRangeEvent, _notInRangeEvent;
    private float _detectionTime;
    private Interactible _nearestInteractible;

    private void Update()
    {
        _detectionTime += Time.deltaTime;
        if (_detectionTime > 1 / _detectionPerSecond)
        {
            _detectionTime = 0;
            _nearestInteractible = GetNearestInteractible();
        }
    }

    public Interactible GetNearestInteractible()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, _detectionRange, _interactibleLayer);
        float minDist = Mathf.Infinity;
        Interactible nearest = null;

        for (int i = 0; i < col.Length; i++)
        {
            Interactible inter = col[i].GetComponent<Interactible>();
            if (!inter) continue;

            float currentDist = (inter.transform.position - transform.position).sqrMagnitude;
            if (currentDist < minDist)
            {
                minDist = (inter.transform.position - transform.position).sqrMagnitude;
                nearest = inter;
            }
        }
        if (nearest == null) _notInRangeEvent.Raise();
        else
        {
            if (nearest is InteractibleMetal)
            {
                if (!((InteractibleMetal)nearest)._isGeneratorActivated) _inRangeEvent.Raise();
            }
            else
            {
                _inRangeEvent.Raise();
            }
        }
        return nearest;
    }

    public void OnInteract()
    {
        if (!_nearestInteractible) return;

        if (_nearestInteractible.HaveQTE)
        {
            if (_nearestInteractible.QTE.IsRuning) return;

            _nearestInteractible.Interact(this, out bool cancelInteraction);
            if (cancelInteraction) return;
            _qteControler.PlayQTE(_nearestInteractible.QTE);
            return;
        }

        _nearestInteractible.Interact(this, out bool notUse);
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(0, 1, 0, .2f);
        Gizmos.DrawSphere(transform.position, _detectionRange);
    }
}
