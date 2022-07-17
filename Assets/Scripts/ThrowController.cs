using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [Header("Properties")]
    public int dicesToThrow = 2;
    public float timeToWait = 2.0f;
    [Header("UI elements")]
    public GameObject timerHeaderText;
    public GameObject timerText;
    public GameObject resultsText;
    public GameObject acceptedText;
    public GameObject throwButton;
    public GameObject diceOne;
    public GameObject diceTwo;
    [Header("Dice properties")]
    public int minNumber = 1;
    public int maxNumber = 6;
    public int maxThrows = 5;

    private float remainingTime;
    private bool throwed;
    private int throws;
    private Automaton automaton;

    private void Start()
    {
        // Assign default values
        remainingTime = timeToWait;
        throwed = false;
        throws = 0;
        // Instantiate automaton
        automaton = new Automaton();
    }

    private void Update()
    {
        if (throwed)
        {
            if (remainingTime > 0)
            {
                timerText.GetComponent<Text>().text = string.Format("{0} SEGUNDOS", (int)remainingTime);

                remainingTime -= Time.deltaTime;
            }
            else
            {
                throwed = false;
                remainingTime = timeToWait;

                ShowResults();
            }
        }
    }

    public void ThrowDices()
    {
        if (throwButton.GetComponentInChildren<Text>().text == "COMENZAR DE NUEVO")
        {
            throwButton.GetComponentInChildren<Text>().text = "TIRAR DADOS";
            resultsText.GetComponent<Text>().text = "";
            acceptedText.SetActive(false);

            diceOne.GetComponent<ThrowDice>().ResetDice();
            diceTwo.GetComponent<ThrowDice>().ResetDice();

            return;
        }

        throwed = true;

        timerHeaderText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        throwButton.GetComponent<Button>().enabled = false;
    }

    private void ShowResults()
    {
        if (throws >= 5)
        {
            automaton = new Automaton();

            resultsText.GetComponent<Text>().text = "";

            throws = 0;
        }

        timerHeaderText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        throwButton.GetComponent<Button>().enabled = true;

        int firstDice = Random.Range(minNumber, maxNumber + 1);
        int secondDice = Random.Range(minNumber, maxNumber + 1);

        char firstDiceType = GetAutomatonCharacter(firstDice);
        char secondDiceType = GetAutomatonCharacter(secondDice);

        resultsText.GetComponent<Text>().text += string.Format("{0} - {1}\n{2} - {3}\n", firstDice, secondDice, firstDiceType, secondDiceType);

        // Set on position
        diceOne.GetComponent<ThrowDice>().SetFaceUp(firstDice);
        diceTwo.GetComponent<ThrowDice>().SetFaceUp(secondDice);

        throws++;

        // Check automaton
        string automatonString = firstDiceType.ToString() + secondDiceType.ToString();

        bool endReached = automaton.WalkWithAutomaton(automatonString);

        if (!acceptedText.activeSelf)
        {
            acceptedText.SetActive(true);
        }

        if (endReached)
        {
            acceptedText.GetComponent<Text>().text = "ESTADO ACEPTADO";
        }
        else
        {
            acceptedText.GetComponent<Text>().text = "ESTADO NO ACEPTADO";
        }

        if (throws >= 5)
        {
            throwButton.GetComponentInChildren<Text>().text = "COMENZAR DE NUEVO";
        }
    }

    private char GetAutomatonCharacter(int myNumber)
    {
        if (myNumber >= 1 && myNumber <= 3)
        {
            return 'L';
        }
        else
        {
            return 'H';
        }
    }
}
