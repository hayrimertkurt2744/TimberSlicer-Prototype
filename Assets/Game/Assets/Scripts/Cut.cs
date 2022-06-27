using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Cut : MonoBehaviour
{
    private Material mat;
    private bool isSliceable=false;
    GameObject kesobj;
    public ParticleSystem woodFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sliceable"))
        {
            mat = other.GetComponent<MeshRenderer>().material;
            kesobj = other.gameObject;
            PlayWoodFx();
            /*isSliceable = true;*/
        }
       
    }
    

    void Update()
    {
        if (/*Input.GetMouseButtonDown(0) &&*/ kesobj != null /*&& isSliceable == true*/)
        {
            SlicedHull Kesilen = Kes(kesobj, mat);
            GameObject kesilenust = Kesilen.CreateUpperHull(kesobj, null);
            kesilenust.AddComponent<MeshCollider>().convex = true;
            kesilenust.transform.position = kesobj.transform.position;
            kesilenust.AddComponent<Rigidbody>();
            kesilenust.layer = LayerMask.NameToLayer("Sliceable");
            GameObject kesilenalt = Kesilen.CreateLowerHull(kesobj, mat);
            kesilenalt.AddComponent<MeshCollider>().convex = true;
            kesilenalt.AddComponent<Rigidbody>();
            kesilenalt.transform.position = kesobj.transform.position;
            kesilenalt.layer = LayerMask.NameToLayer("Sliceable");
            Destroy(kesobj);
            

        }

    }
    
	public SlicedHull Kes(GameObject obj, Material crossSectionMaterial)
	{
		return obj.Slice(transform.position, transform.up, crossSectionMaterial);
	}
    void PlayWoodFx()
    {
        woodFx.Play();

    }



}
