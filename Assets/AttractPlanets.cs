using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //forces sun to have rigidbody
public class AttractPlanets : MonoBehaviour
{
    [SerializeField]private float gravity = 1;

    // we could make a list of attractors, but right now, only the primary sun is one
    private Rigidbody sun;
    public static List<Rigidbody> planets = new(); // list of planets (attracted)

    public bool isSimulating; //do we want to simulate gravity right now ?

    private void Awake()
    {
        sun = GetComponent<Rigidbody>(); // fetch and cache sun's rigidbody 
    }

    private void FixedUpdate()
    {
        if (isSimulating)
        {
            SimulateGravity();
        }   
    }

    public void SimulateGravity()
    {
        foreach (Rigidbody body in planets)
        {
            AddGravitationalForce(sun, body);
        }
    }

    public void AddGravitationalForce(Rigidbody attractor, Rigidbody attracted)
    {
        float massProduct = attractor.mass * attracted.mass * gravity;

        Vector3 difference = attractor.position - attracted.position;
        float distance = Vector3.Distance(attractor.position, attracted.position);

        // Force = gravity * (( mass 1 * mass 2 ) / distance^2)
        float scaledForce = gravity * (massProduct / distance*distance);

        Vector3 forceDirection = Vector3.Normalize(difference);
        Vector3 forceVector = forceDirection * scaledForce;

        attracted.AddForce(forceVector); //makes our target move closer to the attractor

    }
}
