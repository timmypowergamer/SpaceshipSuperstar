using UnityEngine;
using System.Collections.Generic;

public class IOClight : IOCcomp {
	
	public int Probes;
	public float ProbeRadius;
	
	private int probes;
	private float probeRadius;
	private GameObject go;
	private RaycastHit hit;
	private Vector3 hitPoint;
	private SphereCollider probe;
	private Vector3 center;
	private float range;
	private float angle;
	private Ray ray;
	private Vector3 rayDir;
	private int counter;
	private IOCcam iocCam;
	private int frameInterval;
	private bool hidden;
	private Transform parent;
	private int currentLayer;
	private Vector2 rndPoint;
	private GameObject prefab;
	private RaycastHit h;
	private Ray r;
	private Vector3 p;
	
	void Awake () {
		iocCam =  Camera.main.GetComponent<IOCcam>();
		if(iocCam == null)
		{
			this.enabled = false;
		}
		else
		{
			hit = new RaycastHit();
			currentLayer = gameObject.layer;
			h = new RaycastHit();
		}
	}
	
	void Start () {
		UpdateValues();
		Initialize();
		if(GetComponent<Renderer>() == null)
		{
			var r = gameObject.AddComponent<MeshRenderer>();
			r.castShadows = false;
			r.receiveShadows = false;
		}
		prefab = Resources.Load("probe") as GameObject;
		prefab.GetComponent<SphereCollider> ().radius = probeRadius;
		center = transform.position;
		range = light.range;
		angle = light.spotAngle;
		parent = transform;
		switch(light.type)
		{
			case LightType.Point:
				for(int i=0;i<probes;i++)
				{
					ray = new Ray(center, Random.onUnitSphere);
					if(Physics.Raycast(ray, out hit, range))
					{
						go = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;
						go.transform.parent = parent;
						go.layer = currentLayer;
					}
				}
			break;
			
			case LightType.Spot:
				for(int i=0;i<probes;i++)
				{
					rndPoint = Random.insideUnitCircle * (Mathf.Tan(Mathf.Deg2Rad * angle * 0.5f) * range);
					rayDir = ((center + parent.forward * range + parent.rotation * new Vector3(rndPoint.x, rndPoint.y)) - center).normalized;
					ray = new Ray(center, rayDir);
					if(Physics.Raycast(ray, out hit, range))
					{
						go = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;
						go.transform.parent = parent;
						go.layer = currentLayer;
					}
				}
			break;
		}
	}

	public void Initialize()
	{
		light.enabled = false;
		light.renderMode = LightRenderMode.ForcePixel;
		hidden = true;
	}

	public void UpdateValues () {
		if(Probes != 0)
		{
			probes = Probes;
		}
		else probes = iocCam.lightProbes;
		if(ProbeRadius != 0)
		{
			probeRadius = ProbeRadius;
		}
		else probeRadius = iocCam.probeRadius;
	}
	
	public override void UnHide(RaycastHit hit) {
		counter = Time.frameCount;
		hitPoint = transform.worldToLocalMatrix.MultiplyPoint(hit.point);
		if(hidden)
		{
			hidden = false;
			light.enabled = true;
		}
	}
	public void Hide() {
		hidden = true;
		light.enabled = false;
	}
	void Update() {
		frameInterval = Time.frameCount % 6;
		if(!hidden && frameInterval == 0)
		{
			if(Time.frameCount - counter > iocCam.hideDelay)
			{
				if(iocCam.preCullCheck && renderer.isVisible)
				{
					p = transform.localToWorldMatrix.MultiplyPoint(hitPoint);
					r = new Ray(p, iocCam.transform.position - p);
					if(Physics.Raycast(r, out h, iocCam.viewDistance))
					{
						if(!h.collider.CompareTag(iocCam.tag))
						{
							Hide();
						}
						else
						{
							counter = Time.frameCount;
						}
					}
				}
				else
				{
					Hide();
				}
			}
		}
	}
}
