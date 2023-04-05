using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = transform.position + new Vector3(0, GameSettings.Instance.playerHeight, 0);
        Camera.main.transform.rotation = transform.rotation;

        this.gameObject.SetActive(false);

    }
}
