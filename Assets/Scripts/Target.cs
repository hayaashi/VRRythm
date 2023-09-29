using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Target : MonoBehaviour
{
    public Vector3 startPoint;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -3f);
    }


    Vector3 entry;
    Vector3 exit;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.tag == "OnDestroy")
        {
            if (i == 0)
            {
                Vector3 b = new Vector3(startPoint.x + Random.Range(-0.5f, 0.5f), startPoint.y, startPoint.z);
                Instantiate(this.gameObject,
                    b, Quaternion.identity);
                Destroy(this.gameObject);
                i++;
            }
        }
        //entry = other.ClosestPointOnBounds(this.transform.position);
        */
    }
    
    private void OnTriggerExit(Collider other)
    {
        /*
        exit = other.ClosestPointOnBounds(this.transform.position);
        Vector3 slice = exit - entry;

        Debug.Log("sliced");
        this.gameObject.SliceInstantiate
        (
            position: entry,
            direction: slice
        );
        */
        /*
        if (i == 0)
        {
            Vector3 b = new Vector3(startPoint.x + Random.Range(-0.5f, 0.5f), startPoint.y, startPoint.z);
            Instantiate(this.gameObject,
                b, Quaternion.identity);
            Destroy(this.gameObject);
            i++;
        }
        */
    }
}
