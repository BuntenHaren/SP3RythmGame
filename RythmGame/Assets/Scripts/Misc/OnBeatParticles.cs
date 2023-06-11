using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeatParticles : MonoBehaviour
{
	private ParticleSystem p;
	private ParticleSystem.Particle[] particles;
	private Transform target;
	private Transform thisTransform;
	[SerializeField]
	private float affectDistance;

	private Timer timer;
	private bool moveParticlesToTarget;
	[SerializeField]
	private float timeBeforeMoveToPlayer;

	void Start()
	{
		timer = new Timer();
		timer.TimerDone += () => moveParticlesToTarget = true;
		p = GetComponent<ParticleSystem>();
		target = GameObject.Find("Player").transform;

		timer.StartTimer(timeBeforeMoveToPlayer);
	}


	void Update()
	{
		timer.UpdateTimer(Time.fixedDeltaTime);

		particles = new ParticleSystem.Particle[p.particleCount];

		p.GetParticles(particles);
		if (moveParticlesToTarget)
		{
			for (int i = 0; i < particles.GetUpperBound(0); i++)
			{

				float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (8 * Vector3.Distance(target.position, particles[i].position));

				//Debug.DrawRay (particles [i].position, (target.position - particles [i].position).normalized * (ForceToAdd/10));

				particles[i].velocity = (target.position - particles[i].position).normalized * ForceToAdd;

				//particles [i].position = Vector3.Lerp (particles [i].position, target.position, Time.deltaTime / 2.0f);

			}
		}

		p.SetParticles(particles, particles.Length);

	}
}
