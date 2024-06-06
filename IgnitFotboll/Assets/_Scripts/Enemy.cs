using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject origin;
    public GameObject ragdoll;

    private Rigidbody rb;
    private Vector3 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        //rb.AddForce(Vector3.forward * 50f, ForceMode.Impulse);
    }
    public void GetHit(float powerHit)
    {
        GameObject _ragdoll = Instantiate(ragdoll, transform.position, Quaternion.identity);
        origin.SetActive(false);
        _ragdoll.GetComponent<EnemyRagdoll>().GetHit(powerHit);
        Destroy(gameObject);
        // rb.AddForce(Vector3.forward * 30f, ForceMode.Impulse);
    }
    private void Update()
    {
        float distance = Vector3.Distance(startPos, transform.position);
        if(distance > 30)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
       // rb?.AddForce(Vector3.forward * 30, ForceMode.Impulse);
    }
}
