using System.Collections;
using UnityEngine;

public class CameraController3D : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    [SerializeField] Transform player;
    [SerializeField] Vector2 offsetAmount;
    [SerializeField] Vector2 offsetSpeed;


    //Status
    Vector3 aimingPosition;
    float currentOffsetX;
    bool isCameraOffsettingRight;

    //Cache
    float startingZ;

    void Start()
    {
        startingZ = transform.position.z;


        OffsetToRight(true);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            UpdateOffsetAmount();
            aimingPosition = new Vector3(player.position.x + currentOffsetX, player.position.y + offsetAmount.y, startingZ);

            transform.localPosition = new Vector3(
                Mathf.Lerp(transform.localPosition.x, aimingPosition.x, offsetSpeed.x * Time.deltaTime),
                Mathf.Lerp(transform.localPosition.y, aimingPosition.y, offsetSpeed.y * Time.deltaTime),
                startingZ);
        }
    }


    #region Camera offset
    void UpdateOffsetAmount()
    {
        if (PlayerFeedback.FacingRight && !isCameraOffsettingRight)
            OffsetToRight(true);
        else if (!PlayerFeedback.FacingRight && isCameraOffsettingRight)
            OffsetToRight(false);
    }

    void OffsetToRight(bool r)
    {
        isCameraOffsettingRight = r;
        currentOffsetX = r ? offsetAmount.x : -offsetAmount.x;
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
