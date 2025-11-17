using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject[] nodes;

    [SerializeField] bool open;
    [SerializeField] GameObject doorModel;
    [SerializeField] GameObject redSpy, blueSpy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (redSpy.GetComponent<RedSpy>().KeyGot || blueSpy.GetComponent<BlueSpy>().DoorPicked)
        {
            open = !open;

            if (open)
            {
                
                foreach (GameObject node in nodes)
                {
                    node.GetComponent<Pathnode>().nodeActive = true;
                }
                doorModel.SetActive(false);
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
