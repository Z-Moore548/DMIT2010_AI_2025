using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    NavMeshAgent myAgent;

    [SerializeField] GameObject targetObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (targetObject != null)
        {
            myAgent.SetDestination(targetObject.transform.position);
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
