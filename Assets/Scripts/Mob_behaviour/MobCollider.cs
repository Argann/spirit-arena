using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MobCollider : MonoBehaviour {
	public float lifePoints = 10;
	public int damage = 1;
	public bool isBoss = false;
	public bool isDead = false;
	public bool isIntro = false;

	private ParticleSystem partSys;

	void Start() {
		partSys = GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.CompareTag(gameObject.tag)) {
			// Si le monstre est touché par une balle
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.StopMe();

			partSys.Play();

			float dealtDamages = Mathf.Min(lifePoints, bullet.damages);
			bullet.player.Points = bullet.player.Points + (int)(dealtDamages * 100);
			if (isIntro) dealtDamages = 1f;
			lifePoints -= dealtDamages;
			if (!isIntro) SoundManager.PlaySoundHit();
			if (lifePoints <= 0)
			{
				if (!isIntro) WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
				damage = 0;
				isDead = true;
			}
			Destroy(bullet.gameObject);

		} else if (coll.GetComponent<Collider2D>().tag == "Player" && !isDead) {
			// Si le monstre touche un joueur
			PlayerControls player = coll.GetComponent<PlayerControls>();
			player.TakeDamages(damage);
			if (isBoss) {
				player.Recoil(5*new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0f));
			}
			else {
				if (!isIntro) WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
				damage = 0;
			}
		}
	}

	public void DestroyMe() {
		Destroy(gameObject, 0f);
	}
}
