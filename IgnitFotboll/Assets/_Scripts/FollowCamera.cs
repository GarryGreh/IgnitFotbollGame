using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    private Vector2 startPos;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButton(0))
        {
            float pos = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
            Debug.Log("startPos: " + startPos);
            Debug.Log("mousePos: " + cam.ScreenToWorldPoint(Input.mousePosition).x);
            target.GetComponent<PlayerController>().SideMove(pos);
        }
    }
    private void LateUpdate()
    {
        Vector3 follow = new Vector3(transform.position.x, transform.position.y, target.position.z - 13);
        transform.position = follow;
    }
}
