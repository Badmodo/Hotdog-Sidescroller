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

    //Cache
    private Vector3 offsetTL;
    private Vector3 offsetTR;
    private Vector3 offsetBL;
    private Vector3 offsetBR;

    private bool againstLeft;
    private bool againstRight;
    private bool onGround;

    public Vector3 BL { get; private set; }
    public Vector3 BR { get; private set; }
    public Vector3 TL { get; private set; }
    public Vector3 TR { get; private set; }

    public bool AgainstLeft => againstLeft;
    public bool AgainstRight => againstRight;
    public bool OnGround => onGround;

    #region MonoBehavior
    void Awake()
    {
        collider = GetComponent<Collider>();
    }
    void Start()
    {
        //Initialize
        float extentX = collider.bounds.extents.x - 0.01f;
        float extentY = collider.bounds.extents.y - 0.01f;
        offsetBL = new Vector3(-extentX, -extentY);
        offsetBR = new Vector3(extentX, -extentY);
        offsetTL = new Vector3(-extentX, extentY);
        offsetTR = new Vector3(extentX, extentY);
    }

    void Update()
    {
        UpdateRaycastValues();
        UpdateOffsets();
    }
    #endregion

    void UpdateRaycastValues()
    {
        againstLeft = 
            Physics.Raycast(TL, Vector3.left, RaycastDist, groundLayer) ||
            Physics.Raycast(BL, Vector3.left, RaycastDist, groundLayer);
        againstRight =
             Physics.Raycast(TR, Vector3.right, RaycastDist, groundLayer) ||
             Physics.Raycast(BR, Vector3.right, RaycastDist, groundLayer);
        onGround =
             Physics.Raycast(BL, Vector3.down, RaycastDist, groundLayer) ||
             Physics.Raycast(BR, Vector3.down, RaycastDist, groundLayer);

        Debug.DrawRay(BL, Vector3.down * RaycastDist, Color.white);
        Debug.DrawRay(BR, Vector3.down * RaycastDist, Color.white);

        //if (!onGround)
        //{
        //    Debug.DrawRay(BL, Vector3.down * RaycastDist, Color.red, 10f);
        //    Debug.DrawRay(BR, Vector3.down * RaycastDist, Color.green, 10f);
        //}
    }

    void UpdateOffsets()
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
}