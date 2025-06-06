using UnityEngine;

public class AudioEmitterRegistration : MonoBehaviour
{

    /// <summary>
    /// Script nécessaire sur prefab des mobs en masse pour faire marcher le max within radius manager.
    /// Au spawn du prefab : auto-enregistrement.    À la destruction ou désactivation : retrait du système
    /// </summary>

    public string groupName;
    public AK.Wwise.RTPC spreadRTPC;

    private void OnEnable()
    {
        var manager = MaxWithinRadiusManager.Instance;
        if (manager == null) return;

        var group = manager.groups.Find(g => g.groupName == groupName);
        if (group == null)
        {
            Debug.LogWarning($"Group {groupName} not found for emitter {gameObject.name}.");
            return;
        }

        MaxWithinRadiusManager.AudioEmitter emitter = new MaxWithinRadiusManager.AudioEmitter
        {
            emitterObject = this.gameObject,
            spreadRTPC = spreadRTPC,
            spreadValue = 1.0f
        };

        group.emitters.Add(emitter);
    }

    private void OnDisable()
    {
        var manager = MaxWithinRadiusManager.Instance;
        if (manager == null) return;

        var group = manager.groups.Find(g => g.groupName == groupName);
        if (group == null) return;

        group.emitters.RemoveAll(e => e.emitterObject == this.gameObject);
    }
}
