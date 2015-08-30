using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
	public Arm arm;

    //  Ilość kół obrotowych w maszynie
    public const int numberOfSlotRollers = 3;
    //  Ilość WSZYSTKICH pól na kole obrotowym maszyny
    public const int numberOfFieldsOnSlot = 10;
    //  Tablica wartości każdego z pól na kole obrotowym maszyny
    public int[] fieldValues = { 3, 2, 1, 5, 4, 3, 2, 1, 5, 4 };
    //  Tablica wartości wylosowanych w ostatnim losowaniu pól
	[HideInInspector]
    public int[] drawnFields = new int[numberOfSlotRollers];
    //  Tablica skryptów SlotManager na każdym z kół obrotowych maszyny ustawionych od lewej do prawej
    public SlotManager[] slotManager = new SlotManager[numberOfSlotRollers];
    //  Zmienna przechowuje informacje ile kół maszyny skończyło się kręcić. Kiedy skończą wszystkie (finishedRolling == numberOfSlotRollers) wywołaj funkcje
    //  uaktualniającą wynik w ScoreUpdate
	[HideInInspector]
    public int finishedRolling;

	[HideInInspector]
    public bool gameOver;
    bool autoplay;

    public ScoreUpdate scoreUpdateScript;

	void Update()
	{
        if (gameOver)
            return;

		if (Input.GetKeyDown (KeyCode.Space))
		{
			Play ();
		}

        //  Autoplay
        if(autoplay)
            Play();
	}

    //  Obsługa logiki gry
	void Play()
	{
        foreach(SlotManager slot in slotManager)
        {
            if (slot.rolling)
                return;
        }

        //  Wylosuj ilość obrotów dla każdego koła
        Random.seed = System.DateTime.Now.Millisecond;
        int slot1Rotations = Random.Range(17, 27);

        //  Wywołanie animacji losowanie dla każdego koła obrotowego maszyny
        for (int i = 0; i < slotManager.Length; i++)
        {
            //  Losuj i zapisz zwrócony wynik losowania w tablicy drawnFields
            drawnFields[i] = slotManager[i].RollSlotByRotations(slot1Rotations);
            //  Na zmianę dodawaj [3,7] lub [1,4] obrotów do kolejnego koła
            slot1Rotations += (i % 2 == 1) ? Random.Range(3, 7) : Random.Range(1, 4);
        }

        //  Animacja ramienia maszyny
        arm.ArmAnimate ();

        //  Poniesienie kosztu losowania, jeśli zabrakło funduszy (score) ustaw gameOver na true
        gameOver = !scoreUpdateScript.PayToPlay();
	}

    //  Inkrementuj licznik kół, które zakończyły animację i jeśli wszystkie skończyły wywołaj UpdateScore() i zresetuj licznik
    public void UpdateScore()
    {
        //  Któreś koło oborotowe skończyło się obracać
        finishedRolling++;

        //  Jeśli wszystkie koła skończyły się obracać wywołaj funkcję uaktualniającą wynik w ScoreUpdate i wyzeruj finishedRolling przed następnym losowaniem
        if (finishedRolling == numberOfSlotRollers)
        {
            scoreUpdateScript.UpdateScore();
            finishedRolling = 0;
        }
    }

    //  Przestawia wartość logiczną zmiennej autoplay; wywołana przez przyciśnięcie przycisku w grze
    public void ToggleAutoplay()
    {
        autoplay = !autoplay;
    }

	//	Kończy działanie aplikacji. Po wciśnięciu przycisuk Exit w Menu
	public void ExitGame()
	{
		Application.Quit ();
	}
}
