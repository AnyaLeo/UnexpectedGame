using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void enableHiddenObject()
    {
        gameObject.SetActive(true);
    }

    public void disableObject()
    {
        gameObject.SetActive(false);
    }
}
