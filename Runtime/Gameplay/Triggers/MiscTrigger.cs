using UnityEngine;
using UnityEngine.Events;

public class MiscTrigger : MonoBehaviour
{
    public bool isOneShot = false;
    
    public UnityEvent onTriggerEnter;
    
    public UnityEvent onTriggerExit;

    public UnityEvent onTriggerStay;
    
    private bool isCurrentlyBeingTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCurrentlyBeingTriggered)
        {
            return;
        }

        if (other.tag == "Player")
        {
            onTriggerEnter?.Invoke();

            isCurrentlyBeingTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCurrentlyBeingTriggered)
        {
            if (other.tag == "Player")
            {
                onTriggerStay?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCurrentlyBeingTriggered)
        {
            if (other.tag == "Player")
            {
                onTriggerExit?.Invoke();

                if (!isOneShot)
                {
                    isCurrentlyBeingTriggered = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
            return;

        Color oldColor = Gizmos.color;

        // Fill
        Gizmos.color = new Color(0.6f, 0f, 0.8f, 0.25f);

        Matrix4x4 matrix = Matrix4x4.TRS(
            col.transform.position,
            col.transform.rotation,
            col.transform.lossyScale
        );

        Gizmos.matrix = matrix;

        if (col is BoxCollider box)
        {
            Gizmos.DrawCube(box.center, box.size);

            Gizmos.color = new Color(0.6f, 0f, 0.8f, 0.6f);
            Gizmos.DrawWireCube(box.center, box.size);
        }
        else if (col is SphereCollider sphere)
        {
            Gizmos.DrawSphere(sphere.center, sphere.radius);

            Gizmos.color = new Color(0.6f, 0f, 0.8f, 0.6f);
            Gizmos.DrawWireSphere(sphere.center, sphere.radius);
        }
        else if (col is CapsuleCollider capsule)
        {
            // Capsule is annoying — approximate with two spheres + a box
            DrawCapsule(capsule);
        }
        else if (col is MeshCollider mesh)
        {
            if (mesh.sharedMesh != null)
            {
                Gizmos.DrawMesh(mesh.sharedMesh);

                Gizmos.color = new Color(0.6f, 0f, 0.8f, 0.6f);
                Gizmos.DrawWireMesh(mesh.sharedMesh);
            }
        }

        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = oldColor;
    }
    
    void DrawCapsule(CapsuleCollider capsule)
    {
        Vector3 center = capsule.center;
        float radius = capsule.radius;
        float height = Mathf.Max(capsule.height, radius * 2);

        float cylinderHeight = height - (radius * 2);

        // Draw center box (cylinder-ish)
        Gizmos.DrawCube(center, new Vector3(radius * 2, cylinderHeight, radius * 2));

        // Draw spheres on ends
        Vector3 up = Vector3.up * (cylinderHeight / 2);

        Gizmos.DrawSphere(center + up, radius);
        Gizmos.DrawSphere(center - up, radius);

        // Wire
        Gizmos.color = new Color(0.6f, 0f, 0.8f, 0.6f);

        Gizmos.DrawWireCube(center, new Vector3(radius * 2, cylinderHeight, radius * 2));
        Gizmos.DrawWireSphere(center + up, radius);
        Gizmos.DrawWireSphere(center - up, radius);
    }
}
