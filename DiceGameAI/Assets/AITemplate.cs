using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


// Your AI will need to access the GameManager script on the object in the scene.
// You will have access to the following methods:
// void RollDice(): This method with roll the dice for a couple seconds.
// bool IsRolling(): This method will return a bool that tells you if the dice are currently rolling.
// void SetComboActive(int index, bool state): This method will set the interactable state of a combo button at a specified index. It will only do this if the combo has not yet been selected.
// void SelectCombo(int index): This will try to select the combo by index. You can use the enum DiceCombos and cast it to an int. eg. (int)GameManager.DiceCombos.LargeStraight
// void KeepDie(int index): This will toggle the keep button at the index.
// void GetDiceValues(ref int[] values): This will point the array given to the diceValues in the GameManager.
// bool IsComboSelected(int index): This will return if the combo has been selected 


public class AITemplate : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button aiButton;
    [SerializeField] AIStates currentState = AIStates.RollDice1;
    [SerializeField] int[] diceValues = new int[5];
    [SerializeField] int[] diceCount = new int[6];

    [SerializeField] int onePair, twoPair, threeKind, fourKind, fullHouse, smallStraight, largeStraight;
    [SerializeField] int numInStraight, currentRun, partialStraight;
    enum AIStates
    {
        RollDice1,
        EvaluateDice1,
        KeepDice,
        RollDice2,
        EvaluateDice2
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsRolling() && aiButton.interactable == false)
        {
            CheckCombos();
            SelectCombos();
            aiButton.interactable = true;
        }
    }

    public void TakeAIStep()
    {
        switch (currentState)
        {
            case AIStates.RollDice1:
                gameManager.RollDice(); //Push the roll the dice button.
                aiButton.interactable = false;
                UpdateComboButtons();
                currentState = AIStates.KeepDice; // not going to have each on diffrent button presses. on one press it should roll, eval, and keep
                break;
            case AIStates.EvaluateDice1:
                CheckCombos();
                SelectCombos();
                break;
            case AIStates.KeepDice://HELL YEAH!
                if (!gameManager.IsComboSelected(5) && !gameManager.IsComboSelected(4) && numInStraight > 2) //HOLY SHIT ITS WORKING! IM KEEPING PARTIAL STRAIGHTS
                {
                    int m = 0;
                    for (int i = 0; i < numInStraight; i++)
                    {
                        
                        for (int l = 0; l < diceValues.Length; l++)
                        {
                            if (diceValues[l] == partialStraight - m)
                            {
                                gameManager.KeepDie(l);
                                l = diceValues.Length;
                            }
                        }
                        m++;
                    }
                }


                if (gameManager.IsComboSelected(5) && gameManager.IsComboSelected(4) || numInStraight < 3)
                    {
                        //this is the locgic for keeping paris.
                        if (twoPair > -1)
                        {
                            if (gameManager.IsComboSelected(3) && gameManager.IsComboSelected(0)) //if FH and TP are done this will only keep one set of the same dice
                            {
                                for (int i = 0; i < diceValues.Length; i++)
                                {
                                    if (diceValues[i] == onePair)
                                    {
                                        gameManager.KeepDie(i);
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < diceValues.Length; i++)
                                {
                                    if (diceValues[i] == onePair)
                                    {
                                        gameManager.KeepDie(i);
                                    }
                                    if (diceValues[i] == twoPair)
                                    {
                                        gameManager.KeepDie(i);
                                    }
                                }
                            }
                        }
                        else if (onePair > -1)
                        {
                            for (int i = 0; i < diceValues.Length; i++)
                            {
                                if (diceValues[i] == onePair)
                                {
                                    gameManager.KeepDie(i);
                                }
                            }
                        }
                    }                
                break;
            case AIStates.RollDice2:
                                                
                break;
            case AIStates.EvaluateDice2:
                                                
                break;
            default:
                break;
        }
    }

    void CheckCombos()
    {
        //initialize combo varibales
        onePair = -1;
        twoPair = -1;
        threeKind = -1;
        fourKind = -1;
        fullHouse = -1;
        smallStraight = -1;
        largeStraight = -1;

        numInStraight = 0;
        currentRun = 0;
        partialStraight = 0;

        //get dice values
        gameManager.GetDiceValues(ref diceValues);

        //reset dice array
        for (int i = 0; i < diceCount.Length; i++)
        {
            diceCount[i] = 0;
        }
        //count the dice
        for (int i = 0; i < diceValues.Length; i++)
        {
            diceCount[diceValues[i]]++;
        }
        //evaluate dice values to check for active combos
        for (int i = 0; i < diceCount.Length; i++)
        {
            if (diceCount[i] >= 2)
            {
                if (onePair == -1)
                {
                    onePair = i;
                }
                else
                {
                    twoPair = i;
                    gameManager.SetComboActive(0, true);
                }
            }
            if (diceCount[i] >= 3)
            {
                threeKind = i;
                gameManager.SetComboActive(1, true);
            }
            if (diceCount[i] >= 4)
            {
                fourKind = i;
                gameManager.SetComboActive(2, true);
            }
            if (twoPair > -1 && threeKind > -1)
            {
                fullHouse = 0;
                gameManager.SetComboActive(3, true);
            }
            //identify straight
            if (diceCount[i] > 0)
            {
                currentRun++;
            }
            else
            {
                currentRun = 0;
            }
            if (currentRun == 3)
            {
                numInStraight = currentRun;
                partialStraight = i;
            }
            if (currentRun == 4)
                {
                    numInStraight = currentRun;
                    partialStraight = i;
                    smallStraight = 1;
                    gameManager.SetComboActive(4, true);
                }
            if (currentRun == 5)
            {
                numInStraight = currentRun;
                largeStraight = 0;
                gameManager.SetComboActive(5, true);
            }

            // if (i < 3) //this works to find the straights but there is probaby a better way to do this.
            //     {
            //         if (diceCount[i] >= 1 && diceCount[i + 1] >= 1 && diceCount[i + 2] >= 1 && diceCount[i + 3] >= 1)
            //         {
            //             smallStraight = i;
            //             gameManager.SetComboActive(4, true);
            //         }
            //     }
            // if (i < 2)
            // {
            //     if (diceCount[i] >= 1 && diceCount[i + 1] >= 1 && diceCount[i + 2] >= 1 && diceCount[i + 3] >= 1 && diceCount[i + 4] >= 1)
            //     {
            //         largeStraight = i;
            //         gameManager.SetComboActive(5, true);
            //     }
            // }

        }
    }

    void SelectCombos()
    {
        if (largeStraight > -1 && !gameManager.IsComboSelected(5))
        {
            gameManager.SelectCombo(5);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
        else if (fullHouse > -1 && !gameManager.IsComboSelected(3))
        {
            gameManager.SelectCombo(3);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
        else if (fourKind > -1 && !gameManager.IsComboSelected(2))
        {
            gameManager.SelectCombo(2);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
        else if (smallStraight > -1 && gameManager.IsComboSelected(5) && !gameManager.IsComboSelected(4))
        {
            gameManager.SelectCombo(4);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
        else if (threeKind > -1 && gameManager.IsComboSelected(2) && gameManager.IsComboSelected(3) && !gameManager.IsComboSelected(1))
        {
            gameManager.SelectCombo(1);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
        else if (twoPair > -1 && gameManager.IsComboSelected(5) && gameManager.IsComboSelected(4) && gameManager.IsComboSelected(3) && gameManager.IsComboSelected(2) && gameManager.IsComboSelected(1) && !gameManager.IsComboSelected(0))
        {
            gameManager.SelectCombo(0);
            currentState = AIStates.RollDice1;
            UpdateComboButtons();
        }
    }

    void UpdateComboButtons()
    {
        for (int i = 0; i < 6; i++)
        {
            gameManager.SetComboActive(i, false);
        }
    }
}
