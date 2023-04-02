using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    private static Settings instance;
    public static KeyMapping keyMapping = new();
    public static ControlVariables controlVariables = new();
    public static float playerHeight = 6.0f;
    public static float gridSize = 10.0f;

    public static Settings Instance
    {
        get { return instance ??= new GameObject("Settings").AddComponent<Settings>(); }
    }

}