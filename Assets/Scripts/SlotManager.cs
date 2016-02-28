using UnityEngine;

public class SlotManager : MonoBehaviour
{
    // Angle that the roller has to turn to draw the next field
    private float angle = 360 / GameManager.numberOfFieldsOnSlot;
    // Speed and acceleration of the roller
    private float maxSpeed = 7;
    private float minSpeed = .2f;
    private float acceleration = 5;
    [HideInInspector]
    private bool rolling;
    public bool Rolling
    {
        get
        {
            return rolling;
        }
        protected set
        {
            rolling = value;
        }
    }

    private int currentFieldIndex = 0;
    private int currentField = 3;
    private int rotations;
    private float rotatedAngle;
    private float newRot;

    private Transform trans;
    private GameManager gameManager;

    void Start()
    {
        trans = GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Rolling)
        {
            if (rotatedAngle < angle * rotations)
            {
                //  Jeśli zostało więcej niż 10 obrotów przyspieszaj o acceleration na sekundę, ale prędkość nie może przekroczyć maxSpeed
                //  Jeśli zostało mniej niż 10 obrotów zmniejszaj płynnie prędkość, ale nie schodź poniżej minSpeed (w ostatniej klatce zwalniania)
                // If there are more than 10 rotations left accelerate, but clamp rotation speed
                if (angle * rotations - rotatedAngle > 10 * angle)
                    newRot = Mathf.Clamp(newRot + acceleration * Time.deltaTime, 0, maxSpeed);
                else
                    // If there are less than 10 rotations left decclerate and clamp rotation speed to minSpeed
                    newRot = maxSpeed * (angle * rotations - rotatedAngle) / (10 * angle) + minSpeed;

                // Rotate
                trans.Rotate(0, newRot, 0);
                rotatedAngle += newRot;
            }
            else
            {
                Rolling = false;
                rotatedAngle = 0;
                rotations = 0;

                // Notify GameManager that rolling has ended
                gameManager.RollerFinished();
            }
        }
    }

    /// <summary>
    /// Evaluate what value will be drawn when there will be /rot/ rotations
    /// </summary>
    /// <param name="rot">Number of rotations</param>
    /// <returns></returns>
    public int RollSlotByRotations(int rot)
    {
        rotations = rot;
        rotatedAngle = 0;
        Rolling = true;

        // Evaluate what field will be drawn. numberOfFieldsOnSlot has doubled all the values - it has to be divided by 2
        currentFieldIndex = (currentFieldIndex + rot) % (GameManager.numberOfFieldsOnSlot / 2);
        currentField = gameManager.FieldValues[currentFieldIndex];

        //  DEBUG
        //print("Frame " + Time.frameCount + ": " + name + " obróci się " + rot + " razy. Wynik: " + currentField);

        return currentField;
    }
}
