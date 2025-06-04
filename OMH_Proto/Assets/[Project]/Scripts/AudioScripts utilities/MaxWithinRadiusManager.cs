using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class MaxWithinRadiusManager : MonoBehaviour
{
    /// <summary>
    /// Gestion opti des sources audio par groupes suivant le principe MaxWithinRadius.
    /// Limite deux sources proches par groupe, et une seule pour les groupes loins.
    /// Spread via RTPC Wwise (TODO)
    /// </summary>
    public static MaxWithinRadiusManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // emitters audio group
    [System.Serializable]
    public class AudioEmitterGroup
    {
        public string groupName;
        public List<AudioEmitter> emitters = new List<AudioEmitter>();
        public float closeRange = 15f; //voir mesures avec progs
        public float farRange = 50f;
    }

    // emitter audio alone
    [System.Serializable]
    public class AudioEmitter
    {
        public GameObject emitterObject;
        public RTPC spreadRTPC;
        public float spreadValue;
    }

    public List<AudioEmitterGroup> groups = new List<AudioEmitterGroup>();

    public Transform listener; // listener AK et pas Unity !!

    private void Update()
    {
        ProcessAudioEmitters();
    }

    /// <summary>
    /// Update des emitters audio en fonction de leur distance (MaxWithinRadius).
    /// </summary>
    private void ProcessAudioEmitters()
    {
        foreach (var group in groups)
        {
            List<(AudioEmitter emitter, float distance)> emitterDistances = new List<(AudioEmitter, float)>();

            // Calcul des distances pour chaque emitter du groupe
            foreach (var emitter in group.emitters)
            {
                float distance = Vector3.Distance(listener.position, emitter.emitterObject.transform.position);
                emitterDistances.Add((emitter, distance));
            }

            // Tri des emitters par distance croissante
            emitterDistances.Sort((a, b) => a.distance.CompareTo(b.distance));

            int emitterLimit = 0;

            // Emitters near
            foreach (var ed in emitterDistances)
            {
                if (ed.distance <= group.closeRange)
                {
                    if (emitterLimit < 2)
                    {
                        EnableEmitter(ed.emitter, 1.0f); // Spread maximal
                        emitterLimit++;
                    }
                    else
                    {
                        VirtualizeEmitter(ed.emitter);
                    }
                }
                // Emitters far
                else if (ed.distance <= group.farRange)
                {
                    if (emitterLimit < 3) // 2 proches + 1 lointain
                    {
                        EnableEmitter(ed.emitter, 0.5f); // Spread mini
                        emitterLimit++;
                    }
                    else
                    {
                        VirtualizeEmitter(ed.emitter);
                    }
                }
                else
                {
                    VirtualizeEmitter(ed.emitter);
                }
            }
        }
    }

    /// <summary>
    /// Active l'emitter et applique le spread correspondant via RTPC.
    /// </summary>
    private void EnableEmitter(AudioEmitter emitter, float spreadValue)
    {
        if (!emitter.emitterObject.activeSelf)
            emitter.emitterObject.SetActive(true);

        emitter.spreadRTPC.SetValue(emitter.emitterObject, spreadValue);
    }

    /// <summary>
    /// Virtualise l'émetteur en le désactivant.
    /// </summary>
    private void VirtualizeEmitter(AudioEmitter emitter)
    {
        if (emitter.emitterObject.activeSelf)
            emitter.emitterObject.SetActive(false);
    }
}