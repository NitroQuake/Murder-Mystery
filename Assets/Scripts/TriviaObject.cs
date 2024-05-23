using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using TMPro;


public class TriviaObject : MonoBehaviour
{
    private Dictionary<string, string> trivia;
    public GameObject triviaPopup;
    public TextMeshProUGUI question;
    private int amountOfQ;
    public string[] answers;
    public string[] arrayQuestions;
    private PlayerObject playerObject;
    private string state;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<PlayerObject>();
        trivia = new Dictionary<string, string>();
        fillUpDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fills up the dictionary with questions
    public void fillUpDictionary()
    {
        trivia.Add("What NFL team did OJ Simpson not Play for? 1. New York Giants, 2. Buffalo Bills, 3. AFC All-Stars, 4. San Fransico 49ers", "New York Giants");
        trivia.Add("Gregor Clegane, from Game of Thrones, is also known as 1. The Goat 2. The Mountain 3. The Hound 4. The Bear", "The Mountain");
        trivia.Add("Robert Ressler coined the term \"serial killer\" while a member of the Criminal Apprehension Program in what organization during the 1970s?  1. The CIA 2. The US Coastguard 3. The Los Angeles Police Department 4. The FBI", "The FBI");
        trivia.Add("What is the name of the killer in the Friday the 13th franchise? 1. Freddy Krueger 2. Michael Myers 3. Jason Voorhees 4. Norman Bates", "Jason Vorhees");
        trivia.Add("Who are the villains in the Resident Evil franchise? 1. The Umbrella Corporation 2. The Illuminati 3. The Sinister Six 4. The Brady Bunch", "The Umbrella Corporation");
        trivia.Add("Who framed Rodger Rabbit? 1. Daffy Duck 2. Judge Doom 3. J Edgar Hoover 4. Scooby Doo", "Judge Doom");
        trivia.Add("Freddy Krueger is from which movie Franchise? 1. Friday the 13th 2. Halloween 3. Nightmare on Elm Street 4. Candyman", "Nightmare on Elm Street");
        trivia.Add("Who wrote The Silence of the Lambs? 1. Steven King 2. Marion Chesney 3. MC Beaton 4. Thomas Harris", "Thomas Harris");
        trivia.Add("How many Hamish MacBeth Mystery Books did MC Beaton, aka Marion Chesney, write? 1. 38 2. 3 3. 5 4. 1", "38");
        trivia.Add("How many times has Kenny died in South Park?  1. 314 2. 126 3. 0 4. 42", "126");
        trivia.Add("Which of the following Characters is still alive in the A Song of Ice and Fire book series? 1. John Snow 2. Quentyn Martell 3. Rickon Stark 4. Hoster Tully", "Rickon Stark");
        trivia.Add("In The Great Gatsby, who is not the perpetrator or victim of murder or manslaughter? 1. Daisy Buchanan 2. Myrtle Wilson 3. George Wilson 4. Tom Buchanan", "Tom Buchanan");
        trivia.Add("Which character is not in It by Steven King? 1. Beverly Marsh 2. Stan Marsh 3. Ben Hanscom 4. Henry Bowers", "Stan Marsh");
        trivia.Add("During the filming Sarumans death scene, who told Peter Jackson what a knife to the back sounds like? 1. J. R R. Tolkien 2. George R. R. Martin 3. Christopher Lee 4. Viggo Mortensen", "Christopher Lee");
        trivia.Add("Who is the Greek God of the Underworld? 1. Neptune 2. Mars 3. Zues 4. Hades", "Hades");
        trivia.Add("In South Park, which character yells \"Oh my God, they killed Kenny\" whenever Kenny dies? 1. Stan Marsh 2. Kyle Broflovski 3. Eric Cartman 4. Beverly Marsh", "Stan Marsh");
        trivia.Add("The Mountain is the pseudonym of which character in A Song of Ice and Fire? 1. Jeor Mormont 2. Gregor Clegane 3. Vargo Hoat 4. Jamie Lannister", "Gregor Clegane");
        trivia.Add("Who was the first person to coin the term \"serial killer\"? 1. J Edgar Hoover 2. Sherlock Holmes 3. Robert Ressler 4. Dave Toschi", "Robert Ressler");
        trivia.Add("Marion Chesney, aka MC Beaton, wrote 38 books in which of the following series? 1. The Hardy Boys 2. Sherlock Holmes 3. Nancy Drew 4. Hamish Macbeth", "Hamish Macbeth");
        trivia.Add("the Umbrella corporation is the main antagonist in which video game series? 1. Resident Evil 2. Doom 3. Overwatch 4. Call of Duty", "Resident Evil");
        trivia.Add("In The Great Gatsby, what bad thing does Tom Buchanon do? 1. Murder 2. Adultery 3. Manslaughter 4. Credit Card Fraud", "2. Adultery");
        trivia.Add("Which of the following is not a pseudonym used by Marion Chesney? 1. MC Beaton 2. Anne Fairfax 3. Richard Bachman 4. Helen Crampton", "Richard Bachman");
        trivia.Add("Who killed the Great Gatsby? 1. Daisy Buchanan 2. Myrtle Wilson 3. Tom Buchanan 4. George Wilson", "George Wilson");
        trivia.Add("Where did Jack the Ripper do his killings? 1. Whitechapel 2. North Kensington 3. White Harbor 4. Scottland", "Whitechapel");
        trivia.Add("In A Song of Ice and Fire, it is suggested that some Freys were baked into pies at which Location? 1.The Shire 2.White Harbor 3.Narnia 4.Scottland", "White Harbor"); 
        trivia.Add("The neighborhood of Whitechapel in London is known for which widely reviled resident? 1.Roose Bolton 2.James Corden 3.Jack the Ripper 4.Sauron", "Jack the Ripper"); 
        trivia.Add("Which of the following is not a horror movie? 1. Hereditary 2. Winnie the Pooh 3. The Visit 4. Deadpool", "Deadpool");
        trivia.Add("Which of the following characters can be killed like a normal person? 1. Saruman 2. Deadpool 3. Kenny McCormick 4. Ganondorf", "Saruman");
        trivia.Add("Who kills Saruman in The Lord of the Rings? 1. Ganondorf 2. Wormtongue 3. Link 4. Pipin", "Wormtongue");
        trivia.Add("Jamie Lannister got his nickname, Kingslayer, after killing which king? 1.Aegon 2.Aenys 3.Aerys 4.Aerion", "Aerys");
    }

