using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    private static GameSettings instance;
    public KeyMapping keyMapping = new();
    public ControlVariables controlVariables = new();
    public float playerHeight = 6.0f;
    public WorldVariables worldVariables = new();
    public Tags tags = new();

    public static GameSettings Instance
    {
        get { return instance ??= new GameObject("GameSettings").AddComponent<GameSettings>(); }
    }

}