using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour {

	public float speed = 1.0f;
	public bool animate = false;
	public float zRotationDestination = 310.0f;
	public float zRotation = 340.0f;

    Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

	void Update ()
	{
		zRotation = transform.rotation.eulerAngles.z;

        //  Animacja ze stałą szybkością speed od rotacji na osi z 340 do 310 i z powrotem z szybkością -0.5
		if (animate)
		{
            //  Ruch w dół
			if(speed > 0)
			{
				transform.Rotate(0, 0, -speed);

                //  Nadaj prędkości przeciwny zwrot i mniejszą wartości jeśli dźwignia dotarła do końca
				if(zRotation <= zRotationDestination)
					speed = -0.5f;
			}
            //  Ruch powrotny w górę
			else if(speed < 0)
			{
				transform.Rotate(0, 0, -speed);

				if(zRotation >= 340.0f)
				{
					zRotation = 340.0f;
					speed = 1.0f;
					animate = false;
				}
			}
		}
	}

    //  Sprawdzenie czy animacja nie jest właśnie w trakcie
	public void ArmAnimate()
	{
		if (animate)
			return;

		animate = true;
	}
}
