using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI leaderBoard;

    // Start is called before the first frame update
    void Start()
    {
        leaderBoard.text = HighScoreObject.Instance.displayLeaderBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
