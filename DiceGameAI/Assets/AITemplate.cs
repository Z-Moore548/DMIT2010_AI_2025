using TMPro;
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

    [SerializeField] AIStates currentState = AIStates.RollDice1;

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
        
    }

    public void TakeAIStep()
    {
        switch (currentState)
        {
            case AIStates.RollDice1:
                gameManager.RollDice(); //Push the roll the dice button.
                currentState = AIStates.EvaluateDice1;
                break;
            case AIStates.EvaluateDice1:
                Debug.Log("Gamers");
                                                // Count and store in an array how many of each value there are.
                                                // If you have 5 numbers in a sequence & haven’t completed LS, select LS 
                                                // Else If you have 4 of a single num & haven’t completed it FK, select FK 
                                                // Else If you have 3 of a single num and 2 of a different num & haven’t completed it FH, select FH 

                                                // Else If LS is done, and you have 4 numbers in sequence & haven’t completed SS, select SS 
                                                // Else If FK and FH are done, and you have 3 of a single num & haven’t completed TK, select TK 
                                                // Else if LS, SS, FK, FH, and TK are done, and you have 2 of a single num and 2 of a different num & haven’t completed TP, select TP 
                                                // If a combo was selected, 
                                                // check if all combos are complete.
                                                // 	End game
                                                // If not  
                                                //  End turn                

                break;
            case AIStates.KeepDice:
                                                // If SS or LS is not done, check the array for three or more numbers in sequence.
                                                // 	If there is, keep those numbers (go next)
                                                // If FK, FH, TK, and TP are done, check for 2 or more number is sequence.
                                                // 	If there is, keep those numbers (go next)
                                                // If SS and LS are done, or there aren’t three numbers in sequence. Check the array for any numbers where there is more than one.
                                                // 	If there are multiple duplicate numbers. Check if TP and FH is done
                                                // 		If they are not done, keep both pairs (go next)
                                                // 		If they are done keep one set of duplicates, however many they may be. (go next)
                                                // 	If only one set of duplicates
                                                // 		Keep that set of duplicates (go next)

                break;
            case AIStates.RollDice2:
                                                //Push the roll the dice button
                break;
            case AIStates.EvaluateDice2:
                                                // Count and store in an array how many of each value there are.
                                                // If you have 5 numbers in a sequence & haven’t completed LS, select LS 
                                                // Else If you have 4 numbers in a sequence & haven’t completed SS, select SS 
                                                // Else If you have 4 of a single num & haven’t completed FK, select FK 
                                                // Else If you have 3 of a single num and 2 of a different num & haven’t completed it FH, select FH 
                                                // Else If you have 3 of a single num & haven’t completed TK, select TK 
                                                // Else If you have 2 of a single num and 2 of a different num & haven’t completed TP, select TP 
                                                // check if all combos are complete.
                                                // End game
                                                // Else 
                                                // End turn

                break;
            default:
                break;
        }
    }

    void CheckCombos()
    {
        
    }

    void UpdateComboButtons()
    {
        
    }
}
