using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	//public float range;

	public int searchRange = 10;

	public CharacterController controller;

	private PlayerCharacter player;

	private Animator animator;

	//private GameObject mainCamera;

	private Attribute attribute;

	private bool isFoundPlayer;
	private bool isAttacking;
	private float attackTimer;

	public float AttackInterval = 3f;

	private NavMeshAgent navAgent = null;

	private GameObject[ ] mobPoints;
	private int mobPointIndex = -1;

	// Use this for initialization
	void Start( ) {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
		player.allEnemies.Add(this.gameObject);
		attackTimer = AttackInterval;
		attribute = GetComponent<Attribute>( );
		navAgent = GetComponent<NavMeshAgent>( );
		mobPoints = GameObject.FindGameObjectsWithTag("MobPoint");
		if ( mobPoints.Length > 0 ) {
			mobPointIndex = 0;
			navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
		}
	}

	// Update is called once per frame
	void Update( ) {
		if ( attribute.IsDeath ) {
			Death( );
			return;
		}

		if ( attackTimer > 0 )
			attackTimer -= Time.deltaTime;

		if( InRange(attribute.attackDistance) ) {
			if ( !isAttacking && attackTimer < 0 ) {
				Attack( );
			}
		} else if ( InRange(searchRange) ) {
			navAgent.SetDestination(player.transform.position);
			animator.SetBool("run", true);
		} else if ( navAgent.remainingDistance < 1 ) {       // arrive at one patrol point
			mobPointIndex = ( mobPointIndex + 1 ) % mobPoints.Length;
			navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
			animator.SetBool("run", true);
		}

		
		//Debug.Log(InRange( ));
	}

	bool InRange(float range) {
		var dis = Vector3.Distance(this.transform.position, player.transform.position);
		if ( dis < range )
			return true;
		return false;
	}

	//void Chase( ) {
	//	Vector3 playerPos = player.transform.position;
	//	playerPos.y = 0;
	//	this.transform.LookAt(playerPos);
	//	controller.Move(this.transform.forward * speed * Time.deltaTime);
	//	//animator.SetFloat("Forward", 1.0f);
	//	animator.SetBool("run", true);
	//}

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
		animator.SetBool("run", false);
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
