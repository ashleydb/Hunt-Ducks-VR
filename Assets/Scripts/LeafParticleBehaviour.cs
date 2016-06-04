using UnityEngine;
using System.Collections;

public class LeafParticleBehaviour : MonoBehaviour
{
    float death_timer_ = -5.0f;

	private GvrAudioSource audio_source_;

	void OnEnable() {
		death_timer_ = 5.0f;
		PlaySound ();
	}

	void Update ()
	{
		if (death_timer_ > 0.0f) {
			death_timer_ -= Time.deltaTime;
			if (death_timer_ <= 0.0f) {
                // Make sure the pool object is tagged appropriately in the editor, (FindWithTag is quicker than Find using strings)
                ObjectPool pool = GameObject.FindWithTag("LeafParticlePool").GetComponent<ObjectPool>();
                pool.Destroy(gameObject);
			}
		}
	}

	void PlaySound() {
		if (audio_source_ == null)
			return;

		audio_source_.loop = false;
		audio_source_.Play();
	}
}

