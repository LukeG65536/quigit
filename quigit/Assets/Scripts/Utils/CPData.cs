using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPData : MonoBehaviour
{
    public Transform cpPos;
    public CPData nextCp;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cpPos = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coll happened");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("new cp set-------------------------------------");
            other.gameObject.GetComponent<CPManager>().currentCP = this;
        }
    }
}
