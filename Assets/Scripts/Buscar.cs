using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buscar : MonoBehaviour
{
     

    // Update is called once per frame
    void Update()

    {
        if (transform.position != GameObject.Find("basee").transform.position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, GameObject.Find("basee").transform.position, 5 * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
    }
}
