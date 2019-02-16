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

	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void Update()
	{
		
	}
}
