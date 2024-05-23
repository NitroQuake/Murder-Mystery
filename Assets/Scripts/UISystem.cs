using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Menu to Game Screen 
    public void setGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void setLeaderBoardScene()
    {
        SceneManager.LoadScene(2);
    }

    public void turnOffPanel()
    {
        panel.SetActive(false);
    }

    public void turnOnPanel()
    {
        panel.SetActive(true);
    }
}
