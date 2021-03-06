using System.Collections;
using UnityEngine;

public class CameraController3D : MonoBehaviour
{
    public float xMin = -500f;
    public float xMax = 2000f;
    public float yMin = 0f;
    public float yMax = 9.9f;

    [SerializeField] Transform player;
    [SerializeField] Vector2 maxOffset = new Vector2(3f, 2f);
    [SerializeField] Vector2 offsetSpeed = new Vector2(2f, 4f);

    //Status
    Vector3 targetPosition;
    float currentOffsetX;
    float currentOffsetY;

    //Cache
    float startingZ;

    void Start()
    {
        startingZ = transform.position.z;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            UpdateOffsetAmount();
            targetPosition = new Vector3(player.position.x + currentOffsetX, player.position.y + currentOffsetY, startingZ);

            transform.localPosition = new Vector3(
                Mathf.Lerp(transform.localPosition.x, targetPosition.x, offsetSpeed.x * Time.deltaTime),
                Mathf.Lerp(transform.localPosition.y, targetPosition.y, offsetSpeed.y * Time.deltaTime),
                startingZ);
        }
    }


    #region Camera offset
    void UpdateOffsetAmount()
    {
        //Horizontal offset based on player facing
        currentOffsetX = PlayerFeedback.FacingRight ? maxOffset.x : -maxOffset.x;

        //Vertical offset based on pressing down
        currentOffsetY = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ) ? -maxOffset.y : 1f;
    }
    #endregion

    #region Helper
    Vector3 ConfinePositionWithinSceneBounds(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);
        return position;
    }
    #endregion

}
