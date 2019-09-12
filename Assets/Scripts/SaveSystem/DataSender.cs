using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    public static DataSender Instance;
    [SerializeField]
    private string url;

    private void Awake()
    {
        Instance = this;
    }
    public void OnSaveMapUnits(int id, double latitude, double longitude, double altitude, int rotation)
    {
        StartCoroutine(SaveMapUnits(id, latitude, longitude, altitude, rotation));
    }
    IEnumerator SaveMapUnits(int id, double latitude, double longitude, double altitude, int rotation)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "create");
        form.AddField("id", id);
        form.AddField("latitude", latitude.ToString());
        form.AddField("longitude", longitude.ToString());
        form.AddField("altitude", altitude.ToString());
        form.AddField("rotation", rotation);
        WWW www = new WWW(url + "createunitapi.php", form);
        yield return www;
    }
}
