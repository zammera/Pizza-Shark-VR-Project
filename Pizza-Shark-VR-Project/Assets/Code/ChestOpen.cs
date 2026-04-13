using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChestOpen : MonoBehaviour
{
    public GameObject key;
    public GameObject keySlot;
    public GameObject chestTop;

    private bool opened = false;

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

    private bool KeyInHole()
    {
        XRSocketInteractor socket = keySlot.GetComponent<XRSocketInteractor>();

        if (socket == null || !socket.hasSelection)
            return false;

        GameObject insertedObject = socket.firstInteractableSelected.transform.gameObject;
        
        return insertedObject == key;
    }

    void Update()
    {
        if (!opened && KeyInHole())
        {
            opened = true;
            PlaySound();
            // Example: rotate the lid upward instead of teleporting it
            chestTop.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);

            chestTop.transform.localPosition = new Vector3(-0.85f, 0.38f, 0f);
        }
    }
}