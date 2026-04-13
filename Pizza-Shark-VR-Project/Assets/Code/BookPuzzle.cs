using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BookPuzzle : MonoBehaviour
{
    // We need a lot of objects, 30 to be exact, 15 for the pictures and 15 for the slots
    public GameObject[] gameArray;
    public GameObject[] slotArray;
    public Material newMaterialRef;
    public GameObject MISHA;

    public AudioSource audioSource;
    public AudioClip soundEffect;
    bool sound_played = false;
    public void PlaySound()
        {
            if (audioSource != null && soundEffect != null && sound_played == false)
            {
                audioSource.PlayOneShot(soundEffect);
                sound_played = true;
            }
        }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private int isSolved()
    //Returns 1 if solved, 0 if not
    {
        for(int i = 0; i < gameArray.Length; i++)
        {
            XRSocketInteractor temp = slotArray[i].GetComponent<XRSocketInteractor>();
            if(temp != null && temp.hasSelection){
                GameObject wantedObject = temp.firstInteractableSelected.transform.gameObject;
                if(wantedObject != gameArray[i])
                {
                    return 0;
                }
            }
            else {
                return 0;
            }
        }
        return 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isSolved());
        if (isSolved() == 1)
        {
            MISHA.GetComponent<Renderer>().material = newMaterialRef;

            PlaySound();
        }
    }
}
