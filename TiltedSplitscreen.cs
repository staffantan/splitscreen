using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class TiltedSplitscreen : PostEffectsBase {
	public Camera camera2;
	public Transform followTarget1, followTarget2;
	public Shader titledSplitscreenShader;
	RenderTexture otherTexture;
	Material m;

	Vector3 direction;
	float yDist, xDist;
	
	public override void Start() {
		//if(camera2.targetTexture == null || camera2.targetTexture.width != Screen.width)
			camera2.targetTexture = new RenderTexture(Screen.width, Screen.height, 32);

		otherTexture = camera2.targetTexture;
		m = new Material(titledSplitscreenShader);
	}
	
	void Update () {
		direction = (followTarget1.position - followTarget2.position).normalized;
		yDist = followTarget1.position.y - followTarget2.position.y;
		xDist = followTarget1.position.x - followTarget2.position.x;
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination){
		if(camera2.enabled){
			m.SetTexture("_OtherTex", otherTexture);
			m.SetVector("_Vector", new Vector4(-direction.x, direction.y, 0, 0));
			m.SetFloat("_YDistance", yDist);
			m.SetFloat("_XDistance", xDist);
			
			Graphics.Blit(source, destination, m);
		}else{
			Graphics.Blit(source, destination);
		}
	}
}
