using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RailNode : MonoBehaviour
{

	public bool isBroken;
	public bool isExplosionTrigger;
	public List<RailNode> connectingNodes;
	public GameObject POI;
	public Vector3 distanceFromPOI;
	public RailNode nextNode;
	public float distMag;

	void FixedUpdate()
	{
		distanceFromPOI = this.transform.position - POI.transform.position;
		distMag = distanceFromPOI.magnitude;
		nextNode = connectingNodes.OrderBy(d => d.distanceFromPOI.magnitude).FirstOrDefault();
	}

}
