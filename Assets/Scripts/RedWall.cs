using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWall : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
