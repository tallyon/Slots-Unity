using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUpdate : MonoBehaviour
{
    int totalScore;
    public int[] scoredFields;
    public int costOfPlaying = 2;

    Text textField;
    public GameManager gameManager;

    public void Start()
    {
        textField = GetComponent<Text>();
        scoredFields = new int[GameManager.numberOfFieldsOnSlot/2];
        totalScore = 10;
        textField.text = "Score: " + totalScore;
    }

    //  Oblicz wynik losowania i wypisz go na ekranie
    public void UpdateScore()
    {
        //  Sprawdź jaka wartość została wylosowana, znajdź tę wartość w gameManager.fieldValues, pobierz jej indeks i inkrementuj ten sam indeks
        //  tablicy scoredFields w tej klasie
        print("DrawnFields: " + gameManager.drawnFields.ToString());
        foreach (int draw in gameManager.drawnFields)
        {
            for (int i = 0; i < scoredFields.Length; i++)
            {
                if (gameManager.fieldValues[i] == draw)
                {
                    scoredFields[i]++;
                    print("Trafiono " + draw + "! To " + scoredFields[i] + " trafienie! i: " + i);
                }
            }
        }

        //  Dla każdego elementu pomnóż scoreFields[i] z gameManager.fieldValues[i] i dodaj do totalScore
        int newScore = 0;

        for(int i = 0; i < scoredFields.Length; i++)
        {
            //  Zalicz trafienie tylko kiedy trafiono wartość przynajmniej dwa razy
            if(scoredFields[i] > 1)
                newScore += scoredFields[i] * gameManager.fieldValues[i];
        }

        totalScore += newScore;

        //  Uaktualnij wynik w polu tekstowym w interfejsie
        textField.text = "Score: " + totalScore;

        //  Wyczyść tablicę z liczbą trafień wartości scoredFields
        for (int i = 0; i < scoredFields.Length; i++)
        {
            scoredFields[i] = 0;
        }
    }

    //  Odejmij koszt losowania od wyniku, jeśli 0 lub mniej zwróc false i napisz Game Over!
    public bool PayToPlay()
    {
        totalScore -= costOfPlaying;

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
