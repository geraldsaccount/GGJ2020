using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explode : MonoBehaviour {
	[SerializeField] private float delay;
	[SerializeField] private float radius;
	[SerializeField] private float force;
	[SerializeField] private LayerMask explosive;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject fireEffect;
	[SerializeField] private bool explodeOnStart;
	private PauseMenu PauseMenu;

	private void Awake() {
		PauseMenu = FindObjectOfType<PauseMenu>();
	}

	public void StartExploding(bool wasDriving) {
		StartCoroutine(Delay(wasDriving));
	}
	
	// ReSharper disable Unity.PerformanceAnalysis
	public IEnumerator Delay(bool wasDriving) {
		GameObject fire = Instantiate(fireEffect, transform.position, transform.rotation);
		float randDelay = Random.Range(0, delay);
		
		yield return new WaitForSeconds(randDelay);
		Destroy(fire);
		Explosion();
		
		
		yield return new WaitForSeconds(1);
		PauseMenu.Pause(true);
		Destroy(gameObject);
	}
	
	public void Explosion() {
		Instantiate(explosionEffect, transform.position, transform.rotation);
		
		Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider col in collidersToMove) {
			Rigidbody rb = col.GetComponent<Rigidbody>();
			if (rb != null) {
				rb.AddExplosionForce(force, transform.position, radius);
			}
		}

		Collider[] collidersToExplode = Physics.OverlapSphere(transform.position, radius, explosive);
		foreach (Collider col in collidersToExplode) {
			Explode explode = col.GetComponent<Explode>();
			if (explode != null && explode != this) {
				StartCoroutine(AfterExplosion(explode));
			}
		}
		
		
	}

	private IEnumerator AfterExplosion(Explode explode) {
		yield return new WaitForSeconds(2);
		explode.Explosion();
	}
}
