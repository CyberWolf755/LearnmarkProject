using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRInteractableObject2))]
public class HighlightableObject : MonoBehaviour {

	

	[SerializeField]
	private Color highlightColor = new Color(1, 1, 0, 1);

	private Color initialColor;


	// Use this for initialization
	void Start () {
		initialColor = gameObject.GetComponent<MeshRenderer>().material.color;
	}


	public void Highlight()
	{
		
		
	}

	public void UnHighlight()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = initialColor;
		
	}
}
