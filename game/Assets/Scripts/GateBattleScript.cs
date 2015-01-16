﻿using UnityEngine;
using System.Collections;

public class GateBattleScript : MonoBehaviour {
	private BoxCollider coll;
	private Coordinates coords;
	private TileManager tileManager;
	private int rotation = 0;
	private bool flag = false;
	public GameObject opponent;
	private Vector3 pos;
	private Quaternion rot;

	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<BoxCollider>();
		coords = TileManager.PosToCoordinates(gameObject.transform.position);
		tileManager = GameObject.Find ("Third Person Camera").GetComponent<TileManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		pos = (GameObject.Find("Bike")).transform.position;
		pos.x = pos.x + 2;
		rot = Quaternion.identity;
	}

	private void MoveGate(int relative_x, int relative_z, int relative_rotation){
		coords.X += relative_x;
		coords.Z += relative_z;
		rotation = (rotation + relative_rotation) % 360;
		gameObject.transform.position = TileManager.CoordinatesToPos(coords);
		gameObject.transform.eulerAngles = Vector3.up * (rotation);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("Player")){
			tileManager.CreateArena();
			GameObject gate = GameObject.Find ("Gate");
			Destroy(gate);
			Destroy(this.gameObject);
			if (flag == false) { 
				var opp = Instantiate(opponent, pos, rot);
				opp.name = "Opponent";
				flag = true;
			}
		}
	}
}