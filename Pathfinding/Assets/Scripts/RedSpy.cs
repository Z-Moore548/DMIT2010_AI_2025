using UnityEngine;

public class RedSpy : MonoBehaviour
{
    [SerializeField] GameObject startNode, endNode, currentNode, targetNode, prevNode, key, file;
    [SerializeField] GameObject[] waypoints;
    float moveSpeed;
    int waypointIndex = 0;
    [SerializeField] private bool keyGot, fileGot, fileDestoryed;

    public bool KeyGot { get => keyGot; set => keyGot = value; }
    public bool FileGot { get => fileGot; set => fileGot = value; }

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
        else
        {
            transform.Translate((targetNode.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
        if(currentNode == waypoints[1])
        {
            keyGot = true;
            key.SetActive(false);
        }
        if(endNode == waypoints[3] && keyGot)
        {
            waypointIndex++;
            endNode = waypoints[waypointIndex];
        }
        if(currentNode == waypoints[3])
        {
            fileGot = true;
            file.SetActive(false);
        }
        if(endNode == waypoints[3] && fileGot)
        {
            waypointIndex++;
            endNode = waypoints[waypointIndex];
        }
        if(currentNode == waypoints[5])
        {
            fileDestoryed = false;
        }
        if(endNode == waypoints[5] && fileDestoryed)
        {
            waypointIndex++;
            endNode = waypoints[waypointIndex];
        }
    }

    public void Caught()
    {
        waypointIndex = 0;
        Start();
    }
}
