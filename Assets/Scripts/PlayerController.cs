using Fusion;
using Fusion.Addons.Physics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
	private NetworkRigidbody3D rb;
	private GameObject boquilla;
	private ChangeDetector changeDetector;
	private Vector3 SpawnPos;
	public GameObject bala;
	public TextMesh txtVida;

	[Networked]
	public int vida { get; set; }
	

	private void Awake()
	{
		rb = GetComponent<NetworkRigidbody3D>();
		boquilla = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject;
	}

	public override void Spawned()
	{
		changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
		this.vida = 10;

		/*if (HasInputAuthority)
		{
			Camera c = Camera.main;
			c.transform.SetParent(this.transform);
			c.transform.position = this.transform.position + new Vector3(0.0199999996f, 2.17000008f, -3.83999991f);	
		}*/

		SpawnPos = this.transform.position;
	
	}

	public override void FixedUpdateNetwork()
	{
		if (GetInput(out InputData inputData))
		{

			/*if (HasInputAuthority && inputData.disparo == true)
			{
				RPC_Vida();
			}*/


			if (HasStateAuthority)
			{
				rb.Rigidbody.MovePosition(transform.position + transform.forward * inputData.movimiento * 5 * Runner.DeltaTime);

				Quaternion rotacion = Quaternion.Euler(new Vector3(0,inputData.rotacion,0) * Runner.DeltaTime * 100);

				rb.Rigidbody.MoveRotation(rb.Rigidbody.rotation * rotacion);

				if (inputData.disparo == true)
				{
					NetworkObject b = Runner.Spawn(bala, boquilla.transform.position, Quaternion.Euler(90,rb.Rigidbody.rotation.y*360, 0), Object.InputAuthority);
					Debug.Log(rb.Rigidbody.rotation.y*360);
					b.GetComponent<NetworkRigidbody3D>().Rigidbody.AddForce(boquilla.transform.forward * 50,ForceMode.Impulse);

					
				}
			}	

		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (HasStateAuthority)
		{
			if (collision.gameObject.CompareTag("Bala"))
			{
				vida--;

				if (vida == 0)
				{
					//Runner.Despawn(Object);

					this.transform.position = SpawnPos;
					this.vida = 10;
				}
			}
			else if (collision.gameObject.CompareTag("pelota"))
			{
				ReadOnlyDictionary<string, SessionProperty> P = Runner.SessionInfo.Properties;

				if (P.TryGetValue("Pelotas", out SessionProperty data))
				{
					int p = (int) data.PropertyValue;
					p++;

					Dictionary<string, SessionProperty> Propiedades = new Dictionary<string, SessionProperty>();
					Propiedades.Add("Pelotas", (SessionProperty)p);
				}
			}
		}
		
	}

	public override void Render()
	{
		foreach (var change in changeDetector.DetectChanges(this))
		{
			switch (change)
			{
				case nameof(vida):
					txtVida.text = vida.ToString();
				break;
			}
		}
		txtVida.text = vida.ToString();
	}

	/*[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
	public void RPC_Vida(RpcInfo info = default)
	{
		this.vida++;
	}*/
}
