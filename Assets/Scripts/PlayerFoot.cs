using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
	
	public enum Side
	{
		Left,
		Right
	}

	public Side side;
	public PlayerFoot otherFoot;

	private const float SPEED = 3.0f;

	private Rigidbody2D rb;
	private FixedJoint2D fj;
	private HashSet<GameObject> floors;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fj = GetComponent<FixedJoint2D>();
		floors = new HashSet<GameObject>();
	}
	
	private void FixedUpdate()
	{
		//TODO: if both fj disabled, allow movement (midair)

		if (otherFoot.fj.enabled) //if other foot on ground
		{
			float xVel = Input.GetAxis(side + "Horizontal");
			float yVel = Input.GetAxis(side + "Vertical");
			Vector2 vel = new Vector2(xVel, yVel) * SPEED;

			//pop off:
			//compare vel to floor normal (dot product?)
			//magnitude with unity input gravity for "forceful" input?

			if (vel.magnitude > 0.1f)
			{
				fj.enabled = false;
				floors.Clear();
			}

			rb.velocity = vel;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Collide(collision);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		Collide(collision);
	}

	private void Collide(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("LevelGeometry") && !floors.Contains(collision.gameObject))
		{
			floors.Add(collision.gameObject);

			if (collision.rigidbody == null)
			{
				//connect to fixed point in world
				fj.autoConfigureConnectedAnchor = false;
				fj.connectedAnchor = rb.position;
			}
			else
			{
				//connect to other rigidbody
				fj.autoConfigureConnectedAnchor = true;
				fj.connectedBody = collision.rigidbody;
			}
			fj.enabled = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("LevelGeometry"))
		{
			floors.Remove(collision.gameObject);
		}
	}
}
