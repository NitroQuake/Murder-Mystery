using System;
using System.Collections;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerObject : MonoBehaviour
{
    private int bullets;
    private int coins;
    private int turns;
    private int score;
    public int currRoom;
    private string playerName;

    private MapObject mapObject;
    private GameControlObject gameControlObject;
    public GameObject originOfGun;
    private TriviaObject triviaObject;
    public Sound sound;
    private PlayerMovement playerMovement;
    public TextMeshProUGUI coinsDisplay;
    public TextMeshProUGUI turnsDisplay;
    public TextMeshProUGUI bulletsDisplay;

    public void Start()
    {
        this.score = 100;
        this.bullets = 3;
        this.turns = 0;
        this.coins = 10;
        setPlayerLocation();
        this.currRoom = getPlayerLocation();
        this.playerName = "";

        mapObject = GameObject.Find("GameControlObject").GetComponent<MapObject>();
        gameControlObject = GameObject.Find("GameControlObject").GetComponent<GameControlObject>();
        triviaObject = GameObject.Find("TriviaObject").GetComponent<TriviaObject>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        coinsDisplay.text = "Coins: " + coins;
        turnsDisplay.text = "Turns: " + turns;
        bulletsDisplay.text = "Bullets: " + bullets;

        // checks if coins are less than 0 and if so end game
        if(coins < 0)
        {
            //game over
            playerMovement.canPlayerMove(false);
            gameControlObject.gameOverEvent(0);
        }
    }

    public void setPlayerObject(String name)
    {
        this.score = 100;
        this.bullets = 3;
        this.turns = 0;
        this.coins = 0;
        this.currRoom = getPlayerLocation();
        this.playerName = name;
    }

    // equips gun 
    public void equipGun()
    {
        originOfGun.SetActive(true);
    }

    // dequips gun
    public void dequipGun()
    {
        originOfGun.SetActive(false);
    }

    // if player got more than 2 questions correct, player purchased bullets
    public void purchaseBullets(int amountCorrect)
    {
        if(amountCorrect >= 2) 
        {
            playerMovement.canPlayerMove(true);
            coins -= 3;
            turns++;
            bullets += 2;
        } else
        {
            playerMovement.canPlayerMove(true);
            coins -= 3;
            turns++;
        }
    }

    // if player got more than 2 questions correct, player purchased secret
    public void purchaseSecret(int amountCorrect)
    {
        if (amountCorrect >= 2)
        {
            playerMovement.canPlayerMove(true);
            coins -= 3;
            turns++;
            string secret = mapObject.returnSecret(UnityEngine.Random.Range(0, 5), currRoom);
            gameControlObject.setSecret(secret);
        }
        else
        {
            playerMovement.canPlayerMove(true);
            coins -= 3;
            turns++;
        }
    }

    // if player got more than 2 questions correct, player escaped, but if not game over
    public void didPlayerEscape(int amountCorrect)
    {
        if (amountCorrect >= 2)
        {
            coins -= 3;
            playerMovement.canPlayerMove(true);
            transform.position = new Vector2(0, 11.5f);
            setPlayerLocation();
        }
        else
        {
            //game over
            playerMovement.canPlayerMove(false);
            gameControlObject.gameOverEvent(0);
        }
    }

    // if player got more than 3 questions correct, player beat wumpus and set random position for wumpus, but if not game over
    public void didPlayerEscapeWumpus(int amountCorrect)
    {
        if (amountCorrect >= 3)
        {
            coins-=5;
            int[] bottomlessPits = mapObject.returnBottomlessPits();
            int[] gangsters = mapObject.returnBats();
            playerMovement.canPlayerMove(true);
            int random = UnityEngine.Random.Range(1, 31);
            // makes sure the wumpus doesn't spawn in room that is a pit, gangster, player positon, or wumpus
            while(random == currRoom || random == mapObject.getMurdererPosition() || random == bottomlessPits[0] || random == bottomlessPits[1] || random == bottomlessPits[2] || random == gangsters[0] || random == gangsters[1] || random == gangsters[2])
            {
                random = UnityEngine.Random.Range(1, 31);
            }
            mapObject.setMurdererPosition(random);
        }
        else
        {
            //game over
            playerMovement.canPlayerMove(false);
            gameControlObject.gameOverEvent(0);
        }
    }

    // displays trivia for bullets
    public void showTriviaForBullet()
    {
        playerMovement.canPlayerMove(false);
        triviaObject.ifShowTrivia(true);
        triviaObject.getRandomQuestion(3);
        triviaObject.setState("ForBullets");
    }

    // displays trivia for secret
    public void showTriviaForSecret()
    {
        playerMovement.canPlayerMove(false);
        triviaObject.ifShowTrivia(true);
        triviaObject.getRandomQuestion(3);
        triviaObject.setState("ForSecret");
    }

    // displays trivia for pit
    public void showTriviaForPit()
    {
        playerMovement.canPlayerMove(false);
        triviaObject.ifShowTrivia(true);
        triviaObject.getRandomQuestion(3);
        triviaObject.setState("ForEscapePit");
    }

    // displays trivia for wumpus
    public void showTriviaForWumpus()
    {
        playerMovement.canPlayerMove(false);
        triviaObject.ifShowTrivia(true);
        triviaObject.getRandomQuestion(5);
        triviaObject.setState("ForWumpus");
    }

    // updates bullet fields
    public void shootBullet()
    {
        bullets--;
        turns++;           
    }

    // update player location to first room 
    public void setPlayerLocation()
    {
        this.currRoom = 1;
    }

    // when player moves from room to rom
    public void move(int loc) //move player method
    {
        dequipGun();
        coins++;
        turns++;
        currRoom = loc;
        int[] bats = mapObject.returnBats();
        int[] bottomlessPits = mapObject.returnBottomlessPits();
        // Moves the player if it hits a bat or produces trivia when at a pit or fights wumpus
        sound.playDoorOpen();
        for(int i = 0; i < 3; i++)
        {
            if(currRoom == mapObject.getMurdererPosition())
            {
                sound.playJumpscare();
                showTriviaForWumpus();
            } else if(currRoom == bats[i])
            {
                sound.playHenchmen();
                mapObject.batChangeRoom(i);
                int random = UnityEngine.Random.Range(1, 31);
                // makes sure not to spawn in a pit, a wumpus, or a gangster
                while (random == bottomlessPits[0] || random == bottomlessPits[1] || random == bottomlessPits[2] || random == bats[0] || random == bats[1] || random == bats[2] || random == mapObject.getMurdererPosition())
                {
                    random = UnityEngine.Random.Range(1, 31);
                }
                currRoom = random;
                Vector2 newLoc = gameControlObject.getRoomPosition(random);
                gameObject.transform.position = newLoc;
            } else if(currRoom == bottomlessPits[i])
            {
                sound.playTrapDoor();
                showTriviaForPit();
            }
        }
        gameControlObject.checkSurroundingRooms(currRoom);
    }

    public String getPlayerName()  //Returns players
    {
        return this.playerName;
    }

    public int getPlayerLocation()  //Returns current rooms
    {
        return this.currRoom;
    }

    public int getTurns()  //Returns amount of turns a player has done
    {
        return this.turns;
    }

    public int getCoins()  //Returns amount of coins a player has
    {
        return this.coins;
    }

    public int getBullets()  //Returns amount of bullets a player has
    {
        return this.bullets;
    }

    public Array getInventory()  //Creates array of player inventory to be displayed
    {
        String[] inv = { "bullets = " + this.bullets, "coins = " + this.coins, "turns taken = " + this.turns };
        return inv;
    }

    public int computeScore()  //Calculates players score
    {
        this.score = 100 - this.turns + this.coins + (10 * this.bullets);
        return this.score;
    }
}
