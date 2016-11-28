using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	public float range;

	public Transform player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( inRange( ) ) {
			this.transform.LookAt(player.position);
		}
		Debug.Log(inRange( ));
	}

	bool inRange( ) {
		if ( Vector3.Distance(this.transform.position, player.position) < range )
			return true;
		return false;
	}

}
