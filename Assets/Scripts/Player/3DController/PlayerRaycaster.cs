using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DefaultExecutionOrder(-100)]
public class PlayerRaycaster : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    Collider collider;

    //Cache
    Vector3 offsetTL;
    Vector3 offsetTR;
    Vector3 offsetBL;
    Vector3 offsetBR;

    public Vector3 BL { get; private set; }
    public Vector3 BR { get; private set; }
    public Vector3 TL { get; private set; }
    public Vector3 TR { get; private set; }

    public bool AgainstLeft =>
        Physics.Raycast(TL, Vector3.left, 0.1f, groundLayer) ||
        Physics.Raycast(BL, Vector3.left, 0.1f, groundLayer);
    public bool AgainstRight =>
        Physics.Raycast(TR, Vector3.right, 0.1f, groundLayer) ||
        Physics.Raycast(BR, Vector3.right, 0.1f, groundLayer);
    public bool OnGround =>
        Physics.Raycast(BL, Vector3.down, 0.1f, groundLayer) ||
        Physics.Raycast(BR, Vector3.down, 0.1f, groundLayer);

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
        UpdateOffsets();
    }
    #endregion

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