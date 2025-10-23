using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

public class ChaserMover : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    RaycastHit hitFront, hitLeft, hitRight;
    [SerializeField] float forwardDist, sideDist, downDist;
    bool leftWall, rightWall;

    int randInt;

    [SerializeField] GameObject downCheck, jumpCheck;

    Rigidbody rbody;

    bool grounded;
    int count;

    [SerializeField] List<GameObject> targets = new List<GameObject>();
    [SerializeField] List<GameObject> pickup = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = 6;
        forwardDist = 1.0f;
        sideDist = 2.0f;
        // downDist = 1.0f;

        rbody = GetComponent<Rigidbody>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            // Rotate the mover if an object is detected in front
            if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.4f, 0.9f, 0.4f), transform.forward, out hitFront, Quaternion.identity, forwardDist))
            {
                transform.LookAt(transform.position - hitFront.normal);

                RotateAway();
            }
            else if(pickup.Count > 0)
            {
                if(!Physics.Linecast(transform.position + new Vector3(0, 1, 0), pickup[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count++;
                    if (count >= 50)
                    {
                        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                        {

                        }
                        else
                        {
                            transform.LookAt(pickup[0].transform.position);
                        }

                    }

                }
                if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), pickup[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count = 0;
                }
                if (Vector3.Distance(transform.position + Vector3.forward, pickup[0].transform.position) < 1.5f)
                {
                    pickup[0].SetActive(false);
                    StartCoroutine(SpeedUp());
                }
                if(pickup[0].activeSelf == false)
                {
                    pickup.RemoveAt(0);
                }
            }
            else if (targets.Count > 0)
            {
                if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), targets[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count++;
                    if (count >= 50)
                    {
                        transform.LookAt(targets[0].transform.position);
                    }

                }
                if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), targets[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count = 0;
                }

                if (Vector3.Distance(transform.position + Vector3.forward, targets[0].transform.position) < 1.5f) //The boxcast gets in the way of this and makes it turn
                {
                    targets[0].SetActive(false);
                }

                if (targets[0].activeSelf == false)
                {
                    targets.RemoveAt(0);
                }
            }

            // Rotate the mover if a hole is detected in front
             if (!Physics.BoxCast(downCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
            {
                // Check if there is a floor to jump to
                if (Physics.BoxCast(jumpCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f), -transform.up, out hitFront, Quaternion.identity, forwardDist))
                {
                    // Check to make sure there is no object in the way of the jump
                    if (!Physics.CheckBox(jumpCheck.transform.position, new Vector3(0.5f, 0.9f, 0.5f)))
                    {
                        rbody.AddRelativeForce(transform.up * 300);
                        grounded = false;
                    }
                    else
                    {
                        RotateAway();
                    }
                }
                else
                {
                    RotateAway();
                }
            }
        }
    }
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawWireCube(downCheck.transform.position  + new Vector3(0,-1,0), new Vector3(1, 1, 1));
    // }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.fixedDeltaTime);
    }
    void RotateAway()
    {
        leftWall = false;
        rightWall = false;

        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist))
        {
            leftWall = true;
        }

        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            targets.Add(other.transform.parent.gameObject);
        }
        if (other.CompareTag("PickUp"))
        {
            pickup.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            targets.Remove(other.transform.parent.gameObject);
        }
        if (other.CompareTag("PickUp"))
        {
            pickup.Remove(other.transform.parent.gameObject);
        }
    }
    IEnumerator SpeedUp()
    {
        movementSpeed += 2;
        yield return new WaitForSeconds(3);
        movementSpeed -= 2;
    }
}
