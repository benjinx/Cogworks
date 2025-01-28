using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{
    public enum HitboxMode
    {
        Manual,
        Ticking,
        Duration,
    }

    public bool debugGizmos = false;

    public LayerMask hitLayers;

    public HitboxMode mode;

    public float tickInterval;

    public float duration;

    public UnityEvent onHit;

    private BoxCollider boxCollider;

    private List<Collider> hitObjects = new List<Collider>();

    private float moveDistance;

    private Vector3 previousPostion;

    private Timer durationTimer;

    private string tickIdenfier;

    // Do an example of a spline hitbox

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        EnableHitbox();

        previousPostion = transform.position;

        tickIdenfier = gameObject.name + " tick";
    }

    private void Update()
    {
        // Check if the collider is active
        if (boxCollider.enabled)
        {
            // This will give us a more accurate distance to check,
            // which should prevent skipping at higher speeds
            moveDistance = Vector3.Distance(previousPostion, transform.position);

            float checkDistance = 0.5f;

            // if we have a move distance, let's use that, otherwise we'll just default to the checkDistance
            if (moveDistance > 0)
            {
                checkDistance = moveDistance;
            }

            List<Collider> hitObjectsThisFrame = new List<Collider>();

            RaycastHit[] raycastHits = Physics.BoxCastAll(transform.position,
                                                        boxCollider.bounds.extents * 0.5f,
                                                        Vector3.forward,
                                                        transform.rotation,
                                                        checkDistance,
                                                        hitLayers);

            // Add new
            foreach (RaycastHit hit in raycastHits)
            {
                // Skip ourselves
                if (hit.collider == boxCollider)
                {
                    continue;
                }

                if (hit.collider.TryGetComponent(out Hitbox hitbox))
                {
                    if (!hitObjects.Contains(hit.collider))
                    {
                        hitObjects.Add(hit.collider);

                        onHit?.Invoke();

                        hitbox.onHit?.Invoke();
                    }

                    hitObjectsThisFrame.Add(hit.collider);
                }
            }

            // Verify our list is up to date by removing old colliders
            for (int i = hitObjects.Count - 1; i >= 0; --i)
            {
                Collider collider = hitObjects[i];

                if (!hitObjectsThisFrame.Contains(collider))
                {
                    if (mode == HitboxMode.Ticking)
                    {
                        Timer tickingTimer = GetTickingTimer(collider.gameObject);

                        tickingTimer.StopTimer();
                        Destroy(tickingTimer);
                    }

                    hitObjects.RemoveAt(i);
                }
            }


            // This is considered stay? if it's in the list and nothing
            // is happening to it? while it's in list?
            foreach (Collider collider in hitObjects)
            {
                if (mode == HitboxMode.Ticking)
                {
                    // Check if we already have the ticking timer, if not add and start
                    if (GetTickingTimer(collider.gameObject) == null)
                    {
                        Timer tickTimer = collider.gameObject.AddComponent<Timer>();
                        tickTimer.identifer = tickIdenfier;
                        tickTimer.duration = tickInterval;
                        tickTimer.repeating = true;
                        tickTimer.onComplete_Internal += InvokeHit;
                        tickTimer.StartTimer();
                    }
                }
            }

            previousPostion = transform.position;
        }
    }

    private Timer GetTickingTimer(GameObject gobj)
    {
        // Find all timers
        List<Timer> timers = gobj.GetComponents<Timer>().ToList();

        foreach (Timer timer in timers)
        {
            // Find the ticking one
            if (timer.identifer == tickIdenfier)
            {
                return timer;
            }
        }

        return null;
    }

    private void InvokeHit()
    {
        onHit?.Invoke();
    }

    public void EnableHitbox()
    {
        hitObjects.Clear();
        boxCollider.enabled = true;

        if (mode == HitboxMode.Duration)
        {
            durationTimer = gameObject.AddComponent<Timer>();
            durationTimer.endAction = Timer.EndAction.DestroyTimer;
            durationTimer.duration = duration;
            durationTimer.onComplete_Internal += DisableHitbox;
            durationTimer.StartTimer();
        }
    }

    public void DisableHitbox()
    {
        boxCollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (!debugGizmos)
        {
            return;
        }

        BoxCollider debugBoxCollider = GetComponent<BoxCollider>();

        if (debugBoxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(debugBoxCollider.center, debugBoxCollider.size);
            //Gizmos.DrawCube(debugBoxCollider.center, debugBoxCollider.size);
        }
    }
}
