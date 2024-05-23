using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public int roomNum;
    public DoorObject[] doors;
    public CaveObject caveObject;
    public int[] adjRooms = new int[6];
    public int[] chosenRooms = new int[3];
    public bool receieved = false;
    // Start is called before the first frame update
    void Start()
    {
        caveObject = GameObject.Find("GameControlObject").GetComponent<CaveObject>();
        doors = GetComponentsInChildren<DoorObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if caveobject is fron generating map and does it once
        if (caveObject.allowReturning && !receieved)
        {
            adjRooms = caveObject.returnAvailableRooms(roomNum);
            for(int i = 0; i < doors.Length; i++)
            {
                // Assigning doors their room number and what room that door leads to
                doors[i].setLeadsToRoom(adjRooms[i]);
                doors[i].setDoorNum(roomNum);
            }
            chosenRooms = caveObject.returnAdjRooms(roomNum);

            // chooses which doors show up and which don't based on the cave object
            for(int i = 0; i < doors.Length; i++)
            {
                bool theChosenRoom = false;
                for(int j = 0; j < chosenRooms.Length; j++)
                {
                    if (adjRooms[i] == chosenRooms[j])
                    {
                        theChosenRoom = true;
                    }
                }
                if(!theChosenRoom)
                {
                    doors[i].gameObject.SetActive(false);
                }
            }
            receieved = true;
        }
    }

    // set room num
    public void setRoomName(int num)
    {
        roomNum = num;
    }

    // return door that leads to room
    public DoorObject returnDoor(int leadsToRoom)
    {
        for(int i = 0; i < doors.Length;i++)
        {
            if (doors[i].returnDoorSetLeadsToRoom() == leadsToRoom)
            {
                return doors[i];
            }
        }
        return null;
    }
}
