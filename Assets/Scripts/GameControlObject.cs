using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControlObject : MonoBehaviour
{
    public RoomObject[] roomObjects;
    private CaveObject caveObject;
    private MapObject mapObject;
    private PlayerObject playerObject;
    public TextMeshProUGUI hint;
    public TextMeshProUGUI secret;
    public GameObject gameOver;
    public GameObject gameWin;
    public TextMeshProUGUI score;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < roomObjects.Length; i++)
        {
            // set room number 
            roomObjects[i].setRoomName(i + 1);
        }

        caveObject = GetComponent<CaveObject>();
        mapObject = GetComponent<MapObject>();
        playerObject = GameObject.Find("Player").GetComponent<PlayerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // returns the door position in a room that leads to the room 
    public Vector2 getDoorPosition(int roomNum, int leadsToRoom)
    {
        return roomObjects[roomNum - 1].returnDoor(leadsToRoom).gameObject.transform.position;
    }

    // returns toom position
    public Vector2 getRoomPosition(int roomNum)
    {
        return roomObjects[roomNum - 1].transform.position;
    }

    // Check surroudins to print hint
    public void checkSurroundingRooms(int roomNum)
    {
        int[] rooms = caveObject.returnAdjRooms(roomNum);
        hint.text = "Hint";
        int[] bats = mapObject.returnBats();
        int[] bottomlessPits = mapObject.returnBottomlessPits();
        for (int i = 0; i < rooms.Length;i++)
        {
            for(int j = 0;  j < 2; j++)
            {
                if (rooms[i] == bats[j])
                {
                    hint.text = "I Hear Gangsters";
                } else if (rooms[i] == bottomlessPits[j])
                {
                    hint.text = "I Feel A Draft";
                }
            }
            if (rooms[i] == mapObject.getMurdererPosition())
            {
                hint.text = "I Hear The Murderer";
            } 

        }
    }

    // checks if the room number that bullet went into had the murderer
    public void didPlayerHitWumpus(int roomNum)
    {
        if(mapObject.getMurdererPosition() == roomNum)
        {
            // win
            gameWin.SetActive(true);
            // caculate points
            score.text = "Score: " + playerObject.computeScore();
            HighScoreObject.Instance.SaveScores(playerObject.computeScore());
            HighScoreObject.Instance.LoadScores();
        } else
        {
            int random = UnityEngine.Random.Range(1, 31);
            while (random == playerObject.getPlayerLocation())
            {
                random = UnityEngine.Random.Range(1, 31);
            }
            mapObject.setMurdererPosition(random);
        }
    }

    // sets the secret message on display
    public void setSecret(string secretMessage)
    {
        secret.text = secretMessage;
    }

    // display gameover screen 
    public void gameOverEvent(int score)
    {
        HighScoreObject.Instance.SaveScores(score);
        HighScoreObject.Instance.LoadScores();
        gameOver.SetActive(true);
    }

    // sends player back to menu
    public void sentToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
