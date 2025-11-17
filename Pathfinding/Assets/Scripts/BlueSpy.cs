using System.Collections;
using UnityEngine;

public class BlueSpy : MonoBehaviour
{
    [SerializeField] GameObject startNode, endNode, currentNode, targetNode, prevNode, file;
    [SerializeField] GameObject[] waypoints;
    float moveSpeed;
    int waypointIndex = 0;
    private bool fileGot, doorPicked, picking;

    public bool FileGot { get => fileGot; set => fileGot = value; }
    public bool DoorPicked { get => doorPicked; set => doorPicked = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = startNode.transform.position;
        currentNode = startNode;
        targetNode = currentNode;
        endNode = waypoints[waypointIndex];
        moveSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1f)
        {
            prevNode = currentNode;
            currentNode = targetNode;

            if(currentNode == endNode)
            {
                waypointIndex++;
                // if (waypointIndex > waypoints.Length)
                // {
                //     waypointIndex = 0;
                // }
                endNode = waypoints[waypointIndex];
            }

            float closestDistance = 10000;
            //GameObject closestNode;

            Pathnode pathScript = currentNode.GetComponent<Pathnode>();

            if (pathScript != null)
            {
                //bool found = false;
                //int pathIndex = 0;

                //int randNum = Random.Range(0, pathScript.connections.Count);
                //targetNode = pathScript.connections[randNum];

                for (int i = 0; i < pathScript.connections.Count; i++)
                {
                    if(pathScript.connections[i] != prevNode && pathScript.connections[i].GetComponent<Pathnode>().nodeActive)
                    {
                        if(Vector3.Distance(pathScript.connections[i].transform.position, endNode.transform.position) < closestDistance)
                        {
                            targetNode = pathScript.connections[i];
                            closestDistance = Vector3.Distance(pathScript.connections[i].transform.position, endNode.transform.position);
                        }
                        
                    }
                }
                
                
            }
        }
        else if (picking)
        {
            
        }
        else
        {
            transform.Translate((targetNode.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
        if(currentNode == waypoints[2])
        {
            FileGot = true;
            file.SetActive(false);
        }
        if(currentNode == waypoints[0])
        {
            StartCoroutine(PickingLock());
        }
    }

    IEnumerator PickingLock()
    {
        picking = true;
        yield return new WaitForSeconds(3);
        picking = false;
        doorPicked = true;
    }
}
