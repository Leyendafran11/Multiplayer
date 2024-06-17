using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : NetworkBehaviour
{

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Bala"))
		{
			Runner.Despawn(Object);
		}
		
	}

}
