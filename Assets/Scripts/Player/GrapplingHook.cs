using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrapplingHook : MonoBehaviour
{
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerController playerMovement;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();

    private bool distanceSet;

    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

    public float climbSpeed = 3f;
    private bool isColliding;

    float cameraToMouseDistance;

    void Awake()
    {
        // Disables the ropeJoint and sets the player position
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();

        cameraToMouseDistance = transform.position.z - Camera.main.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Captures the world position of the mouse using screentoworldpoint, and use this to create the aim angle 
        //var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraToMouseDistance));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // This is for rotation and for calculating the angles in degree.
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        
        // Variable to track player position
        playerPosition = transform.position;

        // used to ditermin if the rope is attached to an anchor point
        if (!ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
        }
        else
        {
            crosshairSprite.enabled = false;
            // 1
            if (ropePositions.Count > 0)
            {
                // 2
                var lastRopePoint = ropePositions.Last();
                var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);

                // 3
                if (playerToCurrentNextHit)
                {
                    var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);

                        // 4
                        if (wrapPointsLookup.ContainsKey(closestPointToHit))
                        {
                            ResetRope();
                            return;
                        }

                        // 5
                        ropePositions.Add(closestPointToHit);
                        wrapPointsLookup.Add(closestPointToHit, 0);
                        distanceSet = false;
                    }
                }
            }
        }

        HandleInput(aimDirection);

        UpdateRopePositions();

        HandleRopeLength();

    }

    // Position the crosshair in relation to the mouse and angle generated in Update
    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

    // Aim direction pulled from Update, and on press fire raycast
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            // When the left mouse button is pressed line renderer is enabled
            if (ropeAttached) return;
            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            // If a valid raycast is captured Bool is set to true
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // Little force is applied
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }
            }
            // If raycast dosnt touch anything everything is set to false
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        //resets the rope(recalls it)
        if (Input.GetMouseButton(1))
        {
            ResetRope();
        }
    }

    // The racall
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
       // playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;

        wrapPointsLookup.Clear();
    }

    private void UpdateRopePositions()
    {
        // checking if the rope isnt attached
        if (!ropeAttached)
        {
            return;
        }

        // setting the rope renderer position, an some other positions
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // This section loops back through rope position list
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);

                // set the rope anchor to the second to last position
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // This handles the situation where the second to last position is being used
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // This else handles the last postion pre the players
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }


    // This method takes in two parameters, a RaycastHit2D object, and a PolygonCollider2D
    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        /// <summary>
        /// This converts the polygon collider's collection of points, 
        /// into a dictionary of Vector2 positions (the value of each dictionary entry is the position itself), 
        /// and the key of each entry, is set to the distance that this point is to the player's position (float value). 
        /// Something else happens here: the resulting position is transformed into world space (by default a collider's 
        /// vertex positions are stored in local space - i.e. local to the object the collider sits on, and we want the world space positions)
        /// </summary>

        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        // The dictionary is ordered by key. The distance closest to the player's current position, and the closest one is 
        // returned, meaning that whichever point is returned from this method, is the point on the collider between the player 
        // and the current hinge point on the rope
        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

    private void HandleRopeLength()
    {
        // This looks for vertical axis input and increases or decreases the rope length
        if (Input.GetAxis("Vertical") >= 1f && ropeAttached && !isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && ropeAttached)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }

    void OnTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }

}
