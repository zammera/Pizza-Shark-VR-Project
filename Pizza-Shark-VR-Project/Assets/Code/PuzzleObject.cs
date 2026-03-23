using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Data;

public class PuzzleObject : MonoBehaviour {
    // Define the 4 objects that can be turned for the puzzle.
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;
    public GameObject Fire1;
    public GameObject Fire2;
    public GameObject Fire3;
    public GameObject Fire4;
    public GameObject DoorObject;

    public Material newMaterialRef;

    // Each object has 8 positions, rounded from in game angle (Range from 0 - 360 -> 45 deg per position):
        // 1 -> | ^ (337.5 - 22.49)
        // 2 -> / ^ (22.5  - 67.49)
        // 3 -> - > (67.5  - 112.49)
        // 4 -> \ v (112.5 - 157.49)
        // 5 -> | v (157.5 - 202.49)
        // 6 -> / v (202.5 - 247.49)
        // 7 -> - < (247.5 - 292.49)
        // 8 -> \ ^ (292.5 - 337.49)

    // This should be its own function
    private int GetPuzzlePosition(GameObject obj)
    {
        float angle = obj.transform.localRotation.eulerAngles.z;
        if(angle < 22.5){
            return 1;
        }
        if(angle < 67.5){
            return 2;
        }
        if(angle < 112.5){
            return 3;
        }
        if(angle < 157.5)
        {
            return 4;
        }
        if(angle < 202.5)
        {
            return 5;
        }
        if(angle < 247.5)
        {
            return 6;
        }
        if(angle < 292.5)
        {
            return 7;
        }
        if(angle < 337.5)
        {
            return 8;
        }
        return 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        int angle1 = GetPuzzlePosition(Object1);
        int angle2 = GetPuzzlePosition(Object2);
        int angle3 = GetPuzzlePosition(Object3);
        int angle4 = GetPuzzlePosition(Object4);
        Debug.Log("Positions: {" + angle1 + ", " + angle2 + ", " + angle3 + ", " + angle4 + "}");
        
        if(angle1 == 6 && angle2 == 7 && angle3 == 8 && angle4 == 2)
        // if(angle1 == 5 && angle2 == 5 && angle3 == 5 && angle4 == 5)
        {
            // Change Material to indicate completion
            Object1.GetComponent<Renderer>().material = newMaterialRef;
            Object2.GetComponent<Renderer>().material = newMaterialRef;
            Object3.GetComponent<Renderer>().material = newMaterialRef;
            Object4.GetComponent<Renderer>().material = newMaterialRef;

            // Then make it so they arent interactable
            Object1.GetComponent<XRGrabInteractable>().enabled = false;
            Object2.GetComponent<XRGrabInteractable>().enabled = false;
            Object3.GetComponent<XRGrabInteractable>().enabled = false;
            Object4.GetComponent<XRGrabInteractable>().enabled = false;

            DoorObject.GetComponent<HingeJoint>().useSpring = true;
            HingeJoint hinge = DoorObject.GetComponent<HingeJoint>();
            JointLimits limits = hinge.limits;
            limits.min = -90f;
            limits.max = 0f;
            hinge.limits = limits;

            // //Turn Fire on
            // Fire1.GetComponent<TurnFireOn>().TurnOn();
            // Fire2.GetComponent<TurnFireOn>().TurnOn();
            // Fire3.GetComponent<TurnFireOn>().TurnOn();
            // Fire4.GetComponent<TurnFireOn>().TurnOn();

        }
    }
}
