using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HighScoreObject : MonoBehaviour
{
    //make a string array list storing the players name and the players int score as a string. 
    //then return this for the leaderboad object
    public static HighScoreObject Instance;

    //private List <Object> players;
    private List<int> scores;
    private List<string> names;
    private int count;
    //constructer
    //made it take a list as a parameter instead of just a singular player.

    [System.Serializable]
    public class SaveData
    {
        public List<int> scores;
        public List<string> names;
        public int count;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    public void SaveScores(int score)
    {
        scores.Add(score);
        names.Add("Anonymous");
        count++;
        SaveData data = new SaveData();
        data.names = names;
        data.scores = scores;
        data.count = count;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            names = data.names;
            scores = data.scores;
            count = data.count;
        } else
        {
            names = new List<string>();
            names.Add("Anonymous"); // beginning name
            scores = new List<int>();
            count = 1;
            SaveData data = new SaveData();
            data.names = names;
            data.scores = scores;
            data.count = count;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void addName(string name)
    {
        // makes sure that names List does not exceed scores List and replaces the name 
        if (count == names.Count)
        {
            names.RemoveAt(count - 1);
            names.Add(name);
        } else
        {
            names.Add(name);
        }
    }

    public string displayLeaderBoard()
    {
        if(count != 1)
        {
            Dictionary<string, int> organizePairs = new Dictionary<string, int>();
            for (int i = 0; i < count - 1; i++)
            {
                organizePairs.Add(names[i], scores[i]);
            }

            string text = "";
            var items = from pair in organizePairs
                        orderby pair.Value descending
                        select pair;

            int rank = 1;
            // Display results.
            foreach (KeyValuePair<string, int> pair in items)
            {
                text += rank + ". " + pair.Key + " " + pair.Value + "\n";
                rank++;
            }

            return text;
        } else
        {
            return "No Scores Have Been Registered";
        }
    }

    /*
    public void addPlayer(int tally, string title)
    {
        addToDictionary(tally, title);
        scores.Add(tally);
        names.Add(title);
        scores.Sort();
        addToNames();
            
    }
    */

    //calculates score and adds it list scores
    /* public int getScore ()
    {
        //there needs to be a if statement so that the code only runs if the player killed the whumpus
        //or only gets called when the whumpus is killed
        score = 100 - player.getTurns() + player.getCoins() + (10*player.getBullets());
        return score;
    }*/
    //this is used to add the score to arraylist scores. this should also be run after the whumpus is killed
        
    /*?*public void fillLists()
    {
        clearList();
        for (int i = 0; i < players.Count; i++)
        {
            score = players[i].computeScore();
            name = players[i].getPlayerName();
            scores.Add(score);
            names.Add(name);
            addToDictionary();

        }
        //addToNames();
        scores.Sort();
        addToNames();
    }*/

    // public void addScore()
    //{
    //  scores.Add(score);
    //scores.Sort();
    //}

    //this returns all of the scores so that we can make a leader board
    //it should already be in order from highest to lowest when it gets returned
    public List<int> getScores()
    {
        // clearList();
        //fillLists();
        return scores;
    }

    //this returns the highest score
    //this is probably unnessecary
    public int getHighestScore()
    {
        //clearList();
        //fillLists();
        return scores[0]; 
    }
        
    //this adds the name and score to the dictionary
    //this is very important

    /*
    public void addToDictionary(int outcome, string dessegnation)
    {
        results[outcome] = dessegnation;

    }
    

    //this returns the dictionary
    public Dictionary<int, String> returnDictionary()
    {
        return results;
    }

    // this adds the name to name at the same place as the score
    public void addToNames()
    {
        names.Clear();
        int x;
        for (int i = 0; i < scores.Count; i++)
        {
            x = scores[i];
            names.Add(results[x]);
        }
    }
    */
        
    public List<String> getNamesList()
    {
        //clearList();
        //fillLists();
        return names;
    }

    public String getNameAt(int x)
    {
        //fillLists();
        return names[x];
    }

    public int getScoreAt(int z)
    {
        //fillLists();
        return scores[z];
    }

    public void clearList()
    {
        scores.Clear();
        names.Clear();
    }
    //returns a dictionary filled with the username of the player as the key and the score of the player as the value 
    //then you can use a list of usernames to go through the dictionary and get the score of the player.
    //do it backwards 
    //make the dictionary have a key of ints and return a string
    //then also have a list of scores in order using sort.
    //then go through the list of scores and use them as the key for the dictionary to get the username.

}
