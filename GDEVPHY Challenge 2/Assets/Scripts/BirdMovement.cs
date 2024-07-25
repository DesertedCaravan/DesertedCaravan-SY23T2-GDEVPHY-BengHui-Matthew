using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{
    [Header("Important Variables")]
    private Vector3 _currentAcceleration;
    [SerializeField] private float _accelerationFactor;

    private Vector3 _currentSpeed;
    [SerializeField] private float _topSpeed;

    private float _mass;
    [SerializeField] private float _gravity;

    private Vector3 _slingForce;

    // Friction Variables
    private float _c;
    private float _normal;
    private float _frictionMagnitude;

    [Header("Other Game Objects")]
    [SerializeField] private BlackHole blackHole;
    [SerializeField] private GameObject trailDotPrefab;

    // Bird State Variables
    private bool _dragging;
    private bool _birdReleased;
    private float _slingTimePassed;
    private float _timePassed;

    public float Mass { get { return _mass; } }
    public float Gravity { get { return _gravity; } }
    public bool Dragging { get { return _dragging; } }
    public bool BirdReleased { get { return _birdReleased; } }

    // Start is called before the first frame update
    void Start()
    {
        _currentAcceleration = new Vector3(0, 0, 0);
        _currentSpeed = new Vector3(0, 0, 0); // x is sideways, y is upwards
        _slingForce = new Vector3(0, 0, 0);

        _mass = 5.0f;

        _c = 5.0f;
        _normal = 1f;
        _frictionMagnitude = _c * _normal;

        _dragging = false;
        _birdReleased = false;

        _slingTimePassed = 0.0f;
        _timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_birdReleased == true)
        {
            // WIP
            /*
            _slingTimePassed += Time.deltaTime;
            
            if (_slingTimePassed <= 2f)
            {
                ApplyForce(_slingForce);
            }
            */
            
            // _slingForce /= 2.0f;

            // Apply Friction to spaceship by getting the opposite direction multiplied by the _frictionMagnitude
            // ApplyForce((_currentSpeed * -1).normalized * _frictionMagnitude);

            // Apply Gravity (Mass x Acceleration)
            ApplyForce(new Vector3(0, _mass * _gravity, 0));

            ApplyForce(blackHole.CalculateAttraction(this));

            BirdTransformUpdate();

            CheckFloor();

            _timePassed += Time.deltaTime;

            if (_timePassed > 0.01f && _currentSpeed.magnitude > 1.0f)
            {
                Instantiate(trailDotPrefab, this.transform.position, Quaternion.identity);
                _timePassed = 0f;
            }
        }
    }

    private void BirdTransformUpdate()
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
        _currentSpeed *= 0.9f;
    }

    private void CheckFloor()
    {
        if (this.transform.position.y < -3.354f)
        {
            this.transform.position = new Vector3(this.transform.position.x, -3.354f, this.transform.position.z);

            // Reverse direction of velocity
            _currentSpeed = new Vector3(_currentSpeed.x, -_currentSpeed.y, _currentSpeed.z);
        }

        if (this.transform.position.x > 9.45f)
        {
            this.transform.position = new Vector3(-9.45f, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -9.45f)
        {
            this.transform.position = new Vector3(9.45f, this.transform.position.y, this.transform.position.z);
        }
    }

    public void ApplyForce(Vector3 force)
    {
        // divide force by mass of bird
        Vector3 acceleration = force / _mass;

        // Increment _currentAcceleration by acceleration
        _currentAcceleration += acceleration;

        // Clamp _currentAcceleration to _accelerationFactor
        _currentAcceleration = Vector3.ClampMagnitude(_currentAcceleration, _accelerationFactor);
    }

    public void BirdRelease(Vector3 slingForce)
    {
        _slingForce = slingForce;
        _birdReleased = true;

        Debug.Log("old Sling Force: " + _slingForce);

        ApplyForce(_slingForce * 600.0f);

        Debug.Log("New Sling Force: " + (_slingForce * 600.0f));
    }

    public void BirdStop()
    {
        _currentSpeed *= 0;
    }

    private void OnMouseEnter()
    {
        _dragging = true;
    }

    private void OnMouseExit()
    {
        _dragging = false;
    }
}