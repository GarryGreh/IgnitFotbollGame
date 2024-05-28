using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 follow = new Vector3(transform.position.x, transform.position.y, target.position.z - 13);
        transform.position = follow;
    }
}
