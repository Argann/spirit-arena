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

	private static List<AudioClip> soundBodyMonsters;

	private static List<AudioClip> soundSpiritMonsters;

	public static void PlaySoundBodyMonster() {
		source.PlayOneShot(soundBodyMonsters[Random.Range(0, soundBodyMonsters.Count)]);
	}

	public static void PlaySoundSpiritMonster() {
		source.PlayOneShot(soundSpiritMonsters[Random.Range(0, soundSpiritMonsters.Count)]);
	}

	void Start() {
		source = mainSource;
		soundBodyMonsters = bodyMonsters;
		soundSpiritMonsters = spiritMonsters;
	}
}
