using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] trash;

    public Transform planet;



    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);
        int index = Random.Range(0, trash.Length);
        GameObject g = Instantiate(trash[index], planet.position + (Random.onUnitSphere * Random.Range(95f, 130f)), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        // randomize size 1x - 5x
        float multi = Random.Range(1f, 3f);
        Transform t = g.transform;
        g.transform.localScale = new Vector3(t.localScale.x + t.localScale.x * multi, t.localScale.y + t.localScale.y * multi, t.localScale.z + t.localScale.z * multi);
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.AddForce(Random.onUnitSphere * Random.Range(2f, 7.5f), ForceMode.Impulse);

        rb.mass = t.localScale.x;

        StartCoroutine(Spawn());
    }
}
