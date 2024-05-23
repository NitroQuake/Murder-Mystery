using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    public int doorNum;
    public int leadsToRoom;
    private PlayerObject playerObject;
    private GameControlObject gameControlObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<PlayerObject>();
        gameControlObject = GameObject.Find("GameControlObject").GetComponent<GameControlObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setLeadsToRoom(int roomNum)
    {
        leadsToRoom = roomNum;
    }

    public void setDoorNum(int doorNum)
    {
        this.doorNum = doorNum;
    }

    public int returnDoorSetLeadsToRoom()
    {
        return leadsToRoom;
    }

    // Move through doors
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // get new door position and room position
            Vector2 doorPos = gameControlObject.getDoorPosition(leadsToRoom, doorNum);
            Vector2 roomPos = gameControlObject.getRoomPosition(leadsToRoom);
            float weightCloser = 1.5f;
            float weightFarther = 0.5f;
            // Caculate new player location
            Vector2 movePlayer = new Vector2((doorPos.x * weightCloser + roomPos.x * weightFarther) / (weightCloser + weightFarther), (doorPos.y * weightCloser + roomPos.y * weightFarther) / (weightCloser + weightFarther));
            playerObject.gameObject.transform.position = movePlayer;
            // Update player location
            playerObject.move(leadsToRoom);
        } //tracks bullet and destroys it
        else if (collision.CompareTag("Bullet"))
        {
            gameControlObject.didPlayerHitWumpus(leadsToRoom);
            Destroy(collision.gameObject);
        }
    }
}
