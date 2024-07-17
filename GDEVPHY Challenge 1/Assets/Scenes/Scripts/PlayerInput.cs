using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CarMovement car;

    [SerializeField] private float _turningRate;

    // Update is called once per frame
    void Update()
    {
        // GetKeyDown is for on press
        // GetKey is for holding down

        // Input API
        if (Input.GetKey(KeyCode.W))
        {
            car.Forwards();

            Debug.Log("up");

            if (Input.GetKey(KeyCode.A))
            {
                car.TurnCarDirection(-_turningRate);

                Debug.Log("left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                car.TurnCarDirection(_turningRate);

                Debug.Log("right");
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            car.Backwards();

            Debug.Log("down");

            if (Input.GetKey(KeyCode.A))
            {
                car.TurnCarDirection(-_turningRate);

                Debug.Log("left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                car.TurnCarDirection(_turningRate);

                Debug.Log("right");
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            car.TurnCarDirection(-_turningRate / 2);

            Debug.Log("left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            car.TurnCarDirection(_turningRate / 2);

            Debug.Log("right");
        }
    }
}