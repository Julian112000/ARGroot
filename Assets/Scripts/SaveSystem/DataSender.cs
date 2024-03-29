﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Unity;
using System.Xml;
using System.Net;
using System.IO;
using Mapbox.Json.Linq;

public class DataSender : MonoBehaviour
{
    public static DataSender Instance;
    [SerializeField]
    private string url;

    private void Awake()
    {
        Instance = this;
    }
    public void OnSaveMapScenario(string name, int unitamount)
    {
        StartCoroutine(SaveMapScenario(name, unitamount));
    }
    public void OnSaveMapUnits(string scenename, int order, int id, double latitude, double longitude, double altitude, int rotation)
    {
        StartCoroutine(SaveMapUnits(scenename, order, id, latitude, longitude, altitude, rotation));
    }
    public void OnLoadScenario(int id)
    {
        StartCoroutine(LoadScenario(id));
    }
    public void OnLoadUnits(int number, int number2)
    {
        StartCoroutine(GetUnits(number, number2));
    }
    public void DeleteMapUnits(int order)
    {
        StartCoroutine(RemoveMapUnits(order));
    }
    public void OnSelectScenario(int id)
    {
        StartCoroutine(LoadScenarios(id));
    }
    public void OnSaveLocation(string name, double lat, double lon)
    {
        StartCoroutine(SaveLocation(name, lat, lon));
    }
    public void OnLoadLocation(int id)
    {
        StartCoroutine(LoadLocations(id));
    }
    public void OnDeleteLocation(string name)
    {
        StartCoroutine(DeleteLocation(name));
    }

    IEnumerator DeleteLocation(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "delete");
        form.AddField("name", name);
        WWW www = new WWW(url + "locationapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            //
        }
    }
    IEnumerator LoadLocations(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "load");
        form.AddField("id", id);
        WWW www = new WWW(url + "locationapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            string data = www.text;

            string[] values = data.Split(";"[0]);

            string name = values[0];
            double lat = double.Parse(values[1]);
            double lon = double.Parse(values[2]);
            SearchLocation.Instance.AddLocation(name, lat, lon);
        }
    }
    IEnumerator SaveLocation(string name, double latitude, double longitude)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "create");
        form.AddField("name", name);
        form.AddField("latitude", latitude.ToString());
        form.AddField("longitude", longitude.ToString());
        WWW www = new WWW(url + "locationapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            //
        }
    }
    IEnumerator GetUnits(int sceneid, int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "unit");
        form.AddField("sceneid", sceneid);
        form.AddField("order", id);
        WWW www = new WWW(url + "loadapi.php", form);
        yield return www;
        Debug.Log(www.text);
        if (www.text != "error")
        {
            string data = www.text;

            string[] values = data.Split(";"[0]);

            int db_id = int.Parse(values[0]);
            double lat = double.Parse(values[1]);
            double lon = double.Parse(values[2]);
            float alt = float.Parse(values[3]);
            int rot = int.Parse(values[4]);
            SpawnOnMap.Instance.LoadUnit(db_id, new Mapbox.Utils.Vector2d(lat, lon), alt, rot);
        }
    }
    IEnumerator LoadScenario(int sceneid)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "scenario");
        form.AddField("sceneid", sceneid);
        WWW www = new WWW(url + "loadapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            string data = www.text;
            string[] values = data.Split(";"[0]);

            string db_name = values[0];
            string db_time = values[1];
            int db_amount = int.Parse(values[2]);
            SpawnOnMap.Instance.Load(sceneid, db_name, db_time, db_amount);
        }

    }
    IEnumerator LoadScenarios(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "load");
        form.AddField("sceneid", id);
        WWW www = new WWW(url + "scenarioapi.php", form);
        yield return www;
        if (www.text != "")
        {
            string data = www.text;
            string[] values = data.Split(","[0]);

            int db_id = int.Parse(values[0]);
            string db_name = values[1];
            string db_time = values[2];
            ScenarioEditor.Instance.CreateNewPanel(db_id, db_name, db_time);
        }
    }
    IEnumerator SaveMapUnits(string scenename, int order, int id, double latitude, double longitude, double altitude, int rotation)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "create");
        form.AddField("scenename", scenename);
        form.AddField("type", id);
        form.AddField("latitude", latitude.ToString());
        form.AddField("longitude", longitude.ToString());
        form.AddField("altitude", altitude.ToString());
        form.AddField("rotation", rotation);
        form.AddField("idcount", order);
        WWW www = new WWW(url + "unitapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            //
        }
    }
    IEnumerator SaveMapScenario(string name, int unitamount)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "create");
        form.AddField("name", name);
        form.AddField("data", DateTime.Now.ToString());
        form.AddField("unitamount", unitamount);
        WWW www = new WWW(url + "scenarioapi.php", form);
        yield return www;
        if (www.text != "error")
        {
            ScenarioEditor.Instance.LoadScenarios();
            SpawnOnMap.Instance.SaveUnits(name);
        }
    }
    IEnumerator RemoveMapUnits(int order)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "removal");
        form.AddField("idcount", order);
        WWW www = new WWW(url + "deleteapi.php", form);
        yield return www;
        Debug.Log(www.text);
    }
}
