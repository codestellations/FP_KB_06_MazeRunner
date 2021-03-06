﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour {
	
	private Transform target;
	public float speed = 20;
	public float waitTime = .75f;
	public Animator animator;
	Vector2[] path;
	int targetIndex;
	public float lookRadius = 6f;
	Vector2 defaultPosition;
	bool once = true;
	public DialogueManager dialogue;

	void Start() {
		// defining enemy's original position
		defaultPosition = (Vector2)transform.position;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		// function for the pathfinding to start
		StartCoroutine (RefreshPath ());
	}

	IEnumerator RefreshPath() {
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially
			
		while (true) {
			// if the player is within look radius
			if(Vector2.Distance(transform.position, target.position) <= lookRadius){
				if(once){
					dialogue.RequestSentence(7, 8);
					once = false;
           	 	}

				if (targetPositionOld != (Vector2)target.position) {
					targetPositionOld = (Vector2)target.position;

					path = Pathfinding.RequestPath (transform.position, target.position);
					StopCoroutine ("FollowPath");
					StartCoroutine ("FollowPath");
				}

				yield return new WaitForSeconds (waitTime);
			}

			// else the enemy will go back to its original position
			else{
				yield return new WaitForSeconds(2f);
				path = Pathfinding.RequestPath (transform.position, defaultPosition);
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}
		}
	}
		
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			animator.SetBool("isWalking", true);
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if ((Vector2)transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}

				setDirection(currentWaypoint);

				transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;
			}
		}
		animator.SetBool("isWalking", false);
	}

	void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Player")){
            StartCoroutine(gotPlayer());
        }
    }

	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}	
			}
		}
	}

	public void setDirection(Vector2 currentWaypoint){
		if(currentWaypoint.x - transform.position.x < 0){
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(currentWaypoint.x - transform.position.x > 0){
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}

	IEnumerator gotPlayer(){
		if(PlayerStats.health != 0){
			PlayerStats.healthDecrease();
			yield break;
		}
		else{
			dialogue.RequestSentence(9, 10);

			yield return new WaitForSeconds(3f);

			SceneManager.LoadScene(sceneBuildIndex:0);
		}
	}
}
