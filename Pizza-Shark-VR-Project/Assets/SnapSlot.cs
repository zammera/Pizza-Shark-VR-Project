using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapSlot : MonoBehaviour
{
    public CubeColor correctColor;
    public Cube currentCube;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private PuzzleManager puzzleManager;

    void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnCubePlaced);
        socket.selectExited.AddListener(OnCubeRemoved);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnCubePlaced);
        socket.selectExited.RemoveListener(OnCubeRemoved);
    }

    private void OnCubePlaced(SelectEnterEventArgs args)
    {
        currentCube = args.interactableObject.transform.GetComponent<Cube>();
        puzzleManager.CheckPuzzle();
    }

    private void OnCubeRemoved(SelectExitEventArgs args)
    {
        currentCube = null;
        puzzleManager.CheckPuzzle();
    }

    public bool IsCorrect()
    {
        if (currentCube == null)
            return false;

        return currentCube.color == correctColor;
    }
}