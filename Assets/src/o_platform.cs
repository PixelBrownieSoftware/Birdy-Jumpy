using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_platform : MonoBehaviour
{
    public GameObject go;

    private void Update()
    {
        go.transform.position += Vector3.forward * 20 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<o_player>())
        {
            print("Here we go!");
            other.transform.parent = go.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
