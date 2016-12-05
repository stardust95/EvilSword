using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : BaseCharacter {

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