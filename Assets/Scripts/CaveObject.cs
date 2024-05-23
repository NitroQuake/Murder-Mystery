using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveObject : MonoBehaviour
{
    private int[,] availableRooms;
    private int[,] adjcentRooms;
    public bool allowReturning = false;
    // Start is called before the first frame update
    void Start()
    {
        availableRooms = new int[30, 6];
        getAllAvaiableRooms();
        adjcentRooms = new int[30,3];
        createConnectivity();
        allowReturning = true;
        // reloads game if not all rooms have a path to another room 
        if (checkUnreachableRoom())
        {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // return available adjcent rooms for the room 
    public int[] returnAvailableRooms(int roomNum)
    {
        int[] adjRooms = new int[6];
        for (int i = 0; i < adjRooms.Length; i++)
        {
            adjRooms[i] = availableRooms[roomNum - 1, i];
        }
        return adjRooms;
    }

    // returns the possible adjcent rooms for the room 
    public int[] returnAdjRooms(int roomNum)
    {
        int[] adjRooms = new int[3];
        for(int i = 0; i < adjRooms.Length; i++)
        {
            adjRooms[i] = adjcentRooms[roomNum - 1, i];
        }
        return adjRooms;
    }

    // Produces all the adjcent rooms for each room 
    public void getAllAvaiableRooms()
    {
        for (int i = 1; i <= availableRooms.GetLength(0); i+=6)
        {
            int[] values = new int[6];
            values[0] = i - 1;
            values[1] = i - 6;
            values[2] = i - 5;
            values[3] = i + 1;
            values[4] = i + 6;
            values[5] = i + 5;

            for(int j = 0; j < values.Length; j++)
            {
                if (values[j] <= 0)
                {
                    values[j] += 30;
                } else if (values[j] > 30)
                {
                    values[j] %= 10;
                }
                availableRooms[i - 1,j] = values[j];
            }
            
            for(int j = 2 + i - 1;j < 6 + i - 1; j+=2)
            {
                values[0] = j - 1;
                values[1] = j - 6;
                values[2] = j + 1;
                values[3] = j + 7;
                values[4] = j + 6;
                values[5] = j + 5;

                for (int k = 0; k < values.Length; k++)
                {
                    if (values[k] <= 0)
                    {
                        values[k] += 30;
                    }
                    else if (values[k] > 30)
                    {
                        values[k] %= 10;
                    }
                    availableRooms[j - 1, k] = values[k];
                }

                values[0] = j + 1 - 7;
                values[1] = j + 1 - 6;
                values[2] = j + 1 - 5;
                values[3] = j + 1 + 1;
                values[4] = j + 1 + 6;
                values[5] = j + 1 - 1;

                for (int k = 0; k < values.Length; k++)
                {
                    if (values[k] <= 0)
                    {
                        values[k] += 30;
                    }
                    else if (values[k] > 30)
                    {
                        values[k] %= 10;
                    }
                    availableRooms[j, k] = values[k];
                }
            }

            values[0] = i + 5 - 1;
            values[1] = i + 5 - 6;
            values[2] = i + 5 - 5;
            values[3] = i + 5 + 1;
            values[4] = i + 5 + 6;
            values[5] = i + 5 + 5;

            for (int j = 0; j < values.Length; j++)
            {
                if (values[j] <= 0)
                {
                    values[j] += 30;
                }
                else if (values[j] > 30)
                {
                    values[j] %= 10;
                }
                availableRooms[i + 4, j] = values[j];
            }
        }
    }

    // prints the 2d array into the console 
    public void print2DArray(int[,] array2D)
    {
        String text = "";
        for (int i = 0; i < array2D.GetLength(0); i++)
        {
            for (int j = 0; j < array2D.GetLength(1); j++)
            {
                text += array2D[i, j] + " ";
            }
            Debug.Log(text);
            text = "";
        }
    }

    // Generates the paths between rooms
    public void createConnectivity()
    {
        for (int i = 0; i < adjcentRooms.GetLength(0); i+=2)
        {
            for (int j = 0; j < adjcentRooms.GetLength(1); j++)
            {
                if (adjcentRooms[i, j] != 0) 
                {
                    continue;
                }

                int random = UnityEngine.Random.Range(0, 6);
                int k = 0;
                // count is used to make sure that it doesn't loop forever as some rooms cannot have 3 rooms 
                int count = 0;
                while (k < adjcentRooms.GetLength(1) && count <= 12)
                {
                    // checks for duplicates and if chosen adjacent room is full 
                    if (availableRooms[i, random] == adjcentRooms[i, k] || adjcentRooms[availableRooms[i, random] - 1, 2] != 0)
                    {
                        random = UnityEngine.Random.Range(0, 6);
                        k = 0;  
                        continue; 
                    }
                    k++;  
                    count++;
                }

                // if count is more than 12, it signals an infinite loop, it reloads the game
                if(count > 12)
                {
                    SceneManager.LoadScene(1);
                }

                // Sets chosen room to the room 
                for (int l = 0; l < adjcentRooms.GetLength(1); l++)
                {
                    if(adjcentRooms[availableRooms[i, random] - 1, l] == 0)
                    {
                        adjcentRooms[availableRooms[i, random] - 1, l] = i + 1;
                        break;
                    } 
                }

                int room = availableRooms[i, random];
                adjcentRooms[i, j] = room;
            }
        }
    }

    // checks if all rooms have a path to another room 
    public bool checkUnreachableRoom()
    {
        for (int i = 0; i < adjcentRooms.GetLength(0); i++)
        {
            if (adjcentRooms[i, 0] == 0)
            {
                return true;
            }
        }
        return false;
    }
}
