using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour {
	[SerializeField] private float delay;
	[SerializeField] private float radius;
	[SerializeField] private float force;
	[SerializeField] private LayerMask explosive;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject fireEffect;
	[SerializeField] private bool explodeOnStart;
	
	private void Start() {
		if (explodeOnStart) {
			StartCoroutine(Delay());
			
		}
	}

	public void StartExploding() {
		StartCoroutine(Delay());
	}
	
	// ReSharper disable Unity.PerformanceAnalysis
	public IEnumerator Delay() {
		GameObject fire = Instantiate(fireEffect, transform.position, transform.rotation);
		float randDelay = Random.Range(0, delay);
		
		yield return new WaitForSeconds(randDelay);
		Destroy(fire);
		Explosion();
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
				explode.StartExploding();
			}
		}
		
		Destroy(gameObject);
	}
}
