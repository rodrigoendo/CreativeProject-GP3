﻿using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {
	
		private int _targetWaypoint = 0;
		//private BaseLevelScript _levelScript;
		private Transform _waypoints;
		
	private RaycastHit hit;

		public float movementSpeed = 2f;
		
		// Use this for initialization
		void Start () 
		{
			//_levelScript = GameObject.Find("LevelScript").GetComponent<BaseLevelScript>();
			_waypoints = GameObject.Find("Waypoints").transform;
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}
		
		// Fixed update
		void FixedUpdate()
		{
			handleWalkWaypoints();
		}
		
		// Handle walking the waypoints
		private void handleWalkWaypoints()
		{
			Transform targetWaypoint = _waypoints.GetChild(_targetWaypoint);
			Vector3 relative = targetWaypoint.position - transform.position;
			Vector3 movementNormal = Vector3.Normalize(relative);
			float distanceToWaypoint = relative.magnitude;
			float targetAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;
			
			if (distanceToWaypoint < 0.1)
			{
				if (_targetWaypoint + 1 < _waypoints.childCount)
				{
					// Set new waypoint as target
					_targetWaypoint++;
				}
				else
				{
					// Inform level script that a unit has reached the last waypoint
//					_levelScript.reduceHearts(1);
					Destroy(gameObject);
					return;
				}
			}
			else
			{
				// Walk towards waypoint
				rigidbody2D.AddForce(new Vector2(movementNormal.x, movementNormal.y) * movementSpeed);
			}
			
			// Face walk direction
			transform.rotation = Quaternion.Euler(0, 0, targetAngle);
		}

}
