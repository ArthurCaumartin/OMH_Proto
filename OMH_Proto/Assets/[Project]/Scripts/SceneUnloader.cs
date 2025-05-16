using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUnloader : MonoBehaviour
{
    [SerializeField] private string _name;

    private IEnumerator Start()
    {
        Scene scene = SceneManager.GetSceneByName(_name);
        if (scene == null) print("No Scene");
        yield return new WaitForSeconds(2);
        // yield return null;
        if (scene != null)
        {
            print("Scene to unload : " + scene.buildIndex);
            SceneManager.UnloadSceneAsync(scene);
        }
    }
}