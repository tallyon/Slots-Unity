using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour {

	public float angle = 0;
	public float maxSpeed = 8;
	public float acceleration = 0;
	public bool rolling = false;

    float rotations;
    float rotatedAngle;
    float newRot;

    //  Ilość WSZYSTKICH pól na kole obrotowym maszyny
    int numberOfFieldsOnSlot = 10;

    Transform transform;
    
	void Start ()
	{
        transform = GetComponent<Transform>();
		acceleration = Random.Range(1.0f, 3.0f);
        angle = 360 / numberOfFieldsOnSlot;
	}
	
	void Update ()
	{
        if (rolling)
        {            
            if (rotatedAngle < angle * rotations)
            {
                float maxSpeed = 5;

                if (angle * rotations - rotatedAngle > 10 * angle)
                    newRot = Mathf.Clamp(newRot + acceleration * Time.deltaTime, 0, maxSpeed);
                else
                    newRot = maxSpeed * (angle * rotations - rotatedAngle) / (10 * angle) + .1f;
                
                transform.Rotate(0, newRot, 0);
                rotatedAngle += newRot;
            }
            else
            {
                rolling = false;
                rotatedAngle = 0;
                rotations = 0;
            }
        }
	}

    public void RollSlotByRotations(int rot)
    {
        if (rolling)
            return;

        rotations = rot;
        rotatedAngle = 0;
        rolling = true;

        print(Time.deltaTime + ": " + transform.name + " obróci się " + rotations + " razy.");
    }
}
