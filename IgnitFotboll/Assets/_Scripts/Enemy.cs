using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject origin;
    public GameObject ragdoll;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    private void FixedUpdate()
    {
       // rb?.AddForce(Vector3.forward * 30, ForceMode.Impulse);
    }
}
