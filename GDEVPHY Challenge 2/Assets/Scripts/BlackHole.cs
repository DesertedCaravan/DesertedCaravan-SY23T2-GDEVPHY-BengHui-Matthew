using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("Important Variables")]
    [SerializeField] private float _minDistOfAttraction;
    [SerializeField] private float _maxDistOfAttraction;
    [SerializeField] private float _maxAttraction;

    private float _mass;

    // Ideal _minDistOfAttraction: 1.0f
    // Ideal _maxDistOfAttraction: 10.0f
    // Ideal _maxAttraction: 5.0f

    // Start is called before the first frame update
    void Start()
    {
        _mass = 10.0f;
    }

    public Vector3 CalculateAttraction(BirdMovement bird)
    {
        Vector3 attractionDirection = this.transform.position - bird.transform.position;
        float attractionDistance = attractionDirection.magnitude;
        attractionDistance = Mathf.Clamp(attractionDistance, _minDistOfAttraction, _maxDistOfAttraction);

        float strength = (_maxAttraction * bird.Mass * _mass) / (attractionDistance * attractionDistance);
        return attractionDirection.normalized * strength;
    }

}
