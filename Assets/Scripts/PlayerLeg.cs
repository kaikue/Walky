using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeg : MonoBehaviour
{
	public GameObject foot;
	public GameObject head;

	private LineRenderer lr;

	private void Start()
	{
		lr = GetComponent<LineRenderer>();
	}
	
	private void Update()
	{
		Vector3[] positions = { foot.transform.position, head.transform.position };
		lr.SetPositions(positions);
	}
}
