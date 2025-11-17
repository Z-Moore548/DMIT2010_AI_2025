using UnityEngine;

public class AIPathfinder : MonoBehaviour
{
    [SerializeField] GameObject startNode, endNode, currentNode, targetNode, prevNode;
    [SerializeField] GameObject chaseTarget;
    [SerializeField] GameObject[] waypoints;
    float moveSpeed;
    int waypointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = startNode.transform.position;
        currentNode = startNode;
        targetNode = currentNode;
        endNode = waypoints[waypointIndex];
        moveSpeed = 5.0f;
        chaseTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(chaseTarget != null)
        {
            Debug.Log("Chasing");
        }
        if(chaseTarget == null)
        {
            //If the AI is at the target node than find a new target to move to
            if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1f)
            {
                prevNode = currentNode;
                currentNode = targetNode;

                if(currentNode == endNode)
                {
                    waypointIndex = Random.Range(0, waypoints.Length);
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
            else
            {
                transform.Translate((targetNode.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueSpy") || other.gameObject.CompareTag("RedSpy"))
        {
            chaseTarget = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlueSpy") || other.gameObject.CompareTag("RedSpy"))
        {
            chaseTarget = null;
        }
    }

}
