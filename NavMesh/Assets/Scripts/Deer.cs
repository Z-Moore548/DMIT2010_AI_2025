using UnityEngine;
using UnityEngine.AI;


public class Deer : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField] GameObject targetObject;
    [SerializeField] States state;
    int hunger, sleepy, stamina;

    enum States
    {
        Wandering, Resting, Serching, Eating, Running, Dead
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
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

    public void SetTarget(Vector3 target)
    {
        myAgent.SetDestination(target);
    }

    public void SetTarget(GameObject target)
    {
        targetObject = target;
    }
}
