using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public float playerHeight = 6;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = transform.position + new Vector3(0, playerHeight, 0);
        Camera.main.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