    // shows or does not show the trivia pop up
    public void ifShowTrivia(bool showTrivia)
    {
        triviaPopup.SetActive(showTrivia);
    }

    // get trivia questions randomly until it reaches the amount needed
    public void getRandomQuestion(int amount)
    {
        setAmountOfQs(amount);
        arrayQuestions = new string[amount];
        answers = new string[amount];
        int[] checkForRepition = new int[amount];
        // fills checkforrepition to -1 because indexes cannot be negative numbers
        for(int i = 0; i < amount; i++)
        {
            checkForRepition[i] = -1;
        }
        for(int i = 0; i < amount; i++)
        {
            int random = UnityEngine.Random.Range(0, trivia.Count);
            int j = 0;
            // avoids repition of questions by checking index
            while(j < amount)
            {
                if (checkForRepition[j] == random)
                {
                    random = UnityEngine.Random.Range(0, trivia.Count);
                    j = 0;
                    continue;
                }
                j++;
            }
            // assign it to checkforrepition and arrayQuestions
            checkForRepition[i] = random;
            arrayQuestions[i] = trivia.ElementAt(random).Key;
        }
        setRandomQuestion(arrayQuestions[amount - 1]);
    }

    // displays question
    public void setRandomQuestion(string theQuestion)
    {
        question.text = theQuestion;
    }

    // sets the amount of questions
    public void setAmountOfQs(int num)
    {
        amountOfQ = num;
    }

    // sets the state for example, are these wumpus questions, purchase questions, or escape pit questions
    public void setState(string stateOfWhat)
    {
        state = stateOfWhat;
    }

    // returns the amount of correct answers the player got
    public int returnAmountCorrect()
    {
        int countCorrect = 0;
        for(int i = 0; i < arrayQuestions.Length; i++)
        {
            if (trivia[arrayQuestions[i]].ToLower() == answers[i].ToLower()) 
            { 
                countCorrect++;
            }
            arrayQuestions[i] = null;
            answers[i] = null;
        }
        return countCorrect;
    }

    // player answers all the questions
    public void answerQ(string answer)
    {
        amountOfQ--;
        answers[amountOfQ] = answer;
        // checks if there are more questions if so player continues to go through all the questions, thye last questions show up first
        if(amountOfQ > 0)
        {
            setRandomQuestion(arrayQuestions[amountOfQ - 1]);
        } else 
        {
            // updates in playerObject concerning if they purchased or escaped
            triviaPopup.SetActive(false);
            if (state.Equals("ForBullets"))
            {
                playerObject.purchaseBullets(returnAmountCorrect());
            }
            else if (state.Equals("ForSecret"))
            {
                playerObject.purchaseSecret(returnAmountCorrect());
            }
            else if (state.Equals("ForEscapePit"))
            {
                playerObject.didPlayerEscape(returnAmountCorrect());
            } else if(state.Equals("ForWumpus"))
            {
                playerObject.didPlayerEscapeWumpus(returnAmountCorrect());
            }
        }
    }
}
