using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    protected GameObject gameObject;

    public Field(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public bool IsCollision()
    {
        try
        {
            return gameObject.name.Contains("Wall");
        }
        catch
        {
            return false;
        }
    }
}
public class GridIndex
{
    public int x;
    public int y;
    public GridIndex(Vector3 worldPosition, Bounds bounds)
    {
        x = GetIndexFromWorld(worldPosition.x - bounds.min.x);// - (GetIndexFromWorld(worldPosition.x) - GetIndexFromWorld(bounds.min.x));
        y = GetIndexFromWorld(worldPosition.z - bounds.min.z);
    }


    public static int GetIndexFromWorld(float x)
    {
        return ((int)x) / ((int)GameSettings.Instance.worldVariables.gridSize);
    }
}


public class World : MonoBehaviour
{
    protected static World instance;
    protected uint width, height;
    protected Field[,] fields;
    protected Bounds worldBounds;

    public void CalculateSize(GameObject[] fieldGameObjects)
    {
        width = 0;
        height = 0;
        worldBounds = new();

        foreach (var field in fieldGameObjects)
        {
            worldBounds.Encapsulate(field.transform.position);
        }

        width = (uint) GridIndex.GetIndexFromWorld(worldBounds.max.x - worldBounds.min.x) + 1;
        height = (uint) GridIndex.GetIndexFromWorld(worldBounds.max.z - worldBounds.min.z) + 1;
    }

    public static World Instance
    {
        get { return instance ??= new GameObject("World").AddComponent<World>(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allFieldGameObjects = GameObject.FindGameObjectsWithTag(GameSettings.Instance.tags.field);

        CalculateSize(allFieldGameObjects);
        GenerateMap(allFieldGameObjects);
    }

    private void GenerateMap(GameObject[] allFieldGameObjects)
    {
        fields = new Field[height, width];

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                fields[i, j] = new Field(default);
            }
        }

        foreach (var fieldGameObject in allFieldGameObjects)
        {
            GridIndex gridIndex = new(fieldGameObject.transform.position, worldBounds);
            fields[gridIndex.y, gridIndex.x] = new Field(fieldGameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal bool CheckCollision(Vector3 worldPosition, Vector3 current)
    {
        GridIndex gridIndex = new(worldPosition, worldBounds);

        try
        {
            return fields[gridIndex.y, gridIndex.x].IsCollision();
        }
        catch
        {
            return true;
        }
    }
}
