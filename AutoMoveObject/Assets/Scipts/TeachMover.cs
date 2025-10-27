using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class AdvancedMover : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    RaycastHit hitFront, hitLeft, hitRight, hitEnemy;
    [SerializeField] float forwardDist, sideDist, downDist;
    bool leftWall, rightWall;

    int randInt, count;//count might not work in the future as i just have it so it takes a second before detecting something. 
    // but if ther is two hunters detected but one is behind a wall it wont see the second

    [SerializeField] GameObject downCheck, jumpCheck;

    Rigidbody rbody;

    bool grounded, isDisguised;
    Vector3 chaserNormal;

    [SerializeField] List<GameObject> chaser = new List<GameObject>();
    [SerializeField] List<GameObject> speed = new List<GameObject>();
    [SerializeField] List<GameObject> disguise = new List<GameObject>();

    public bool IsDisguised { get => isDisguised; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = 5;
        forwardDist = 1.0f;
        sideDist = 2.0f;
        // downDist = 1.0f;

        rbody = GetComponent<Rigidbody>();
        grounded = true;
        isDisguised = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
        if (grounded)
        {
            // Rotate the mover if an object is detected in front
            if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.4f, 0.9f, 0.4f), transform.forward, out hitFront, Quaternion.identity, forwardDist))
            {
                transform.LookAt(transform.position - hitFront.normal);

                RotateAway();
            }
            else if (disguise.Count > 0)
            {
                if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), disguise[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count++;
                    if (count >= 50)
                    {
                        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                        {

                        }
                        else
                        {
                            transform.LookAt(disguise[0].transform.position);
                        }

                    }
                }
                if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), disguise[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count = 0;
                }
                if (Vector3.Distance(transform.position + Vector3.forward, disguise[0].transform.position) < 2)
                {
                    disguise[0].SetActive(false);
                    StartCoroutine(Disguise());
                }
                if (disguise[0].activeSelf == false)
                {
                    disguise.RemoveAt(0);
                }
            }
            else if (speed.Count > 0)
            {
                if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), speed[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count++;
                    if (count >= 50)
                    {
                        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                        {

                        }
                        else
                        {
                            transform.LookAt(speed[0].transform.position);
                        }

                    }

                }
                if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), speed[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count = 0;
                }
                if (Vector3.Distance(transform.position + Vector3.forward, speed[0].transform.position) < 2)
                {
                    speed[0].SetActive(false);
                    StartCoroutine(SpeedUp());
                }
                if (speed[0].activeSelf == false)
                {
                    speed.RemoveAt(0);
                }
            }
            else if( chaser.Count > 1) //&& !Physics.Linecast(transform.position + new Vector3(0, 1, 0), chaser[1].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
            {
                
                if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), chaser[1].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count++;
                    if(count >= 50)
                    {
                       if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                        {

                        }
                        else
                        {
                            Vector3 mid = chaser[0].transform.position + chaser[1].transform.position;
                            Vector3 dirMid = mid - transform.position;
                            transform.rotation = Quaternion.LookRotation(transform.position - dirMid);
                        } 
                    }
                    
                }
                else 
                {
                    if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), chaser[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                    {
                        count++;
                        if (count >= 50)
                        {
                            if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                            {

                            }
                            else
                            {
                                transform.rotation = Quaternion.LookRotation(transform.position - chaser[0].transform.position);
                            }

                        }
                    }
                }
            }
            else if (chaser.Count > 0)
            {
                Debug.Log("RUN");
                if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), chaser[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    //transform.LookAt(chaser[0].transform.position);
                    count++;
                    if (count >= 50)
                    {
                        if (Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), -transform.right, out hitLeft, Quaternion.identity, sideDist) || Physics.BoxCast(transform.position + transform.up, new Vector3(0.5f, 1, 0.5f), transform.right, out hitRight, Quaternion.identity, sideDist))
                        {

                        }
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(transform.position - chaser[0].transform.position);
                        }

                    }

                }
                if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), chaser[0].transform.position + new Vector3(0, 1, 0), 3 << LayerMask.NameToLayer("Walls")))
                {
                    count = 0;
                }
                // if (Vector3.Distance(transform.position + Vector3.forward, targets[0].transform.position) < 1.5f) //The boxcast gets in the way of this and makes it turn
                // {
                //     targets[0].SetActive(false);
                // }

                if (chaser[0].activeSelf == false)
                {
                    chaser.RemoveAt(0);
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
        if (other.CompareTag("Chaser"))
        {
            chaser.Add(other.transform.parent.gameObject);
        }
        if (other.CompareTag("PickUp"))
        {
            speed.Add(other.transform.parent.gameObject);
        }
        if (other.CompareTag("Disguse"))
        {
            disguise.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chaser"))
        {
            chaser.Remove(other.transform.parent.gameObject);
        }
        if (other.CompareTag("PickUp"))
        {
            speed.Remove(other.transform.parent.gameObject);
        }
        if (other.CompareTag("Disguse"))
        {
            disguise.Add(other.transform.parent.gameObject);
        }
    }
    IEnumerator SpeedUp()
    {
        movementSpeed += 2;
        yield return new WaitForSeconds(3);
        movementSpeed -= 2;
    }
    IEnumerator Disguise()
    {
        isDisguised = true;
        yield return new WaitForSeconds(3);
        isDisguised = false;
    }
}

