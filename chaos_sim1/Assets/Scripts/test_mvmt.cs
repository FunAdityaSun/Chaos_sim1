using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_mvmt : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = new Vector3(1, 0, 0);
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Rigidbody not found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
