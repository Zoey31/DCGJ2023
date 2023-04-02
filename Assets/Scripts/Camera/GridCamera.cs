using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    MOVE,
    ROTATE,
    TURN_AROUND,
    FIGHT_IDLE,
    FIGHT_ENEMY,
    FIGHT_ATTACK,
    FIGHT_ENEMY_ATTACK
}

public class GridCamera : MonoBehaviour
{
    protected PlayerState state;
    protected Vector3 startMovePosition, endMovePosition;
    protected Quaternion startRotate, endRotate;
    protected float time;
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.IDLE;
    }

    void StartRotateBy(float angle)
    {
        time = 0;
        startRotate = transform.rotation;
        endRotate = Quaternion.Euler(startRotate.eulerAngles + new Vector3(0, angle, 0));
    }

    void HandleMovingControl()
    {
        if (Input.GetKeyDown(Settings.keyMapping.GO_FORWARD))
        {
            state = PlayerState.MOVE;
            StartMoveBy(Settings.gridSize);
        }
        else if (Input.GetKeyDown(Settings.keyMapping.TURN_LEFT))
        {
            state = PlayerState.ROTATE;
            StartRotateBy(-90);
        }
        else if (Input.GetKeyDown(Settings.keyMapping.TURN_RIGHT))
        {
            state = PlayerState.ROTATE;
            StartRotateBy(90);
        }
        else if (Input.GetKeyDown(Settings.keyMapping.TURN_AROUND))
        {
            state = PlayerState.TURN_AROUND;
            StartRotateBy(180);
        }
    }

    private void StartMoveBy(float gridSize)
    {
        startMovePosition = transform.position;
        endMovePosition = startMovePosition + transform.rotation * Vector3.forward * gridSize;
        time = 0;
    }

    void HandleFightControl()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (state == PlayerState.IDLE)
        {
            HandleMovingControl();
        }
        if (state == PlayerState.FIGHT_IDLE)
        {
            HandleFightControl();
        }
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case PlayerState.ROTATE:
            case PlayerState.TURN_AROUND:
                HandleRotate();
                break;
            case PlayerState.MOVE:
                HandleMove();
                break;
        }
    }

    private void HandleMove()
    {
        float speed = Settings.controlVariables.moveSpeed;
        transform.position = Vector3.Lerp(startMovePosition, endMovePosition, time * speed);

        if (time * speed >= 1.0f)
        {
            state = PlayerState.IDLE;
        }

        time += Time.deltaTime;
    }

    private void HandleRotate()
    {
        float speed = state == PlayerState.ROTATE ? Settings.controlVariables.rotateSpeed : Settings.controlVariables.turnAroundSpeed;

        transform.rotation = Quaternion.Lerp(startRotate, endRotate, time * speed);

        if (time * speed >= 1.0f)
        {
            state = PlayerState.IDLE;
        }

        time += Time.deltaTime;
    }
}
