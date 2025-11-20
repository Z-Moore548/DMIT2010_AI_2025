using UnityEngine;
using UnityEditor;

public class ObjectCustom
{
    #region Color
    [MenuItem("Customize Object/Color/Red", priority = 0)]

    static void ChangeColorToRed()
    {
        foreach (Transform shape in Selection.transforms)
        {
            shape.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    [MenuItem("Customize Object/Color/Blue", priority = 0)]

    static void ChangeColorToBlue()
    {
        foreach (Transform shape in Selection.transforms)
        {
            shape.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    [MenuItem("Customize Object/Color/Yellow", priority = 0)]
    static void ChangeColorToYellow()
    {
        foreach (Transform shape in Selection.transforms)
        {
            shape.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
    [MenuItem("Customize Object/Color/Red", true)]
    [MenuItem("Customize Object/Color/Blue", true)]
    [MenuItem("Customize Object/Color/Yellow", true)]

    static bool ValidateShapesB()
    {
        bool validated = true;
        if (Selection.count == 0)
        {
            validated = false;
        }

        return validated;
    }
    #endregion
    #region Size
    [MenuItem("Customize Object/Size/2X", priority = 50)]

    static void GrowSize()
    {
        foreach (Transform shape in Selection.transforms)
        {
            shape.transform.localScale *= 2;
        }
    }
    [MenuItem("Customize Object/Size/-2X", priority = 50)]
    static void ShrinkSize()
    {
        foreach (Transform shape in Selection.transforms)
        {
            shape.transform.localScale /= 2;
        }
    }
    [MenuItem("Customize Object/Size/2X", true)]
    [MenuItem("Customize Object/Size/-2X", true)]
    static bool ValidateSizeSmall()
    {
        bool validated = true;
        if (Selection.count == 0)
        {
            validated = false;
        }

        return validated;
    }
    #endregion
}
