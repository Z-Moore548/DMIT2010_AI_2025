using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject slotOne, slotTwo, slotThree, slotFour, locked, Unlocked;
    [SerializeField] GameObject blueSpy, redSpy;
    

    // Update is called once per frame
    void Update()
    {
        if (blueSpy.GetComponent<BlueSpy>().HasTaser)
        {
            slotOne.SetActive(true);
        }
        else
        {
            slotOne.SetActive(false);
        }
        if (blueSpy.GetComponent<BlueSpy>().FileGot)
        {
            slotTwo.SetActive(true);
        }
        else
        {
            slotTwo.SetActive(false);
        }
        if (redSpy.GetComponent<RedSpy>().KeyGot)
        {
            slotFour.SetActive(true);
        }
        else
        {
            slotFour.SetActive(false);
        }
        if (redSpy.GetComponent<RedSpy>().FileGot)
        {
            slotThree.SetActive(true);
        }
        else
        {
            slotThree.SetActive(false);
        }
        if(redSpy.GetComponent<RedSpy>().KeyGot || blueSpy.GetComponent<BlueSpy>().DoorPicked)
        {
            Unlocked.SetActive(true);
            locked.SetActive(false);
        }
        else
        {
            Unlocked.SetActive(false);
            locked.SetActive(true);
        }
    }
}
