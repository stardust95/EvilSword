using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : BaseCharacter {

	public class PlayerAttibute {
		public int HP;
		public int MP;
		public float attack;
		public float defence;
		public float attackDistance;
		public float skillDistance;

		public PlayerAttibute( ) {
			HP = 100;
			MP = 100;
			attack = 10.0f;
			defence = 10.0f;
			attackDistance = 1.0f;
			skillDistance = 7.0f;
		}

	}

	PlayerAttibute m_attribute;

	private bool m_isAttacking = false;
	public bool isAttacking
	{
		get { return m_isAttacking; }
	}

	private List<GameObject> m_allEnemies = new List<GameObject>( );
	public List<GameObject> allEnemies {
		get { return m_allEnemies;  }
	}

	public new void Start( ) {
		base.Start( );
		m_attribute = new PlayerAttibute( );
		GetComponentInChildren<TrailRenderer>( ).enabled = false;
		
	}

	public void ChangeAttackState(int state) {
		m_isAttacking = state > 0;
		GetComponentInChildren<TrailRenderer>( ).enabled = m_isAttacking;
		Debug.Log(state);
	}
	
	public void Attack(string attack ) {
		//UpdateAnimator(new Vector3( ), attack);
		CalcAttackEnemies( );
	}

	protected void CalcAttackEnemies( ) {
		foreach ( GameObject enemy in allEnemies ) {
			EnemyCharacter e = enemy.GetComponent<EnemyCharacter>( );

			float distance = Vector3.Distance(e.transform.position, transform.position);

			Vector3 dir = ( e.transform.position - transform.position ).normalized;

			float direction = Vector3.Dot(transform.forward, dir);

			if ( direction > 0 && distance < m_attribute.attackDistance ) {
				e.BeAttacked( );
			} else {
				//e.BeAttacked(false);
			}
		}
		return;
	}

	public void UpdateAnimator(string animClip ) {

		if ( animClip != null ) {
			m_Animator.SetBool(animClip, true);
		}

	}

	public void UpdateAnimator( Vector3 move, string animClip ) {

		UpdateAnimator(animClip);

		base.UpdateAnimator(move);
	}
}