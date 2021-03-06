﻿using UnityEngine;
using System.Collections;

public class HitParticleBehaviour : MonoBehaviour
{
	float death_timer_ = -3.0f;

	void OnEnable() {
		death_timer_ = 3.0f;
	}

	void Update ()
	{
		if (death_timer_ > 0.0f) {
			death_timer_ -= Time.deltaTime;
			if (death_timer_ <= 0.0f) {
                // Make sure the pool object is tagged appropriately in the editor, (FindWithTag is quicker than Find using strings)
                ObjectPool pool = GameObject.FindWithTag("HitParticlePool").GetComponent<ObjectPool>();
                pool.Destroy(gameObject);
			}
		}
	}
}

