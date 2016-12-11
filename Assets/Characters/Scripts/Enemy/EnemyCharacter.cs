using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	public float range;

	public CharacterController controller;

	private PlayerCharacter player;

	public GameObject damageTextObject;

	public int damageTextDuring = 3;

	public GameObject damageEffect;

	private List<GameObject> damageTexts = new List<GameObject>( );

	private Animator animator;

	private GameObject mainCamera;

	private GameObject effectContainer;

	// Use this for initialization
	void Start( ) {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
		player.allEnemies.Add(this.gameObject);
		effectContainer = GameObject.FindGameObjectWithTag("EffectContainer");

	}

	// Update is called once per frame
	void Update( ) {
		updateDamageText( );

		if ( !InRange( ) ) {
			Chase( );
		} else {
			//animator.SetFloat("Forward", 0.0f);
			animator.SetBool("run", false);
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

	void updateDamageText( ) {

		//var transform = mainCamera.transform.position;
		damageTexts.RemoveAll(item => item == null);
		//return;
		foreach ( var text in damageTexts ) {
			text.transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0)); ;
			//text.transform.LookAt(mainCamera.transform.position);
			//text.transform.Rotate(new Vector3(0, 180, 0));
		}

	}
	private void ShowDamageText( string str, bool isCritical = false) {		
		GameObject text =  Instantiate(damageTextObject, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		text.GetComponent<TextMesh>( ).text = str;
		if( isCritical ) {
			text.GetComponent<TextMesh>( ).color = Color.red;
		}
		damageTexts.Add(text);
		Destroy(text, 2f);           // last only 2 seconds

		GameObject effect = Instantiate(damageEffect) as GameObject;
		effect.transform.SetParent(effectContainer.transform);
		effect.transform.position = effectContainer.transform.position;
		Destroy(effect, 2f);
	}	

	public void BeAttacked( ) {
		int damage = (int)Random.Range(100, 200);
		animator.SetBool("hurt", true);
		ShowDamageText(damage.ToString(), damage > 150);
	}

}
