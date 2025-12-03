using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Wolf : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField] GameObject targetObject, caveLocation, deer;
    [SerializeField] States state;
    [SerializeField] float sleepy, stamina;
    [SerializeField] bool deerSpotted;
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
        deerSpotted = false;
        //state = States.Resting;
    }

    private void Update()
    {
        switch (state)
        {
            case States.Wandering: //Need to make moveemnt random
                myAgent.speed = 5;
                SetTarget(new Vector3(0,transform.position.y, 0));
                if(sleepy <= 0)
                {
                    state = States.Resting;
                }
                
                if (deerSpotted)
                {
                    state = States.Stalking;
                }
                break;
            case States.Resting: //done
                if(transform.position.x != caveLocation.transform.position.x && transform.position.z != caveLocation.transform.position.z)
                {
                    SetTarget(caveLocation.transform.position);
                }
                else
                {
                    sleepy += 1;
                }
                if(sleepy >= 1000)
                {
                    state = States.Wandering;
                }
            
                break;
            case States.Stalking: //done not tested
                myAgent.speed = 2;
                SetTarget(deer);
                if (deer.GetComponent<Deer>().Running)
                {
                    state = States.Chasing;
                }
                if (!deerSpotted)
                {
                    state = States.Wandering;
                }
                break;
            case States.Chasing: // done not tested
                myAgent.speed = 7;
                stamina--;
                if(stamina <= 0)
                {
                    sleepy = 0;
                    state = States.Wandering;
                }
                if (!deerSpotted)
                {
                    state = States.Wandering;
                }
                if(Vector3.Distance(transform.position, deer.transform.position) < 3)//can be changed
                {
                    state = States.Attacking;
                }
            break;
            case States.Attacking: // need to actually make the attack
                deer.GetComponent<Deer>().Attacked();//Need to make this activate on intervals
                if(Vector3.Distance(transform.position, deer.transform.position) > 3)//can be changed
                {
                    state = States.Chasing;
                }
                if (deer.GetComponent<Deer>().Dead)
                {
                    state = States.Eating;
                }
            break;
            case States.Eating: // done not tested
                SetTarget(null);
                StartCoroutine(Eating());
            break;
            
        }
        if(state != States.Resting)
        {
            sleepy -= 0.1f;
        }
        if (targetObject != null)
        {
            myAgent.SetDestination(targetObject.transform.position);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            deerSpotted = true;
            deer = other.gameObject.transform.parent.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            deerSpotted = false;
            deer = null;
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

    IEnumerator Eating()
    {
        yield return new WaitForSeconds(3);
        state = States.Wandering;
    }
}
