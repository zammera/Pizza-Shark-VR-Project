using UnityEngine;

public enum CubeColor
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo, 
    Violet
}

public class Cube : MonoBehaviour
{
    public CubeColor color;

    private Renderer cubeRenderer;
    private Material cubeMaterial;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;
    }

    public void GlowWhite()
    {
        cubeMaterial.EnableKeyword("_EMISSION");
        cubeMaterial.SetColor("_EmissionColor", Color.white * 3f);
    }
}