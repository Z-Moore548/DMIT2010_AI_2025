using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UITestSample : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] GameObject squareTwo;
    [SerializeField] GameObject dropdown;
    public void Quit()
    {
        Application.Quit();
    }
    public void Red()
    {
        square.GetComponent<SpriteRenderer>().color = Color.red;
        squareTwo.GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void Green()
    {
        square.GetComponent<SpriteRenderer>().color = Color.green;
        squareTwo.GetComponent<SpriteRenderer>().color = Color.green;
    }

    
}
