using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDice : MonoBehaviour
{
    [Header("Dice properties")]
    public float rotationSpeed = 25.0f;
    public float rollSpeed = 10.0f;

    private bool stopped = false;
    private Quaternion desiredRotation;

    private void Update()
    {
        if (!stopped)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rollSpeed * Time.deltaTime);
        }
    }

    public void SetFaceUp(int faceNumber)
    {
        stopped = true;

        switch (faceNumber)
        {
            case 1:
                desiredRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                break;
            case 2:
                desiredRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case 3:
                desiredRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                break;
            case 4:
                desiredRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            case 5:
                desiredRotation = Quaternion.Euler(-180.0f, 0.0f, 0.0f);
                break;
            case 6:
                desiredRotation = Quaternion.Euler(90.0f, 0.0f, -90.0f);
                break;
        }
    }

    public void ResetDice()
    {
        stopped = false;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
