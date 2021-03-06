using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DefaultExecutionOrder(-100)]
public class PlayerRaycaster : MonoBehaviour
{
    const float RaycastDist = 0.15f;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    //Reference
    private Collider collider;

    //Status
    private bool againstLeft;
    private bool againstRight;
    private bool onGround;
    private bool prevOnGround;

    //Cache
    private Vector3 offsetTL;
    private Vector3 offsetTR;
    private Vector3 offsetBL;
    private Vector3 offsetBR;

    private float floatOnGroundOffset;

    public Vector3 BL { get; private set; }
    public Vector3 BR { get; private set; }
    public Vector3 TL { get; private set; }
    public Vector3 TR { get; private set; }

    public bool AgainstLeft => againstLeft;
    public bool AgainstRight => againstRight;
    public bool OnGround => onGround;

    #region MonoBehavior
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void Start()
    {
        //Initialize
        float extentX = collider.bounds.extents.x - 0.01f;
        float extentY = collider.bounds.extents.y - 0.01f;
        offsetBL = new Vector3(-extentX, -extentY);
        offsetBR = new Vector3(extentX, -extentY);
        offsetTL = new Vector3(-extentX, extentY);
        offsetTR = new Vector3(extentX, extentY);

        floatOnGroundOffset = collider.bounds.extents.y + 0.1f;
    }

    private void Update()
    {
        UpdateRaycastValues();
        UpdateOffsets();
        prevOnGround = onGround;
    }
    #endregion

    private void UpdateRaycastValues()
    {
        againstLeft =
            Physics.Raycast(TL, Vector3.left, RaycastDist, groundLayer) ||
            Physics.Raycast(BL, Vector3.left, RaycastDist, groundLayer);
        againstRight =
             Physics.Raycast(TR, Vector3.right, RaycastDist, groundLayer) ||
             Physics.Raycast(BR, Vector3.right, RaycastDist, groundLayer);

        if (Physics.Raycast(BL, Vector3.down, out RaycastHit hitL, RaycastDist, groundLayer))
        {
            if (!IsTargetMovingPlatform(hitL.collider))
                OffsetPlayerAboveGround(hitL);
            onGround = true;
        }
        else if (Physics.Raycast(BR, Vector3.down, out RaycastHit hitR, RaycastDist, groundLayer))
        {
            if (!IsTargetMovingPlatform(hitR.collider))
                OffsetPlayerAboveGround(hitR);
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void OffsetPlayerAboveGround(RaycastHit hit)
    {
        //If the player has just landed, then offset the player a distance from the ground,
        //because we dont want the player stand flush against the ground as he could be obstructed by the 
        //discontinuous box colliders of the ice blocks.
        if (!prevOnGround)
        {
            Vector3 p = transform.position;
            p.y = hit.point.y + floatOnGroundOffset;
            transform.position = p;
        }
    }

    private void UpdateOffsets()
    {
        BL = transform.position + offsetBL;
        BR = transform.position + offsetBR;
        TL = transform.position + offsetTL;
        TR = transform.position + offsetTR;

        Debug.DrawRay(BL, Vector3.left * 0.1f, Color.yellow);
        Debug.DrawRay(BR, Vector3.right * 0.1f, Color.green);
        Debug.DrawRay(TL, Vector3.left * 0.1f, Color.red);
        Debug.DrawRay(TR, Vector3.right * 0.1f, Color.blue);
    }

    private bool IsTargetMovingPlatform(Collider col) => col.GetComponent<MovingPlatform>() != null;
}