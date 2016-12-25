using UnityEngine;

public class Attribute : MonoBehaviour {

	public int HP;
	public int MP;
	public float attack;
	public float defence;
	public float attackDistance;
	public float skillDistance;

	// Use this for initialization
	void Start( ) {

		HP = 100;
		MP = 100;
		attack = 10.0f;
		defence = 10.0f;
		attackDistance = 1.0f;
		skillDistance = 7.0f;
	}

	// Update is called once per frame
	void Update( ) {

	}
}
