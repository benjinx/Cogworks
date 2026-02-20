using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UVScroller : MonoBehaviour
{
    // Speed of scroll in UV Space
    public Vector2 scrollRate = Vector2.up;
    
    // Which material on the renderer
    public int materialIndex = 1;
    
    static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private Renderer renderer;
    private Material material;
    
    private Vector2 uvFactor;
    private Vector2 offset;
    
    void Awake()
    {
        renderer = GetComponent<Renderer>();

        if (renderer.materials.Length <= materialIndex)
        {
            Debug.LogWarning("UVScroller: materialIndex " + materialIndex + " out of range!");
            enabled = false;
        }
        
        material = renderer.materials[materialIndex];
        
        Vector2 tiling = material.GetTextureScale(MainTex);
        Vector3 scale = transform.localScale;
        
        // Assuming:
        // UV X -> World X
        // UV Y -> World Z
        uvFactor = new Vector2(tiling.x / scale.x, tiling.y / scale.z);
    }

    void Update()
    {
        offset += Vector2.Scale(scrollRate * Time.deltaTime, uvFactor);
        
        material.mainTextureOffset = offset;
    }
}
