// using System.Collections;
// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.Interactors;
// using UnityEngine.SceneManagement;

// public class GoopSocketHandler : MonoBehaviour
// {
//     public XRSocketInteractor socket;
//     public Renderer goopRenderer;

//     [Header("Valid Ingredients")]
//     public GameObject pillow1;
//     public GameObject pillow2;
//     public GameObject strawberry;
//     public GameObject bun;
//     public GameObject tnt;
//     public GameObject carrot;
//     public GameObject mushroom;

//     [Header("Sink Settings")]
//     public float lowerDistance = 0.2f;
//     public float lowerTime = 0.5f;
//     public float destroyDelay = 0.1f;

//     [Header("Material Settings")]
//     public int goopMaterialIndex = 1;

//     private bool isProcessing = false;
//     private bool recipeCompleted = false;

//     // Track which ingredients have been added
//     private bool hasPillow = false;
//     private bool hasStrawberry = false;
//     private bool hasBun = false;
//     private bool hasTnt = false;
//     private bool hasCarrot = false;
//     private bool hasMushroom = false;

//     private void Reset()
//     {
//         socket = GetComponent<XRSocketInteractor>();
//     }

//     private void OnEnable()
//     {
//         if (socket != null)
//             socket.selectEntered.AddListener(OnItemInserted);
//     }

//     private void OnDisable()
//     {
//         if (socket != null)
//             socket.selectEntered.RemoveListener(OnItemInserted);
//     }

//     private void OnItemInserted(SelectEnterEventArgs args)
//     {
//         if (isProcessing) return;

//         var interactable =
//             args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable;

//         if (interactable != null)
//         {
//             StartCoroutine(ProcessInsertedItem(interactable));
//         }
//     }

//     private IEnumerator ProcessInsertedItem(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable)
//     {
//         isProcessing = true;

//         GameObject insertedObject = interactable.gameObject;
//         Debug.Log("Inserted item: " + insertedObject.name);

//         // Only process valid ingredients
//         bool isValidIngredient = TryApplyIngredientEffect(insertedObject);

//         if (!isValidIngredient)
//         {
//             Debug.Log("Object is not a valid cauldron ingredient. Leaving it in the socket.");
//             isProcessing = false;
//             yield break;
//         }

//         yield return null;

//         if (socket != null && socket.interactionManager != null)
//         {
//             socket.interactionManager.SelectExit(
//                 (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor)socket,
//                 (UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)interactable
//             );
//         }

//         var grab =
//             insertedObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

//         if (grab != null)
//         {
//             grab.enabled = false;
//         }

//         Vector3 startPos = insertedObject.transform.position;
//         Vector3 endPos = startPos + Vector3.down * lowerDistance;

//         float elapsed = 0f;
//         while (elapsed < lowerTime)
//         {
//             insertedObject.transform.position = Vector3.Lerp(startPos, endPos, elapsed / lowerTime);
//             elapsed += Time.deltaTime;
//             yield return null;
//         }

//         insertedObject.transform.position = endPos;

//         yield return new WaitForSeconds(destroyDelay);

//         Destroy(insertedObject);

//         CheckRecipeComplete();

//         isProcessing = false;
//     }

//     private bool TryApplyIngredientEffect(GameObject insertedObject)
//     {
//         if (goopRenderer == null)
//         {
//             Debug.LogWarning("Goop Renderer is not assigned.");
//             return false;
//         }

//         Material[] mats = goopRenderer.materials;

//         if (goopMaterialIndex < 0 || goopMaterialIndex >= mats.Length)
//         {
//             Debug.LogWarning("goopMaterialIndex is out of range on " + goopRenderer.gameObject.name);
//             return false;
//         }

//         Color current = mats[goopMaterialIndex].color;
//         Color updated = current;

//         if (insertedObject == mushroom)
//         {
//             updated.b = Mathf.Clamp01(updated.b + 0.15f);
//             hasMushroom = true;
//         }
//         else if (insertedObject == strawberry)
//         {
//             updated.r = Mathf.Clamp01(updated.r + 0.15f);
//             hasStrawberry = true;
//         }
//         else if (insertedObject == carrot)
//         {
//             Color orangeTarget = new Color(1f, 0.5f, 0f);
//             updated = Color.Lerp(updated, orangeTarget, 0.35f);
//             hasCarrot = true;
//         }
//         else if (insertedObject == bun)
//         {
//             updated.g = Mathf.Clamp01(updated.g - 0.15f);
//             hasBun = true;
//         }
//         else if (insertedObject == pillow1 || insertedObject == pillow2)
//         {
//             updated = Color.Lerp(updated, Color.white, 0.25f);
//             hasPillow = true;
//         }
//         else if (insertedObject == tnt)
//         {
//             updated *= 0.8f;
//             updated.a = current.a;
//             hasTnt = true;
//         }
//         else
//         {
//             return false;
//         }

//         mats[goopMaterialIndex].color = updated;
//         goopRenderer.materials = mats;

//         Debug.Log("Goop color updated to: " + updated);
//         return true;
//     }

//     private void CheckRecipeComplete()
//     {
//         if (recipeCompleted) return;

