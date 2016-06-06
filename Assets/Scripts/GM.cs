using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GM : MonoBehaviour {

	public enum GameState { GS_MENU = 0, GS_TIMER };

	public Text timeText_;
	public Text scoreText_;
	public Spawner spawner;

	private GameState state_ = GameState.GS_MENU;
	private float timer_ = 60.0f;
	private int score_ = 0;

	// Singleton
	public static GM instance_;

	void Awake() {
		if (instance_ == null) {
			instance_ = this;
		}
	}

	void OnDestroy() {
		if (instance_ == this) {
			instance_ = null;
		}
	}

	public static GameState GetState() {
		return instance_.state_;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state_) {
		case GameState.GS_MENU:
			break;
		case GameState.GS_TIMER:
			// Reduce the timer and take us to the Menu if time runs out
			timer_ -= Time.deltaTime;
			timeText_.text = timer_.ToString ("F1"); // e.g. 1dp, e.g. 12.0
			if (timer_ <= 0.0f) {
				ChangeState (GameState.GS_MENU);
			}
			break;
		}
	}

	public static void ChangeState(GameState state) {
		// Cleanup old state
		switch (instance_.state_) {
		case GameState.GS_MENU:
			// TODO: Maybe hide the UI?
			break;
		case GameState.GS_TIMER:
			// TODO: Maybe play a sound or something, since a game would have just finished
			instance_.spawner.RemoveAllEntities ();
			instance_.timeText_.text = "0.0";
			break;
		}

		// Start new state
		switch (state) {
		case GameState.GS_MENU:
			// TODO: Maybe show the UI?
			break;
		case GameState.GS_TIMER:
			// TODO: Maybe play a sound or something, since a game would have just started
			instance_.score_ = 0;
			instance_.timer_ = 60.0f;
			instance_.scoreText_.text = "0";
			instance_.timeText_.text = "60.0";
			instance_.spawner.SpawnEntities (5);
			break;
		}

		instance_.state_ = state;
	}

	public static void TargetHit() {
		switch (instance_.state_) {
		case GameState.GS_MENU:
			Debug.Log ("Warn: TargetHit in GS_MENU");
			break;
		case GameState.GS_TIMER:
			++instance_.score_;
			instance_.scoreText_.text = instance_.score_.ToString();
			instance_.spawner.SpawnEntities (5);
			break;
		}
	}
}
