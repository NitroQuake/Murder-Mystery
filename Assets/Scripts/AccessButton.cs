using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addName(string name)
    {
        HighScoreObject.Instance.addName(name);
    }
}
