using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DefaultExecutionOrder(-100)]
public class PlayerRaycaster : MonoBehaviour
{
    const float WallRaycastDist = 0.1f;
    const float GroundRaycastDist = 0.25f;

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
    private bool onMovingPlatform;

    #region MonoBehavior
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void Start()
    {
        float bodyHeight = collider.bounds.extents.y;

        //Initialize
        float extentX = collider.bounds.extents.x - 0.01f;
        float extentY = bodyHeight - 0.01f;
        offsetBL = new Vector3(-extentX, -extentY + 0.1f);
        offsetBR = new Vector3(extentX, -extentY + 0.1f);
        offsetTL = new Vector3(-extentX, extentY);
        offsetTR = new Vector3(extentX, extentY);

        floatOnGroundOffset = bodyHeight + 0.1f;
    }
    #endregion

    public void UpdateRaycastOrigins()
    {
        BL = transform.position + offsetBL;
        BR = transform.position + offsetBR;
        TL = transform.position + offsetTL;
        TR = transform.position + offsetTR;

        Debug.DrawRay(BL, Vector3.left * WallRaycastDist, Color.yellow);
        Debug.DrawRay(BR, Vector3.right * WallRaycastDist, Color.green);
        Debug.DrawRay(TL, Vector3.left * WallRaycastDist, Color.red);
        Debug.DrawRay(TR, Vector3.right * WallRaycastDist, Color.blue);
        Debug.DrawRay(BL, Vector3.down * GroundRaycastDist, Color.cyan);
        Debug.DrawRay(BR, Vector3.down * GroundRaycastDist, Color.magenta);
    }

    public void RaycastCheckWalls ()
    {
        //Wall
        againstLeft =
            Physics.Raycast(TL, Vector3.left, WallRaycastDist, groundLayer) ||
            Physics.Raycast(BL, Vector3.left, WallRaycastDist, groundLayer);
        againstRight =
             Physics.Raycast(TR, Vector3.right, WallRaycastDist, groundLayer) ||
             Physics.Raycast(BR, Vector3.right, WallRaycastDist, groundLayer);
    }

    public void RaycastCheckGround()
    {
        if (onMovingPlatform)
            return;

        //Floor
        if (Physics.Raycast(BL, Vector3.down, out RaycastHit hitL, GroundRaycastDist, groundLayer))
        {
            if (!IsTargetMovingPlatform(hitL.collider))
                OffsetPlayerAboveGround(hitL);
            onGround = true;
        }
        else if (Physics.Raycast(BR, Vector3.down, out RaycastHit hitR, GroundRaycastDist, groundLayer))
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

    public void CachePrevValue ()
    {
        prevOnGround = onGround;
    }

    public void SetSteppedOnMovingPlatform(bool isOn)
    {
        onMovingPlatform = isOn;
        if (onMovingPlatform)
        {
            onGround = true;

            //Raycast down to stick to platform
            if (Physics.Raycast(BL, Vector3.down, out RaycastHit hitL, 1f, groundLayer))
            {
                if (IsTargetMovingPlatform(hitL.collider))
                    OffsetPlayerAboveGround(hitL);
            }
            else if (Physics.Raycast(BR, Vector3.down, out RaycastHit hitR, 1f, groundLayer))
            {
                if (IsTargetMovingPlatform(hitR.collider))
                    OffsetPlayerAboveGround(hitR);
            }
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

    private bool IsTargetMovingPlatform(Collider col) => col.GetComponent<MovingPlatform>() != null;
}