using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour {

	float angle = 0;
	public float maxSpeed = 7;
    public float minSpeed = .2f;
	float acceleration = 0;
    [HideInInspector]
	public bool rolling = false;

    int currentFieldIndex = 0;
    int currentField = 3;
    int rotations;
    float rotatedAngle;
    float newRot;

    Transform transform;
    public GameManager gameManager;
    
	void Start ()
	{
        transform = GetComponent<Transform>();
        //  Przyspieszenie obrotów koła maszyny podczas losowania
        acceleration = 5;
        //  Ustalenie o jaki kąt należy obrócić koło maszyny żeby wskazać kolejny element losowania
        angle = 360 / GameManager.numberOfFieldsOnSlot;
	}
	
	void Update ()
	{
        if (rolling)
        {            
            if (rotatedAngle < angle * rotations)
            {
                //  Jeśli zostało więcej niż 10 obrotów przyspieszaj o acceleration na sekundę, ale prędkość nie może przekroczyć maxSpeed
                //  Jeśli zostało mniej niż 10 obrotów zmniejszaj płynnie prędkość, ale nie schodź poniżej minSpeed (w ostatniej klatce zwalniania)
                if (angle * rotations - rotatedAngle > 10 * angle)
                    newRot = Mathf.Clamp(newRot + acceleration * Time.deltaTime, 0, maxSpeed);
                else
                    newRot = maxSpeed * (angle * rotations - rotatedAngle) / (10 * angle) + minSpeed;
                
                //  Obrót o newRot
                transform.Rotate(0, newRot, 0);
                rotatedAngle += newRot;
            }
            else
            {
                //  Na koniec obrotu wyzeruj zmienne przed kolejnym losowaniem
                rolling = false;
                rotatedAngle = 0;
                rotations = 0;

                //  Oblicz wynik punktowy losowania i wyświetl go na ekranie
                gameManager.UpdateScore();
            }
        }
	}

    //  Funkcja losująca na podstawie parametru całkowitoliczbowego znaczącego ilość obrotów, jakie mają zostać wykonane
    public int RollSlotByRotations(int rot)
    {
        rotations = rot;
        rotatedAngle = 0;
        rolling = true;

        //  Oblicz jaki będzie wynik losowania. numberOfFieldsOnSlot dzielone jest przez 2, ponieważ tekstura powtarza pola 1 - 5 dwa razy
        currentFieldIndex = (currentFieldIndex + rot) % (GameManager.numberOfFieldsOnSlot / 2);
        currentField = gameManager.fieldValues[currentFieldIndex];

        //  DEBUG
        print("Frame " + Time.frameCount + ": " + name + " obróci się " + rot + " razy. Wynik: " + currentField);

        //  Zwróć wynik losowania
        return currentField;
    }
}
