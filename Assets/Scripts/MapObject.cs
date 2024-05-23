using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public int[] bottomlessPits;
    public int[] bats;
    private int[] alreadyHasHazard;
    public int murderPos;
    public GameObject wumpus;
    public GameObject pit;
    public GameObject[] gangsters;
    private GameControlObject gameControlObject;

    //for itamar's object
    private PlayerObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        //for itamar's object
        playerObject = GameObject.Find("Player").GetComponent<PlayerObject>();

        gameControlObject = GetComponent<GameControlObject>();
        wumpus = GameObject.Find("Wumpus");

        bottomlessPits = new int[3];
        bats = new int[3];
        alreadyHasHazard = new int[6];

        setMurdererPosition(UnityEngine.Random.Range(2, 31));

        bottomlessPits = makePits(); //make sure to do pits makePit() before makeBats()
        bats = makeBats();
        // setLocationOfHazard(bottomlessPits);
        // setLocationOfHazard(bats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Makes random pit positions
    public int[] makePits()
    {
        List<int> pitPositions = new List<int>(3);
        pitPositions.Add(0);
        pitPositions.Add(0);
        pitPositions.Add(0);
        for (int i = 1; i <= 3; i++)
        {
            //random room
            int pitLoc = UnityEngine.Random.Range(2, 31);

            // checks if room is taken, if so loop again
            if (pitLoc == pitPositions[0] || pitLoc == pitPositions[1] || pitLoc == pitPositions[2] || pitLoc == playerObject.getPlayerLocation() || pitLoc == murderPos)
            {
                i--;
            }
            else
            {
                pitPositions[i - 1] = pitLoc;
            }
        }

        int[] pitPositonsArray = new int[3];
        for(int i = 0; i < pitPositonsArray.Length; i++)
        {
            pitPositonsArray[i] = pitPositions[i];
            Vector2 pitPos = gameControlObject.getRoomPosition(pitPositonsArray[i]);
            Instantiate(pit, pitPos, Quaternion.identity);
        }
        return pitPositonsArray;
    }

    // Makes random bats positions
    public int[] makeBats()
    {
        List<int> batPositions = new List<int>(3);
        batPositions.Add(0);
        batPositions.Add(0);
        batPositions.Add(0);
        for (int i = 1; i <= 3; i++)
        {
            int batLoc = UnityEngine.Random.Range(2, 31);

            if (batLoc == batPositions[0] || batLoc == batPositions[1] || batLoc == batPositions[2] || batLoc == bottomlessPits[0]
                || batLoc == bottomlessPits[1] || batLoc == bottomlessPits[2]
                || batLoc == playerObject.getPlayerLocation() || batLoc == murderPos)
            {
                i--;
            }
            else
            {
                batPositions[i - 1] = batLoc;
            }
        }

        int[] batPositonsArray = new int[3];
        for (int i = 0; i < batPositonsArray.Length; i++)
        {
            batPositonsArray[i] = batPositions[i];
            Vector2 gangstersPos = gameControlObject.getRoomPosition(batPositonsArray[i]);
            gangsters[i] = Instantiate(gangsters[i], gangstersPos, Quaternion.identity);
        }
        return batPositonsArray;
    }

    /*
    // Generates random locations for the hazards
    public void setLocationOfHazard(int[] hazard)
    {
        for(int i = 0; i < hazard.Length; i++)
        {
            int random = UnityEngine.Random.Range(2, 31);
            int j = 0;
            while (j < alreadyHasHazard.Length)
            {
                if (random == alreadyHasHazard[j])
                {
                    random = UnityEngine.Random.Range(0, 6);
                    j = 0;
                    continue;
                }
                j++;
            }
            hazard[i] = random;
        }
    }
    */

    // set new position for bat
    public void batChangeRoom(int index)
    {
        int randomRoom = UnityEngine.Random.Range(1, 31);
        // Not the same room with player
        while(randomRoom == playerObject.getPlayerLocation() || randomRoom == murderPos || randomRoom == bottomlessPits[0] || randomRoom == bottomlessPits[1] || randomRoom == bottomlessPits[2])
        {
            randomRoom = UnityEngine.Random.Range(1, 31);
        }
        bats[index] = randomRoom;
        Vector2 gangstersPos = gameControlObject.getRoomPosition(bats[index]);
        gangsters[index].transform.position = gangstersPos;
    }

    public int[] returnBottomlessPits()
    {
        return bottomlessPits;
    }

    public int[] returnBats()
    {
        return bats;
    }

    // sets murder position
    public void setMurdererPosition(int roomNum)
    {
        Vector2 wumpusPos = gameControlObject.getRoomPosition(roomNum);
        murderPos = roomNum;
        wumpus.transform.position = wumpusPos;

    }

    // returns murder position
    public int getMurdererPosition()
    {
        return murderPos;
    }

    // return secret message
    public string returnSecret(int random, int currentRoom)
    {
        if(random == 1)
        {
            return "Gangester is at room " + bats[UnityEngine.Random.Range(0, bats.Length)];
        } else if(random == 2)
        {
            return "Pit is at room " + bottomlessPits[UnityEngine.Random.Range(0, bottomlessPits.Length)];
        } else if(random == 3)
        {
            return "You are in room " + currentRoom;
        } else 
        {
            return "Murderer is at room " + getMurdererPosition();
        }
    }
}
