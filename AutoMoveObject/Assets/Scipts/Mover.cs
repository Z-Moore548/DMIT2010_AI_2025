using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [SerializeField] GameObject dropCheck, jumpCheck;
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    RaycastHit hitFront, hitLeft, hitRight;
    [SerializeField] float forwardDist, sideDist;
    [SerializeField] float jumpForce;
    bool leftWall, rightWall, grounded;
    int randInt;

    [SerializeField] List<GameObject> targets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = 10.0f;
        forwardDist = 1.0f;
        sideDist = 2.0f;

        targets = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 0.9f, 0.5f), transform.forward, out hitFront, Quaternion.identity, forwardDist))
        {
            transform.LookAt(transform.position - hitFront.normal);
            RotateAway();
        }
        else if (targets.Count > 0)
        {
            if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), targets[0].transform.position + new Vector3(0, 1, 0)))
            {
                Debug.Log("Hey");
                transform.LookAt(targets[0].transform.position);
            }

            if (Vector3.Distance(transform.position, targets[0].transform.position) < 1.5f)
            {
                targets[0].SetActive(false);
            }
            if (targets[0].activeSelf == false)
            {
                targets.RemoveAt(0);
            }
        }
        
        if(transform.position.y < -0.4)//this wont work if the floor is varying heights
        {
            if(!Physics.BoxCast(dropCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
            {
                if (Physics.BoxCast(jumpCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
                {
                    rb.AddForce(new Vector3(0, 1, 0) * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
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
            }
            else
            {
                transform.Rotate(Vector3.up, -90);
            }
        }


        leftWall = false;
        rightWall = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            targets.Add(other.transform.parent.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            targets.Remove(other.transform.parent.gameObject);
        }
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawWireCube(transform.position + new Vector3(0, 1, -1), new Vector3(1, 1.8f, 1));
    // }
}
