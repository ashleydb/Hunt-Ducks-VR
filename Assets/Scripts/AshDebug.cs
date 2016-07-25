using UnityEngine;
using System.Collections;

public class AshDebug : MonoBehaviour {

	public void StartTimerGame() {
		GM.ChangeState (GM.GameState.GS_TIMER);
	}

}
