using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyRagdoll : MonoBehaviour
{
    public Rigidbody[] allRb;

    private float timer = 5.0f;
    public void GetHit(float powerHit)
    {      
        foreach (var rb in allRb)
        {
            rb.AddForce(Vector3.forward * powerHit, ForceMode.Impulse);
        }
        //rb?.AddForce(Vector3.forward * powerHit, ForceMode.Impulse);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
