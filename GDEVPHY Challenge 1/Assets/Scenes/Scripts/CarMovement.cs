using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // Movement Variables
    private Vector3 _currentAcceleration;
    [SerializeField] private float _accelerationFactor;

    private Vector3 _currentSpeed;
    [SerializeField] private float _topSpeed;

    // Friction Variables
    private float _c;
    private float _normal;
    private float _frictionMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        _currentAcceleration = new Vector3(0, 0, 0);
        _currentSpeed = new Vector3(0, 0, 0); // x is sideways, z is forward

        _c = 0.20f;
        _normal = 1f;
        _frictionMagnitude = _c * _normal;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply Friction to car by getting the opposite direction multiplied by the _frictionMagnitude
        ApplyForce(-_currentSpeed.normalized * _frictionMagnitude);
        CarTransformUpdate();
    }

    private void CarTransformUpdate()
    {
        // Increment _currentSpeed by _currentAcceleration
        _currentSpeed = _currentSpeed + _currentAcceleration;

        // Clamp _currentSpeed to _topSpeed
        _currentSpeed = Vector3.ClampMagnitude(_currentSpeed, _topSpeed);

        // Increment car position by _currentSpeed
        this.transform.position = this.transform.position + new Vector3(_currentSpeed.x * Time.deltaTime, _currentSpeed.y * Time.deltaTime, _currentSpeed.z * Time.deltaTime);

        // _currentAcceleration should be reset after each calculation has been made
        _currentAcceleration *= 0.0f;
    }

    public void Forwards()
    {
        // Normalize force then multiply by _accelerationFactor
        ApplyForce(transform.forward.normalized * _accelerationFactor);
    }

    public void Backwards()
    {
        // Normalize force then multiply by _accelerationFactor
        ApplyForce(-transform.forward.normalized * _accelerationFactor);
    }

    private void ApplyForce(Vector3 force)
    {
        // Increment _currentAcceleration by force
        _currentAcceleration += force;

        // Clamp _currentAcceleration to _accelerationFactor
        _currentAcceleration = Vector3.ClampMagnitude(_currentAcceleration, _accelerationFactor);
    }

    public void TurnCarDirection(float angle)
    {
        // CarRotateUpdate (angle should be place in the y variable)
        this.transform.Rotate(transform.rotation.x, angle, transform.rotation.z, Space.World);
    }
}