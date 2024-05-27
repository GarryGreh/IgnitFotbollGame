using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 move;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        move = new Vector3(0.0f, 0.0f, 1.0f);
        characterController.Move(move * speed * Time.deltaTime);
    }
    public void SideMove(float _pos)
    {
        Debug.Log(_pos);
        move.x = _pos;
    }
}
