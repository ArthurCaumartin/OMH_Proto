using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactUI : MonoBehaviour
{
    [SerializeField] private Image _artifactImage;

    public void GainArtifact()
    {
        print("ArtifactUI");
        _artifactImage.color = new Color(1,1,1,1);
    }
}
