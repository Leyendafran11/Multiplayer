using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conector : MonoBehaviour
{
    private NetworkRunner red;

	private void Start()
	{
		red = GetComponent<NetworkRunner>();
		red.name = "Que grande lokete. MONDONGO";
		red.ProvideInput = true;

		
	}

	public void pulsarBoton()
	{
		Dictionary<string, SessionProperty> Propiedades = new Dictionary<string, SessionProperty>();
		Propiedades.Add("Pelotas", (SessionProperty)0);


		red.StartGame(
					new StartGameArgs
					{
						GameMode = GameMode.AutoHostOrClient,
						SessionName = "Detras del humo no se ve",
						Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
						SceneManager = this.GetComponent<NetworkSceneManagerDefault>(),
						SessionProperties = Propiedades
					}
				  );
	}
}