//         if (hasPillow &&
//             hasStrawberry &&
//             hasBun &&
//             hasTnt &&
//             hasCarrot &&
//             hasMushroom)
//         {
//             recipeCompleted = true;
//             Done();
//         }
//     }

//     private void Done()
//     {
//         Debug.Log("Recipe complete!");
//         SceneManager.LoadScene("END");
//     }
// }

using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.SceneManagement;

public class GoopSocketHandler : MonoBehaviour
{
    public XRSocketInteractor socket;
    public Renderer goopRenderer;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip ingredientDropSound;

    [Header("Valid Ingredients")]
    public GameObject pillow1;
    public GameObject pillow2;
    public GameObject strawberry;
    public GameObject bun;
    public GameObject tnt;
    public GameObject carrot;
    public GameObject mushroom;

    [Header("Sink Settings")]
    public float lowerDistance = 0.2f;
    public float lowerTime = 0.5f;
    public float destroyDelay = 0.1f;

    [Header("Material Settings")]
    public int goopMaterialIndex = 1;

    private bool isProcessing = false;
    private bool recipeCompleted = false;

    private bool hasPillow = false;
    private bool hasStrawberry = false;
    private bool hasBun = false;
    private bool hasTnt = false;
    private bool hasCarrot = false;
    private bool hasMushroom = false;

    private void Reset()
    {
        socket = GetComponent<XRSocketInteractor>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (socket != null)
            socket.selectEntered.AddListener(OnItemInserted);
    }

    private void OnDisable()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnItemInserted);
    }

    private void OnItemInserted(SelectEnterEventArgs args)
    {
        if (isProcessing) return;

        var interactable =
            args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable;

        if (interactable != null)
        {
            StartCoroutine(ProcessInsertedItem(interactable));
        }
    }

    private IEnumerator ProcessInsertedItem(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable)
    {
        isProcessing = true;

        GameObject insertedObject = interactable.gameObject;
        Debug.Log("Inserted item: " + insertedObject.name);

        bool isValidIngredient = TryApplyIngredientEffect(insertedObject);

        if (!isValidIngredient)
        {
            Debug.Log("Object is not a valid cauldron ingredient. Leaving it in the socket.");
            isProcessing = false;
            yield break;
        }

        PlaySound();

        yield return null;

        if (socket != null && socket.interactionManager != null)
        {
            socket.interactionManager.SelectExit(
                (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor)socket,
                (UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)interactable
            );
        }

        var grab =
            insertedObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grab != null)
        {
            grab.enabled = false;
        }

        Vector3 startPos = insertedObject.transform.position;
        Vector3 endPos = startPos + Vector3.down * lowerDistance;

        float elapsed = 0f;
        while (elapsed < lowerTime)
        {
            insertedObject.transform.position = Vector3.Lerp(startPos, endPos, elapsed / lowerTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        insertedObject.transform.position = endPos;

        yield return new WaitForSeconds(destroyDelay);

        Destroy(insertedObject);

        CheckRecipeComplete();

        isProcessing = false;
    }

    private void PlaySound()
    {
        if (audioSource != null && ingredientDropSound != null)
        {
            audioSource.PlayOneShot(ingredientDropSound);
        }
    }

    private bool TryApplyIngredientEffect(GameObject insertedObject)
    {
        if (goopRenderer == null)
        {
            Debug.LogWarning("Goop Renderer is not assigned.");
            return false;
        }

        Material[] mats = goopRenderer.materials;

        if (goopMaterialIndex < 0 || goopMaterialIndex >= mats.Length)
        {
            Debug.LogWarning("goopMaterialIndex is out of range on " + goopRenderer.gameObject.name);
            return false;
        }

        Color current = mats[goopMaterialIndex].color;
        Color updated = current;

        if (insertedObject == mushroom)
        {
            updated.b = Mathf.Clamp01(updated.b + 0.15f);
            hasMushroom = true;
        }
        else if (insertedObject == strawberry)
        {
            updated.r = Mathf.Clamp01(updated.r + 0.15f);
            hasStrawberry = true;
        }
        else if (insertedObject == carrot)
        {
            Color orangeTarget = new Color(1f, 0.5f, 0f);
            updated = Color.Lerp(updated, orangeTarget, 0.35f);
            hasCarrot = true;
        }
        else if (insertedObject == bun)
        {
            updated.g = Mathf.Clamp01(updated.g - 0.15f);
            hasBun = true;
        }
        else if (insertedObject == pillow1 || insertedObject == pillow2)
        {
            updated = Color.Lerp(updated, Color.white, 0.25f);
            hasPillow = true;
        }
        else if (insertedObject == tnt)
        {
            updated *= 0.8f;
            updated.a = current.a;
            hasTnt = true;
        }
        else
        {
            return false;
        }

        mats[goopMaterialIndex].color = updated;
        goopRenderer.materials = mats;

        Debug.Log("Goop color updated to: " + updated);
        return true;
    }

    private void CheckRecipeComplete()
    {
        if (recipeCompleted) return;

        if (hasPillow &&
            hasStrawberry &&
            hasBun &&
            hasTnt &&
            hasCarrot &&
            hasMushroom)
        {
            recipeCompleted = true;
            Done();
        }
    }

    private void Done()
    {
        Debug.Log("Recipe complete!");
        SceneManager.LoadScene("END");
    }
}