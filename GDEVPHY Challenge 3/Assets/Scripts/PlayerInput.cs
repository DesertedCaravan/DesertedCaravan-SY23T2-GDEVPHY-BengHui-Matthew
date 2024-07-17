using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private SpaceshipMovement ship;

    [SerializeField] private float _rotationSpeed;

    // Ideal _rotationSpeed = 0.25f;

    // Update is called once per frame
    void Update()
    {
        // GetKeyDown is for on press
        // GetKey is for holding down

        // Input API
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
        {
            ship.Upwards();

            Debug.Log("up");
            
            if (Input.GetKey(KeyCode.A))
            {
                ship.TurnSpaceshipDirection(_rotationSpeed);

                Debug.Log("left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ship.TurnSpaceshipDirection(-_rotationSpeed);

                Debug.Log("right");
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ship.TurnSpaceshipDirection(_rotationSpeed);

            Debug.Log("left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ship.TurnSpaceshipDirection(-_rotationSpeed);

            Debug.Log("right");
        }
    }
}
