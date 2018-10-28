using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private static AudioSource source;

	[SerializeField]
	private AudioSource mainSource;

	[SerializeField]
	private List<AudioClip> bodyMonsters;

	[SerializeField]
	private List<AudioClip> spiritMonsters;

	[SerializeField]
	private AudioClip death;

	[SerializeField]
	private AudioClip hit;

	[SerializeField]
	private AudioClip minigame_hit;

	[SerializeField]
	private AudioClip powerup;

	private static AudioClip soundDeath;

	private static AudioClip soundHit;

	private static AudioClip soundMinigame_hit;

	private static AudioClip soundPowerup;

	private static List<AudioClip> soundBodyMonsters;

	private static List<AudioClip> soundSpiritMonsters;

	public static void PlaySoundBodyMonster() {
		source.PlayOneShot(soundBodyMonsters[Random.Range(0, soundBodyMonsters.Count)]);
	}

	public static void PlaySoundDeath() {
		source.PlayOneShot(soundDeath);
	}

	public static void PlaySoundHit() {
		source.PlayOneShot(soundHit);
	}
	public static void PlaySoundMinigameHit() {
		source.PlayOneShot(soundMinigame_hit);
	}
	public static void PlaySoundPowerup() {
		source.PlayOneShot(soundPowerup);
	}
	public static void PlaySoundSpiritMonster() {
		source.PlayOneShot(soundSpiritMonsters[Random.Range(0, soundSpiritMonsters.Count)]);
	}

	void Start() {
		source = mainSource;
		soundBodyMonsters = bodyMonsters;
		soundSpiritMonsters = spiritMonsters;
		soundHit = hit;
		soundDeath = death;
		soundMinigame_hit = minigame_hit;
		soundPowerup = powerup;
	}
}
