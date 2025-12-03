using UnityEngine;
using UnityEngine.AI;


public class Deer : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField] GameObject targetObject;
    [SerializeField] States state;
    float hunger, sleepy, stamina;
    bool running, dead;

    public bool Running { get => running; set => running = value; }
    public bool Dead { get => dead; set => dead = value; }

    enum States
    {
        Wandering, Resting, Serching, Eating, Running, Dead
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        running = false;
    }

    private void Update()
    {
        switch (state)
        {
            case States.Wandering:
            break;
            case States.Resting:
            break;
            case States.Serching:
            break;
            case States.Eating:
            break;
            case States.Running:
            break;
            case States.Dead:
            break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wolf")
        {
            running = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Wolf")
        {
            running = false;
        }
    }
    public void Attacked()
    {
        
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
