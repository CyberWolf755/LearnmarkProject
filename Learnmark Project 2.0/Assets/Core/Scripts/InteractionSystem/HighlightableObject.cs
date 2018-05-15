using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRInteractableObject2))]
public class HighlightableObject : MonoBehaviour {

	

	[SerializeField]
	private Color highlighColor = new Color(1, 1, 0, 1);

	private Color initialColor;


	// Use this for initialization
	void Start () {
		initialColor = gameObject.GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Highlight()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = highlighColor;
		
	}

	public void UnHighlight()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = initialColor;
		
	}
}
