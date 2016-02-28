using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField]
    private int initialScore;
    private int totalScore;
    private int[] scoredFields;
    [SerializeField]
    public int costOfPlaying;

    Text textField;
    public GameManager gameManager;

    public void Start()
    {
        textField = GetComponent<Text>();
        // All the numbers on each roller of the slot are doubled, so there are half as many numbers as number of fields
        scoredFields = new int[GameManager.numberOfFieldsOnSlot / 2];
        totalScore = initialScore;
        textField.text = "Score: " + totalScore;
    }

    /// <summary>
    /// Check drawn fields, evaluate score and update UI
    /// </summary>
    public void UpdateScore()
    {
        // For every drawn field on the slot machine check how many times this exact field occured and store number of occurencies in scoredFields' appropriate index
        foreach (int draw in gameManager.DrawnFields)
        {
            for (int i = 0; i < scoredFields.Length; i++)
            {
                if (gameManager.FieldValues[i] == draw)
                {
                    scoredFields[i]++;
                    //Debug.Log("Trafiono " + draw + "! To " + scoredFields[i] + " trafienie! i: " + i);
                }
            }
        }

        int newScore = 0;
        for (int i = 0; i < scoredFields.Length; i++)
        {
            // If scored field was drawn at least twice add to the score score field's value multiplied by number of occurencies
            if (scoredFields[i] > 1)
                newScore += scoredFields[i] * gameManager.FieldValues[i];
        }

        totalScore += newScore;

        // Update UI
        textField.text = "Score: " + totalScore;

        // Cler the array of scored fields
        for (int i = 0; i < scoredFields.Length; i++)
        {
            scoredFields[i] = 0;
        }
    }

    /// <summary>
    /// Subtract score to play the slots machine
    /// </summary>
    /// <returns></returns>
    public bool PayToPlay()
    {
        totalScore -= costOfPlaying;

        // If there are not enough points to pay for playing - Game over
        if (totalScore <= 0)
        {
            textField.text = "Game Over!";
            return false;
        }
        else
        {
            textField.text = "Score: " + totalScore;
            return true;
        }
    }
}
