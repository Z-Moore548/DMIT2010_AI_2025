using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] GameObject[] nodes;

    [SerializeField] bool open;
    [SerializeField] GameObject doorModel, redSpy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (redSpy.GetComponent<RedSpy>().keyGot)
        {
            open = true;

            if (open)
            {
                doorModel.SetActive(false);
                foreach (GameObject node in nodes)
                {
                    node.GetComponent<Pathnode>().nodeActive = true;
                }

            }
            else
            {
                doorModel.SetActive(true);
                foreach (GameObject node in nodes)
                {
                    node.GetComponent<Pathnode>().nodeActive = false;
                }
            }
        }        
    }
}
