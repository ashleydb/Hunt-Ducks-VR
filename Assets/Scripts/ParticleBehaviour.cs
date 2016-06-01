using UnityEngine;
using System.Collections;

public class ParticleBehaviour : MonoBehaviour
{

	public float death_timer_ = -5.0f;

	void OnEnable() {
		death_timer_ = 5.0f;
	}

	void Update ()
	{
		if (death_timer_ > 0.0f) {
			death_timer_ -= Time.deltaTime;
			if (death_timer_ <= 0.0f) {
				ParticlePool.Destroy(gameObject);
			}
		}
	}
}

