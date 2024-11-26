using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTESequence
{
    public List<Vector2> inputs;

    public static List<Vector2> RandomSequence(int size)
    {
        List<Vector2> vectors = new List<Vector2>();
        for (int i = 0; i < size; i++)
        {
            Vector2 newV = Random.insideUnitCircle;
            newV.x = (newV.x >= 0f) ? 1f : -1f;
            newV.y = (newV.y >= 0f) ? 1f : -1f;

            if (Random.value > .5f)
                newV.x = 0;
            else
                newV.y = 0;

            vectors.Add(newV);
        }

        return vectors;
    }
}
