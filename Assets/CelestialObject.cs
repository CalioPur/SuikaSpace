using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObject : MonoBehaviour
{
    private Rigidbody body;
    [SerializeField] private Vector3 initVelocity;
    [SerializeField]  private bool applyInitVelocityOnStart;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(applyInitVelocityOnStart) body.AddForce(initVelocity, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        AttractPlanets.planets.Remove(body);
    }

    private void OnEnable()
    {
        if(!AttractPlanets.planets.Contains(body)) AttractPlanets.planets.Add(body);
    }
}
