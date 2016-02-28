using UnityEngine;

public class Arm : MonoBehaviour
{
    private float speed = 1.0f;
    private float zRotationDestination = 310.0f;
    private float zRotation = 340.0f;
    private bool animate;

    private Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        zRotation = trans.rotation.eulerAngles.z;
        
        // Animates with constant speed on z axis from 340 to 310 and backwards with slower speed (speed mod = -.5f)
        if (animate)
        {
            // Animate pulling down the arm
            if (speed > 0)
            {
                trans.Rotate(0, 0, -speed);

                // Set speed modifier to half the speed and backwards
                if (zRotation <= zRotationDestination)
                    speed *= -.5f;
            }
            // Animate slower return of the arm to it's former position
            else if (speed < 0)
            {
                trans.Rotate(0, 0, -speed);

                if (zRotation >= 340.0f)
                {
                    zRotation = 340.0f;
                    speed = 1.0f;
                    animate = false;
                }
            }
        }
    }

    // Set animate flag
    public void Animate()
    {
        animate = true;
    }
}
