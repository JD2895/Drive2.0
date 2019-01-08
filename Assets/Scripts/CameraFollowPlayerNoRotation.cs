using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerNoRotation : MonoBehaviour {
    public Transform target;

    private Vector3 thisPos;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        if (target)
        {
            thisPos.x = target.position.x;
            thisPos.y = target.position.y;
            thisPos.z = -10f;
            transform.position = thisPos;
        } else
        {
            transform.position = thisPos;
        }
    }
}
