using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
using System.Linq;

public class LobbyController : NetworkBehaviour
{

    public TextMeshProUGUI titulo;
	private bool loadLobby;
	private bool loadScene;

	private void Awake()
	{
		loadLobby = false;
		loadScene = false;
	}

	public override void Spawned()
	{
		loadLobby=true;
	}

	public override void Render()
	{
		if (loadLobby)
		{
			titulo.text = "Esperando jugadores . . . " + Runner.ActivePlayers.Count() + "/2";

			if (Runner.IsSceneAuthority && Runner.ActivePlayers.Count() == 2)
			{
				cargarEscena();
			}
		}

	}

	private void cargarEscena()
	{
		if (!loadScene)
		{
			Runner.UnloadScene(SceneRef.FromIndex(1));
			Runner.LoadScene(SceneRef.FromIndex(2));
			loadScene = true;
		}
		
	}

}
