using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Action<Vector2> OnJoystickMove;

    //public Joystick moveJoystick;
    public float moveSpeed;
    public float rotationSpeed;
    public Transform transformRig;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }
}
