// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogBehaviour : MonoBehaviour {

	public GameObject menu_;
	public GameObject leftButton_;
	public GameObject rightButton_;

	public List<AudioClip> attention_sounds_ = new List<AudioClip>();
	public List<AudioClip> select_sounds_ = new List<AudioClip>();

	private GvrAudioSource audio_source_;

	void OnEnable() {
		menu_.SetActive (false);
		audio_source_ = GetComponent<GvrAudioSource>();
		StartCoroutine(PlayAwakeSound());
	}

	IEnumerator PlayAwakeSound() {
		yield return new WaitForEndOfFrame();

		PlaySound(attention_sounds_);
	}

	void Update() {
		// TODO: Probably need a state, (or check state elsewhere in GM,) where if the game isn't running and either the menu isn't open
		// or hasn't been interacted with for some time, play an attention sound.

		// The menu buttons may have been hidden while we were playing a game, so unhide them
		if (GM.GetState () == GM.GameState.GS_MENU)
			ShowMenuButtons (true);
	}

	// Makes the dog bark when he or a menu item is clicked
	public void OnSelect() {
		PlaySound(select_sounds_);
		menu_.SetActive (!menu_.activeSelf);
	}

	// Start a game
	public void OnLeftButton() {
		PlaySound(select_sounds_);
		ShowMenuButtons(false);
		// TODO: This should check for the game mode we've selected using the right button
		GM.ChangeState (GM.GameState.GS_TIMER);
	}

	// TODO: Change Game Mode
	public void OnRightButton() {
		PlaySound(select_sounds_);
		//ShowMenuButtons(false);
		// TODO: Would want to change the mode-to-play state
		// TODO: Change the logo on the button for different modes, (icons would be better than button text.)
		// TODO: Would also want to make the buttons look like speech bubbles...
	}

	public void ShowMenuButtons(bool visible = true) {
		leftButton_.SetActive(visible);
		rightButton_.SetActive(visible);
	}

	void PlaySound(List<AudioClip> sounds) {
		if (sounds.Count == 0 || audio_source_ == null)
			return;

		AudioClip clip = sounds[Random.Range(0, sounds.Count)];
		audio_source_.clip = clip;
		audio_source_.loop = false;
		audio_source_.Play();
	}
}
