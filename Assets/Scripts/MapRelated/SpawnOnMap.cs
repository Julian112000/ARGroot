namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class SpawnOnMap : MonoBehaviour
	{
        public static SpawnOnMap Instance;

        [SerializeField]
        private List<MapUnitData> models = new List<MapUnitData>();
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		float spawnScale = 100f;

        [SerializeField]
		List<GameObject> spawnedObjects;
        [SerializeField]
        List<Vector2d> locations;

        private int currentid;
        private string currentname;
        private string currentdate;

        void Start()
		{
            Instance = this;
			spawnedObjects = new List<GameObject>();
            locations = new List<Vector2d>();
		}
        private void Update()
        {
            int count = spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = spawnedObjects[i];
                var location = locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            }
        }
        public void SaveUnits(string name)
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                DataSaver.SaveMapUnits(spawnedObjects[i], name, i);
            }
        }
        public void Load(int number, string name, string time, int amount)
        {
            foreach (MapUnit unit in FindObjectsOfType(typeof(MapUnit)))
            {
                spawnedObjects.Remove(unit.gameObject);
                Destroy(unit.gameObject);
            }
            for (int i = 0; i < amount; i++)
            {
                DataSender.Instance.OnLoadUnits(number, i);
            }
            currentid = number;
            currentname = name;
            currentdate = time;
        }
        public int GetUnitAmount()
        {
            return spawnedObjects.Count;
        }
        public void LoadUnit(int id, Vector2d location, float altitude, int rotation)
        {
            SpawnObject(models[id].model, location, altitude, rotation);
        }
        public void SpawnObject(GameObject type, Vector2d location, float altitude, int rotation)
        {
            MapUnit instance = Instantiate(type).GetComponent<MapUnit>();
            //rotation
            instance.transform.localPosition = _map.GeoToWorldPosition(location, true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            instance.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
            //data
            instance.latitude = location.x;
            instance.longitude = location.y;
            instance.altitude = altitude;
            instance.rotation = rotation;
            spawnedObjects.Add(instance.gameObject);
            locations.Add(location);

            //ADD NEW ID TO DATABASE AS A NEW TABLE
            //SET ID TO THE ROW
        }
        public void DeleteObject()
        {
            ObjectSelector.Instance.selectedObject.gameObject.SetActive(false);

            DataSender.Instance.DeleteMapUnits(GetUnitOrder(ObjectSelector.Instance.selectedObject.gameObject));
            //DELETE FROM DATABASE
        }
        int GetUnitOrder(GameObject unit)
        {
            int order = -1;
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (spawnedObjects[i] == unit)
                {
                    order = i;
                }
            }
            Debug.Log(order);
            return order;
        }
    }
}