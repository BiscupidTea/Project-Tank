using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public void Log(string LogCode, string tag)
    {
        Debug.Log(LogCode);
    }

    public void LogError(string ErrorCode, string tag)
    {
        Debug.LogError(ErrorCode);
    }

    public void LogWarning(string WarningCode, string tag)
    {
        Debug.LogWarning(WarningCode);
    }

    public void DrawLine(Vector3 start, Vector3 end, Color color, string tag)
    {
        Debug.DrawLine(start, end, color);
    }
    public void DrawRay(Vector3 start, Vector3 direction, Color color, string tag)
    {
        Debug.DrawRay(start, direction, color);
    }
}
