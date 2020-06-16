using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
	[SerializeField] private float cityLength;
	[SerializeField] private float platformWidth;
	[SerializeField] private List<GameObject> buildings;

	private enum Alignment
	{
		Right,
		Left
	}

	[ContextMenu("Create City")]
	public void CreateCity()
	{
		if(buildings == null) return;

		GameObject city = GameObject.Find("City");
		if(city != null) DestroyImmediate(city);

		city = new GameObject("City");

		CreateSide(Alignment.Right, -platformWidth / 2f, cityLength, city.transform);
		CreateSide(Alignment.Left, platformWidth / 2f, cityLength, city.transform);
	}

	private void CreateSide(Alignment alignment, float removal, float length, Transform parent)
	{
		float posZ = 0f;
		
		while(posZ < length)
		{
			Vector3 buildingSize = CreateBuilding(alignment, removal, posZ, parent);
			posZ += buildingSize.z;
		}
			
	}

	private Vector3 CreateBuilding(Alignment alignment, float removal, float posZ, Transform parent)
	{
		GameObject building = Instantiate(buildings[Random.Range(0, buildings.Count)], parent);
		
		Vector3 size = building.GetComponent<BoxCollider>().size;
		Vector3 scale = building.transform.localScale;
		size = Vector3.Scale(size, scale);

		if(building.transform.localRotation.eulerAngles.y != 0f)
			size = SwapSize(size);

		Vector3 pos = Vector3.zero;
		
		switch(alignment)
		{
			case Alignment.Right:
				pos.x = removal - size.x / 2f;
				break;
			case Alignment.Left:
				pos.x = removal + size.x / 2f;
				break;
		}

		pos.y = Random.Range(-1f, 2f);
		pos.z = posZ + size.z / 2f;
		
		// posZ += size.z;
		
		building.transform.position = pos;

		return size;	
	}

	private Vector3 SwapSize(Vector3 size)
	{
		return new Vector3(size.z, size.y, size.x);
	}
}
