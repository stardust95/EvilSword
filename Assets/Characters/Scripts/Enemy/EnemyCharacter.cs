using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	public float range;

	public CharacterController controller;

	private PlayerCharacter player;

	private Animator animator;

	//private GameObject mainCamera;

	private Attribute attribute;

	private bool isAttacking;
	private float attackTimer;

	public float AttackInterval = 3f;

	// Use this for initialization
	void Start( ) {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
		player.allEnemies.Add(this.gameObject);
		attackTimer = AttackInterval;
		attribute = GetComponent<Attribute>( );
	}

	// Update is called once per frame
	void Update( ) {
		if ( attribute.IsDeath ) {
			Death( );
			return;
		}

		if ( attackTimer > 0 )
			attackTimer -= Time.deltaTime;

		if ( !InRange( ) ) {
			Chase( );
		} else {
			animator.SetBool("run", false);		// stop running
			if( !isAttacking && attackTimer < 0) {
				Attack( );
			}
		}
		//Debug.Log(InRange( ));
	}

	bool InRange( ) {
		var dis = Vector3.Distance(this.transform.position, player.transform.position);
		if ( dis < range )
			return true;
		return false;
	}

	void Chase( ) {
		Vector3 playerPos = player.transform.position;
		playerPos.y = 0;
		this.transform.LookAt(playerPos);
		controller.Move(this.transform.forward * speed * Time.deltaTime);
		//animator.SetFloat("Forward", 1.0f);
		animator.SetBool("run", true);
	}

	public void BeAttacked() {

		if ( attribute.IsDeath )
			return;

		int damage = (int)Random.Range(100, 200);
		bool critical = damage > 150;

		animator.SetBool("hurt", true);

		attribute.TakeDamage(damage.ToString(), critical);

		if ( critical )
			player.Shake( );
	}

	public void Attack( ) {
		animator.SetBool("attack1", true);

		float distance = Vector3.Distance(player.transform.position, transform.position);

		Vector3 dir = ( player.transform.position - transform.position ).normalized;

		float direction = Vector3.Dot(transform.forward, dir);

		if ( direction > 0 && distance < attribute.attackDistance ) {
			player.BeAttacked( );
		} else {
			//e.BeAttacked(false);
		}

		attackTimer = AttackInterval;
	}

	private void Death( ) {
		animator.SetBool("death", true);

		Destroy(this.gameObject, 3);
	}

}
