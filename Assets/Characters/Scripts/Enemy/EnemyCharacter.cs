using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	public float range;

	public CharacterController controller;

	public Transform player;
	
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>( );
	}
	
	// Update is called once per frame
	void Update () {
		if ( !InRange( ) ) {
			Chase( );
		} else {
			animator.SetFloat("Forward", 0.0f);
		}
		//Debug.Log(InRange( ));
	}

	bool InRange( ) {
		var dis = Vector3.Distance(this.transform.position, player.position);
		if ( dis < range  )
			return true;
		return false;
	}

	void Chase( ) {
		Vector3 playerPos = player.position;
		playerPos.y = 0;
		this.transform.LookAt(playerPos);
		controller.Move(this.transform.forward * speed * Time.deltaTime);
		animator.SetFloat("Forward", 1.0f);
		
	}

}
