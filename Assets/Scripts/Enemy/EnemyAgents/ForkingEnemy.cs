using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkingEnemy : EnemyBodyBase
{
	[SerializeField] private float speed;
	[SerializeField] private float distance;
	private Transform lowPoint, topPoint;
	private bool isRising;

    protected override void Start()
    {
        base.Start();
		isStompable = true;
	}

	private void Update()
	{
		if (isRising)
		{
			transform.Translate(topPoint.position.x, topPoint.position.y, topPoint.position.z);
			if (gameObject.transform.position == topPoint.position) isRising = false;
		}
		else
		{
			transform.Translate(lowPoint.position.x, lowPoint.position.y, lowPoint.position.z);
			if (gameObject.transform.position == lowPoint.position) isRising = true;
		}
	}
	/*	public GameObject TargetPoint;
	public Vector3 origPoint;
	float distance;
	bool reached = false;
	public float Speed = 0.01f;

	public void Start()
	{
		origPoint = transform.position;
	}

	public void Update()
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
		transform.position = Vector3.MoveTowards(pos, towards, Speed);
	}*/
}
