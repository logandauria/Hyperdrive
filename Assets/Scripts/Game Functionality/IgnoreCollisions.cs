using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Give an object the ability to ignore collisions that aren't player related. Primarily for use by the steering wheel and portal button
public class IgnoreCollisions : MonoBehaviour
{

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        // If not player hand, ignore collision
        if(col.gameObject.tag != "PlayerHand")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), col.gameObject.GetComponent<Collider>(), true);
            Debug.Log("collision ignored");
        }
        else
        {
            // negate y position freeze if colliding with hand
            rb.constraints = ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        Debug.Log("collision");
    }

    void OnCollisionExit(Collision col)
    {
        // freeze all local positions when not interacted with
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
