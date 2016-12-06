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
	public bool isAttacking {
		get { return m_isAttacking; }
	}

	private List<GameObject> m_allEnemies = new List<GameObject>( );
	public List<GameObject> allEnemies {
		get { return m_allEnemies;  }
	}

	public new void Start( ) {
		base.Start( );
		m_attribute = new PlayerAttibute( );

	}


	public void ChangeAttackState(int state ) {
		if ( state > 0 )
			m_isAttacking = true;
		else
			m_isAttacking = false;
	}
	
	public void Attack(string attack ) {
		UpdateAnimator(new Vector3( ), attack);
	}

	protected void UpdateAnimator( Vector3 move, string attack ) {

		if ( attack != null ) {
			m_Animator.SetBool(attack, true);
		}

		base.UpdateAnimator(move);
	}
}