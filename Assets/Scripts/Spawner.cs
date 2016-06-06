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
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public List<Transform> spawn_locations_ = new List<Transform>();

	int spawnCount = 0;
    float spawn_timer_ = 0.0f;
    ObjectPool duckPool;
    ObjectPool leafPool;

    void Awake() {
        // Hide all of the spawn point meshes when the game starts
		foreach (var transform in spawn_locations_) {
			transform.GetComponentInParent<MeshRenderer> ().enabled = false;
		}

        // Make sure the pool object is tagged appropriately in the editor, (FindWithTag is quicker than Find using strings)
        duckPool = GameObject.FindWithTag("DuckPool").GetComponent<ObjectPool>();
        leafPool = GameObject.FindWithTag("LeafParticlePool").GetComponent<ObjectPool>();

		// Whenever we start spawning characters, this is how long the first will take
		spawn_timer_ = Random.Range(0.2f, 3.0f);
    }

	// Call this when starting a game, or after another enemy has been killed to make more appear
	public void SpawnEntities(int numberToSpawn) {
		spawnCount += numberToSpawn;
	}

	public void RemoveAllEntities() {
		spawnCount = 0;
		spawn_timer_ = Random.Range(2.0f, 5.0f);
		duckPool.DestroyAllActive ();
	}

	void Update() {
		if (spawnCount > 0 && spawn_timer_ > 0.0f) {
			spawn_timer_ -= Time.deltaTime;

			if (spawn_timer_ <= 0.0f) {
				spawn_timer_ = Random.Range(2.0f, 5.0f);
				--spawnCount;

				int spawn_pos_index = Random.Range(0, spawn_locations_.Count);
				Transform at = spawn_locations_[spawn_pos_index];

                // Spawn an enemy and a particle effect at the spawn point
                duckPool.Create(at.position);
                leafPool.Create(at.position);
            }
		}

	}
}
