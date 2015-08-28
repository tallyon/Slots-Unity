using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	SlotManager slotManager1, slotManager2, slotManager3;
	Arm arm;
    
	void Start ()
	{
		slotManager1 = GameObject.Find("Slot1").GetComponent<SlotManager> ();
		slotManager2 = GameObject.Find("Slot2").GetComponent<SlotManager> ();
		slotManager3 = GameObject.Find("Slot3").GetComponent<SlotManager> ();
		arm = GameObject.Find ("Arm").GetComponent<Arm>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			Play ();
		}
	}

	void Play()
	{
		//Jeśli animacja losowania nie zakończyła się wyjdź z funkcji
		if (slotManager1.rolling || slotManager2.rolling || slotManager3.rolling)
			return;

        //Wylosuj ilość obrotów dla każdego koła
        int slot1Rotations, slot2Rotations, slot3Rotations;

		Random.seed = System.DateTime.Now.Millisecond;
        slot1Rotations = Random.Range(12, 27);
        slot2Rotations = slot1Rotations + Random.Range(3, 7);
        slot3Rotations = slot2Rotations + Random.Range(1, 4);

        //Wywołanie animacji losowanie dla każdego koła
        //slot1
        slotManager1.RollSlotByRotations(slot1Rotations);
		//slot2		
		slotManager2.RollSlotByRotations (slot2Rotations);
		//slot3		
		slotManager3.RollSlotByRotations (slot3Rotations);

        //Animacja ramienia maszyny
        arm.ArmAnimate ();
	}
}
