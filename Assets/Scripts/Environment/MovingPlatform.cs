using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public GameObject TargetPoint;
	public Vector3 origPoint;
	float distance;
	bool reached = false;
	public float Speed = 0.01f;

	public void Start()
	{
		origPoint = transform.position;
	}

	public void FixedUpdate()
	{
		if (!reached)
		{
			distance = Vector3.Distance(transform.position, TargetPoint.transform.position);
			if (distance > .1)
			{
				move(transform.position, TargetPoint.transform.position);
			}
			else
			{
				reached = true;
			}
		}
		else
		{
			distance = Vector3.Distance(transform.position, origPoint);
			if (distance > .1)
			{
				move(transform.position, origPoint);
			}
			else
			{
				reached = false;
			}
		}
	}

	void move(Vector3 pos, Vector3 towards)
	{
		transform.position = Vector3.MoveTowards(pos, towards, Speed * Time.deltaTime * 150f);
	}
}
