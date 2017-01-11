using UnityEngine;
using System.Collections;

public class CameraFollowSplitscreen : MonoBehaviour {
	public GameObject player1, player2;

	public float splitCameraOffset = 5;

	float z;
	float splitDistance = 1;
	bool split;
	Vector3 offset;

	TiltedSplitscreen tss;

	// Use this for initialization
	void Start () {
		z = transform.position.z;
		tss = GetComponent<TiltedSplitscreen>();
		Disable();
		//offset = Vector3.up * 1;
		offset = Vector3.up * (transform.position.y - player1.transform.position.y);
	}

	public void Enable(){
		if(tss == null){
			camera.enabled = true;
		}
	}

	public void Disable(){
		if(tss == null){
			camera.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var midpoint = (player1.transform.position + player2.transform.position) * 0.5f + offset;

		if(split){
			var direction = (player2.transform.position - player1.transform.position).normalized * splitCameraOffset  + offset;

			transform.position = new Vector3(player1.transform.position.x + direction.x, player1.transform.position.y + direction.y, z);
			if(Vector3.Distance(player1.transform.position, player2.transform.position) < splitDistance){
				split = false;
				Disable();
			}
		}else{
			transform.position = new Vector3(midpoint.x, midpoint.y, z);

			var screenPoint = camera.WorldToViewportPoint(player1.transform.position + offset);
			
			if(screenPoint.y > 0.8f || screenPoint.y < 0.2f || screenPoint.x > 0.8f || screenPoint.x < 0.2f){
				Enable();
				split = true;
				splitDistance = Vector3.Distance(player1.transform.position, player2.transform.position);
				splitCameraOffset = Vector3.Distance(player1.transform.position + offset, midpoint);
			}
		}
	}
}
