using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileReplacer : MonoBehaviour
{
    public void RoundPosition()
    {
        Transform[] tList = GetComponentsInChildren<Transform>();
        // Undo.RecordObjects(tList, "tilePos");
        for (int i = 0; i < tList.Length; i++)
        {
            if (tList[i] == transform) continue;
            // float x = Mathf.Round(_tiles[i].localPosition.x);
            // float y = Mathf.Round(_tiles[i].localPosition.y);
            // float z = Mathf.Round(_tiles[i].localPosition.z);

            float x = RoundToNearestHalf(tList[i].localPosition.x);
            float y = RoundToNearestHalf(tList[i].localPosition.y);
            float z = RoundToNearestHalf(tList[i].localPosition.z);

            tList[i].localPosition = new Vector3((float)x, (float)y, (float)z);
        }
    }

    public float RoundToNearestHalf(float a)
    {
        return a = Mathf.Round(a * 2f) * 0.5f;
    }
}
