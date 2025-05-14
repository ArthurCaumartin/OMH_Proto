using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioDebugLog
{
    public static void LogAudio(string scriptOriginName, string gameObjectOriginName = "", string content = "")
    {
        Debug.Log($"<color=orange>AUDIO</color> | CS script={scriptOriginName}, GameObject= {gameObjectOriginName} : {content}");
    }
}
