using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    // Space Background Sprite Link: https://spritefx.blogspot.com/2013/04/sprite-background-space.html
    // Spaceship Sprite Link: https://mungfali.com/post/DD7EDD95A53E6B4888CD27AC2CF2CCC3E060CC8E

    // Movement Variables
    private Vector3 _currentAcceleration;
    [SerializeField] private float _accelerationFactor;

    private Vector3 _currentSpeed;
    [SerializeField] private float _topSpeed;

    // Friction Variables
    private float _c;
    private float _normal;
    private float _frictionMagnitude;

    private float _width;
    private float _height;

    // Start is called before the first frame update
    void Start()
    {
        _currentAcceleration = new Vector3(0, 0, 0);
        _currentSpeed = new Vector3(0, 0, 0); // x is sideways, y is upwards

        // Ideal _accelerationFactor = 0.05f
        // Ideal _topSpeed = 5.0f;

        _c = 0.003f;
        _normal = 1f;
        _frictionMagnitude = _c * _normal;

        _width = 9.45f;
        _height = 5.6f;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply Friction to spaceship by getting the opposite direction multiplied by the _frictionMagnitude
        ApplyForce(-_currentSpeed.normalized * _frictionMagnitude);
        SpaceshipTransformUpdate();
        CheckEdges();
    }

    private void SpaceshipTransformUpdate()
    {
        // Increment _currentSpeed by _currentAcceleration
        _currentSpeed = _currentSpeed + _currentAcceleration;

        // Clamp _currentSpeed to _topSpeed
        _currentSpeed = Vector2.ClampMagnitude(_currentSpeed, _topSpeed);

        // Increment car position by _currentSpeed
        this.transform.position = this.transform.position + new Vector3(_currentSpeed.x * Time.deltaTime, _currentSpeed.y * Time.deltaTime, _currentSpeed.z * Time.deltaTime);

        // _currentAcceleration should be reset after each calculation has been made
        _currentAcceleration *= 0.0f;

        // slowly reduce _currentSpeed across each succeeding calculation (alternative to ApplyForce(-_currentSpeed.normalized * _frictionMagnitude);)
        // _currentSpeed *= 0.996f;
    }

    private void CheckEdges()
    {
        if (this.transform.position.x > _width)
        {
            this.transform.position = new Vector3(-_width, this.transform.position.y, this.transform.position.z);
        }
        else if(this.transform.position.x < -_width)
        {
            this.transform.position = new Vector3(_width, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y > _height)
        {
            this.transform.position = new Vector3(this.transform.position.x, -_height, this.transform.position.z);
        }
        else if (this.transform.position.y < -_height)
        {
            this.transform.position = new Vector3(this.transform.position.x, _height, this.transform.position.z);
        }
    }

    public void Upwards()
    {
        // Normalize force then multiply by _accelerationFactor
        ApplyForce(transform.up.normalized * _accelerationFactor);
    }

    private void ApplyForce(Vector3 force)
    {
        // Increment _currentAcceleration by force
        _currentAcceleration += force;

        // Clamp _currentAcceleration to _accelerationFactor
        _currentAcceleration = Vector3.ClampMagnitude(_currentAcceleration, _accelerationFactor);
    }

    public void TurnSpaceshipDirection(float angle)
    {
        // SpaceshipRotateUpdate (angle should be place in the z variable)
        this.transform.Rotate(transform.rotation.x, transform.rotation.y, angle, Space.World);
    }
}
