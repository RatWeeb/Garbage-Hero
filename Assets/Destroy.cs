using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void awake()
    {
        Destroy(gameObject, 20f);
    }


}
