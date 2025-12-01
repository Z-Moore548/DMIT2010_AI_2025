using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Wolf : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField] GameObject targetObject, caveLocation;
    [SerializeField] States state;
    [SerializeField] int sleepy, stamina;
    enum States
    {
        Wandering, Resting, Stalking, Chasing, Attacking, Eating
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        //transform.position = new Vector3(-38, transform.position.y, -38);
        sleepy = 0;
        stamina = 1000;
        //state = States.Resting;
    }

    private void Update()
    {
        switch (state)
        {
            case States.Wandering:
            break;
            case States.Resting:
                if(transform.position.x != caveLocation.transform.position.x && transform.position.z != caveLocation.transform.position.z)
                {
                    SetTarget(caveLocation.transform.position);
                }
                else
                {
                    sleepy++;
                }
                if(sleepy == 1000)
                {
                    state = States.Wandering;
                }
            
            break;
            case States.Stalking:
            break;
            case States.Chasing:
            break;
            case States.Attacking:
            break;
            case States.Eating:
            break;
        }
    }

    public void SetTarget(Vector3 target)
    {
        myAgent.SetDestination(target);
    }

    public void SetTarget(GameObject target)
    {
        targetObject = target;
    }
}
