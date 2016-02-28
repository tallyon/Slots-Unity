using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Arm arm = null;
    [SerializeField]
    private ScoreUpdate scoreUpdateScript = null;

    private const int numberOfSlotRollers = 3;
    // Fields on each roller are doubled
    public const int numberOfFieldsOnSlot = 10;
    // Values on each field of the roller in order
    [SerializeField]
    private int[] fieldValues = { 3, 2, 1, 5, 4, 3, 2, 1, 5, 4 };
    public int[] FieldValues
    {
        get { return fieldValues; }
        protected set { fieldValues = value; }
    }
    //  Tablica wartości wylosowanych w ostatnim losowaniu pól
    private int[] drawnFields = new int[numberOfSlotRollers];
    public int[] DrawnFields
    {
        get { return drawnFields; }
        protected set { drawnFields = value; }
    }
    // Each roller has it's own SlotManager script
    [SerializeField]
    private SlotManager[] slotManager;
    // If finishedRolling == numberOfSlotRollers, than all rollers finished their job
    private int finishedRolling;

    private bool gameOver;
    private bool autoplay;

    void Update()
    {
        if (gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            Play();

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();

        if (autoplay)
            Play();
    }

    /// <summary>
    /// Roll the slot machine for random time in milliseconds for each roller
    /// </summary>
    void Play()
    {
        // Don't roll again if there are still rollers rolling
        foreach (SlotManager slot in slotManager)
        {
            if (slot.Rolling)
                return;
        }

        // First roller will be rolling for drawn time in milliseconds between 17 and 27
        Random.seed = System.DateTime.Now.Millisecond;
        int slot1Rotations = Random.Range(17, 27);

        // For every roller increase the amount of time it will take to finish rolling by random amount
        for (int i = 0; i < slotManager.Length; i++)
        {
            // Pass time to keep rolling to the roller
            DrawnFields[i] = slotManager[i].RollSlotByRotations(slot1Rotations);
            // Add random between 3 to 7 and 1 to 4 milliseconds to the rolling time alternately
            slot1Rotations += (i % 2 == 1) ? Random.Range(3, 7) : Random.Range(1, 4);
        }

        // Fire animation on the arm
        arm.Animate();

        // Subtract points from the score to initiate rolling, if there was not enough points PayToPlay() will return false
        gameOver = !scoreUpdateScript.PayToPlay();
    }

    /// <summary>
    /// Notify GameManager that another roller finished it's job
    /// </summary>
    public void RollerFinished()
    {
        // Increment the number of rollers that finished
        finishedRolling++;

        // Check if all rollers finished their job and if so calculate score
        if (finishedRolling == numberOfSlotRollers)
        {
            scoreUpdateScript.UpdateScore();
            finishedRolling = 0;
        }
    }

    /// <summary>
    /// Toggle autplay function
    /// </summary>
    public void ToggleAutoplay()
    {
        autoplay = !autoplay;
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
