using UnityEngine;

[System.Serializable]
public class Buff {

    public enum BuffType {
		AttackSpeed,
		AttackDmg,
		MovementSpeed,
		Armor,
		BonusTime,
		MaxHealth
	}


    public string description;
    public BuffType type;
    public float mult;
    public Sprite image;
}