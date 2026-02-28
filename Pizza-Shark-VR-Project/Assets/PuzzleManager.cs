using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public SnapSlot[] slots;

    public void CheckPuzzle()
    {
        foreach (SnapSlot slot in slots)
        {
            if(!slot.IsCorrect())
            {
                Debug.Log("Not Correct Yet!");
                return;
            }
        }

        Debug.Log("Puzzle Solved!");
        PuzzleSolved();
    }

    void PuzzleSolved()
    {
    Debug.Log("You win!");

    foreach (SnapSlot slot in slots)
    {
        if (slot.currentCube != null)
        {
            slot.currentCube.GlowWhite();
        }
    }
}
}
