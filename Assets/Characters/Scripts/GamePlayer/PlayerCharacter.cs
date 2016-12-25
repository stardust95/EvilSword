﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : BaseCharacter {
	

	public GameObject[] Skills;

	public GameObject ActivateEffect;

	private CameraShake cameraShake;

	private bool isAttacking = false;

	private Attribute attribute;

	public bool IsAttacking
	{
		get { return isAttacking; }
	}

	private List<GameObject> m_allEnemies = new List<GameObject>( );
	public List<GameObject> allEnemies {
		get { return m_allEnemies;  }
	}

	public new void Start( ) {
		base.Start( );
		GetComponentInChildren<TrailRenderer>( ).enabled = false;
		cameraShake = GameObject.FindGameObjectWithTag("MyMainCamera").GetComponent<CameraShake>( );
		attribute = GetComponent<Attribute>( );

	}

	public void ChangeAttackState(int state) {
		isAttacking = state > 0;
		GetComponentInChildren<TrailRenderer>( ).enabled = isAttacking;
		//Debug.Log(state);
	}
	
	public void Attack(string attack ) {
		if ( isAttacking || !m_IsGrounded )
			return;

		if( attack.Contains("Skill") ) {
			this.UpdateAnimator("Skill4");
			GameObject activateEffect = Instantiate(ActivateEffect);

			activateEffect.transform.position = this.gameObject.transform.position;

			activateEffect.transform.position += this.gameObject.transform.forward * 1;
			activateEffect.transform.position += new Vector3(0, 1, 0);
			activateEffect.transform.rotation = this.gameObject.transform.rotation;

			Destroy(activateEffect, 1f);

			int skillNum = int.Parse(attack.Substring("Skill".Length)) - 1;
			if( skillNum < Skills.Length ) {
				GameObject effect = Instantiate(Skills[skillNum]) as GameObject;
				effect.transform.position = this.transform.position;
				effect.transform.position += this.transform.forward * 3;
				effect.GetComponent<ProjectileScript>( ).Impact(allEnemies.ToArray());
				Destroy(effect, 5f);
			}

		} else {                // Sword Attack
			this.UpdateAnimator(attack);
			WeaponAttackEnemies( );
		}

	}

	protected void WeaponAttackEnemies( ) {
		foreach ( GameObject enemy in allEnemies ) {
			EnemyCharacter e = enemy.GetComponent<EnemyCharacter>( );

			float distance = Vector3.Distance(e.transform.position, transform.position);

			Vector3 dir = ( e.transform.position - transform.position ).normalized;

			float direction = Vector3.Dot(transform.forward, dir);

			if ( direction > 0 && distance < attribute.attackDistance ) {
				e.BeAttacked( );
				//if ( !isShaking ) StartCoroutine(Shake(0.1f));
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

	public void BeAttacked( ) {
		this.UpdateAnimator("Damaged");
		attribute.TakeDamage("100", false);

	}

	public void Shake( ) {
		cameraShake.ShakeCamera(1f, 0.01f);
	}

}