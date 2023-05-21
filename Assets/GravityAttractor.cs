using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    const float G = 667.4f;

    public Rigidbody rb;
    GravityAttractor[] attractors;
    Transform[] bodies;
    public bool special = false;
    public bool makeMatchRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attractors = FindObjectsOfType<GravityAttractor>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Attractor");
        bodies = new Transform[objs.Length];
        for(int i = 0; i < objs.Length; i++)
        {
            bodies[i] = objs[i].transform;
        }
    }

    public void Attract(GravityAttractor attractor, Transform body)
    {
        Rigidbody obj = attractor.rb;

        Vector3 dir = rb.position - obj.position;
        float dis = dir.magnitude;
        // perform calculation for force of gravity 
        float forceMagnitude = G * (rb.mass * obj.mass) / Mathf.Pow(dis, 2);
        // apply a force of (forceMag) in the direction
        Vector3 force = dir.normalized * -forceMagnitude;

        rb.AddForce(force);
        if (makeMatchRot)
        {
            Vector3 gravityUp = (body.position - transform.position).normalized;
            Vector3 bodyUp = body.up;

            Quaternion targetRot = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
            body.rotation = Quaternion.Slerp(body.rotation, targetRot, 50f * Time.deltaTime);
        }
    }
     
    void FixedUpdate()
    {
        for (int i = 0; i < attractors.Length; i++)
        {
            if(attractors[i] != this && !attractors[i].special)
            {
                Attract(attractors[i], bodies[i]);
            }
        }
        
    }

}
