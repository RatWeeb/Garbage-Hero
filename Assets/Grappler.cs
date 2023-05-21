using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Grappler : MonoBehaviour
{
    public Transform[] firePoints;
    public Transform shootPoint;
    public LineRenderer[] lines;
    public LayerMask trashLayer;
    public float grappleSpeed;
    public AudioSource grappleSound;
    public AudioSource crushSfx;
    public AudioSource zipSfx;
    public int maxMass;

    private bool rightGrapple, leftGrapple;
    private Transform leftObj, rightObj;
    private Rigidbody[] trashRb = new Rigidbody[2];
    private Score score;


    // Start is called before the first frame update
    void Start()
    {
        lines[0].enabled = false;
        lines[1].enabled = false;
        score = GameObject.Find("GameManager").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Grapple("left");
        }
        else if(!Input.GetButton("Fire1") || leftObj == null)
        {
            if (leftGrapple)
            {
                leftGrapple = false;
                lines[0].enabled = false;
                zipSfx.Stop();
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Grapple("right");
        }
        else if(!Input.GetButton("Fire2") || rightObj == null)
        {
            if (rightGrapple)
            {
                rightGrapple = false;
                lines[1].enabled = false;
                zipSfx.Stop();
            }
        }
        MoveTowards();
        UpdateLines();

    }

    void UpdateLines()
    {
        lines[0].SetPosition(0, firePoints[1].position);
        lines[1].SetPosition(0, firePoints[0].position);
    }

    void MoveTowards()
    {
        if (leftGrapple)
        {
            FaceToward(leftObj);
            lines[0].SetPosition(1, leftObj.position);
            Move(0, leftObj);
        }
        if(rightGrapple)
        {
            FaceToward(rightObj);
            lines[1].SetPosition(1, rightObj.position);
            Move(1, rightObj);
        }
    }

    void Move(int index, Transform obj)
    {
        if (trashRb[index] != null)
        {
            float massScalingFactor = 1f - (trashRb[index].mass / maxMass); // Assuming maxMass is the maximum mass value you want to use for scaling
            float adjustedSpeed = grappleSpeed * massScalingFactor;
            trashRb[index].MovePosition(trashRb[index].position + obj.forward * adjustedSpeed * Time.deltaTime);

        }
    }

    void FaceToward(Transform d)
    {
        zipSfx.Play();
        Vector3 dir = new Vector3(transform.position.x - d.position.x, transform.position.y - d.position.y, transform.position.z - d.position.z);

        Vector3 newDirection = Vector3.RotateTowards(d.forward, dir, 0.005f, 0.0f);

        d.rotation = Quaternion.LookRotation(newDirection);
    }

    void Grapple(string dir)
    {

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 1000,trashLayer))
        {

            grappleSound.Play();
            if (dir == "left")
            {
                leftObj = hit.transform;
                leftGrapple = true;
                trashRb[0] = hit.transform.gameObject.GetComponent<Rigidbody>();
                lines[0].enabled = true;
                
            }
            else
            {
                rightObj = hit.transform;
                rightGrapple = true;
                trashRb[1] = hit.transform.gameObject.GetComponent<Rigidbody>();
                lines[1].enabled = true;
            }

        }


    }

    // grapple on and then make it face you

    // apply a constant movement to it

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.layer == LayerMask.NameToLayer("Trash"))
        {
            Destroy(c.gameObject,0f);
            score.score++;
            score.timeLeft += 5;
            score.UpdateScore();
            score.UpdateTime();
            crushSfx.Play();
        }
    }

}
