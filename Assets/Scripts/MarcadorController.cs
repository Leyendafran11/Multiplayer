using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

public class MarcadorController : NetworkBehaviour
{
    public TextMeshPro texto;

    [Networked]
    public int pelotas { get; set; }

	public override void Spawned()
	{
		if (HasStateAuthority)
		{
			pelotas = 0;
		}
	
	}

	private void Update()
	{
		ReadOnlyDictionary<string, SessionProperty> P = Runner.SessionInfo.Properties;

		if (P.TryGetValue("Pelotas", out SessionProperty data))
		{
			if (HasStateAuthority)
			{
				pelotas = (int)data.PropertyValue;	
			}
		}
	}

	public override void Render()
	{
		texto.text = pelotas.ToString();
	}
}
