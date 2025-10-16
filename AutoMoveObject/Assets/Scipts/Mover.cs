using System.Collections;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [SerializeField] GameObject dropCheck, jumpCheck;
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    RaycastHit hitFront, hitLeft, hitRight;
    [SerializeField] float forwardDist, sideDist;
    [SerializeField] float jumpForce;
    bool leftWall, rightWall, jumping;
    int randInt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = 10.0f;
        forwardDist = 1.0f;
        sideDist = 2.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Physics.BoxCast(transform.position + new Vector3(0, 1, 0), new Vector3(0.5f, 0.9f, 0.5f), transform.forward, out hitFront, Quaternion.identity, forwardDist))
        {
            transform.LookAt(transform.position - hitFront.normal);
            RotateAway();
        }
        if(transform.position.y < -0.4)//this wont work if the floor is varying heights
        {
            if(!Physics.BoxCast(dropCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
            {
                if (Physics.BoxCast(jumpCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
                {
                    Jump();
                }
                else
                {
                    RotateAway();
                }
            }
        }
        

    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.fixedDeltaTime);
    }

    void RotateAway()
    {
        if (Physics.BoxCast(transform.position + new Vector3(0, 1, 0), new Vector3(0.5f, 0.9f, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist))
        {

            leftWall = true;

        }

        if (Physics.BoxCast(transform.position + new Vector3(0, 1, 0), new Vector3(0.5f, 0.9f, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
        {

            rightWall = true;

        }

        if (leftWall && !rightWall)
        {

            transform.Rotate(Vector3.up, 90);
        }
        else if (!leftWall && rightWall)
        {

            transform.Rotate(Vector3.up, -90);
        }
        else if (leftWall && rightWall)
        {

            transform.Rotate(Vector3.up, 180);
        }
        else
        {
            randInt = Random.Range(0, 2);
            if (randInt == 0)
            {
                transform.Rotate(Vector3.up, 90);
                Debug.Log("RightRand");
            }
            else
            {
                transform.Rotate(Vector3.up, -90);
                Debug.Log("LeftRand");
            }
        }


        leftWall = false;
        rightWall = false;
    }
    void Jump()
    {
        rb.AddForce(new Vector3(0, 1, 0) * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
