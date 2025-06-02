using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class CheckerVolSlidersAssign : EditorWindow
{
    [MenuItem("Tools/Check if volume sliders are valid")]
    public static void ValidateSliders()
    {
        var sliders = GameObject.FindObjectsOfType<UpdateVolumeLevels>();
        int errors = 0;

        foreach (var updater in sliders)
        {
            if (updater.GetComponent<Slider>() == null)
            {
                Debug.LogError($"[CheckerVolSlidersAssign] Le GameObject '{updater.gameObject.name}' n'a pas de Slider.");
                errors++;
                continue;
            }

            var slider = updater.GetComponent<Slider>();
            var onValueChanged = slider.onValueChanged;
            bool hasUpdateVolume = false;

            foreach (var call in onValueChanged.GetPersistentEventCount().Enumerate())
            {
                if (onValueChanged.GetPersistentTarget(call) == updater &&
                    onValueChanged.GetPersistentMethodName(call) == "UpdateVolume")
                {
                    hasUpdateVolume = true;
                    break;
                }
            }

            if (!hasUpdateVolume)
            {
                Debug.LogError($"[CheckerVolSlidersAssign] Le slider de '{updater.gameObject.name}' n'appelle pas UpdateVolume() sur OnValueChanged.");
                errors++;
            }
        }

        if (errors == 0)
            Debug.Log($"[CheckerVolSlidersAssign] No error (meaning there's no slider volume assigned or they are correctly assigned).");
        else
            Debug.LogWarning($"[CheckerVolSlidersAssign] {errors} problème(s) détecté(s) dans la scène.");
    }
}

// Extension méthode utilitaire pour itérer proprement
public static class EventExtension
{
    public static System.Collections.Generic.IEnumerable<int> Enumerate(this int count)
    {
        for (int i = 0; i < count; i++)
            yield return i;
    }
}