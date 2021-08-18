using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [Header("Push Settings")]

    [Range(-10.0f, 100.0f)]
    public float PushPower = 10.0f;
    [Range(-10.0f, 10.0f)]
    public float PushDistance = 2.8f;
    public Animator animator;
    Vector3 Direction;
    private bool ObjectMoving = false;

    private bool CanPush = false;


    void Start()
    {

    }

    void Update()
    {
        PushAction();
    }

    private void PushAction()
    {
        // if object moving and player is pressing F key, animation will start.
        if (Input.GetKey(KeyCode.F))
        {
            CanPush = true;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            CanPush = false;
            ObjectMoving = false;
            animator.SetBool("pushing", false); // deactivating the push animation
        }

        if (ObjectMoving)
        {
            animator.SetBool("pushing", true); // activating the push animation
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody Body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (Body == null || Body.tag != "Pushable") { return; } // check tag

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3) { return; }

        // object won't rotate
        Body.GetComponent<Rigidbody>().freezeRotation = true;

        // calculating player between object distance
        float Dist = Vector3.Distance(Body.gameObject.transform.position, this.transform.position);
        // Debug.Log(Dist);

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 PushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);


        // Apply the push
        if (CanPush && Dist < PushDistance)
        {
            Body.isKinematic = false;
            Push.ApplyForceToReachVelocity(Body, PushDirection.normalized * PushPower, 0.1f);
            ObjectMoving = true;
        }
        else
        {
            ObjectMoving = false;
            Body.isKinematic = true;
            Body.GetComponent<Rigidbody>().freezeRotation = false;
        }

        // if object is moving, it is calling to the animation.
        if (Body.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {
            ObjectMoving = true;
        }
        else
        {
            ObjectMoving = false;
        }
    }


    // physics library Source: https://gist.github.com/ditzel/1f207c838f0023fcbd34c5c67955fd25
    private static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
