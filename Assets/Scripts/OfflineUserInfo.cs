using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class OfflineUserInfo : MonoBehaviour
{
    #region Singleton
    private static OfflineUserInfo _instance;

    public static OfflineUserInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OfflineUserInfo>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<OfflineUserInfo>();
                    singletonObject.name = "----- " + typeof(OfflineUserInfo) + " -----";
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }
    #endregion

    public Mouledoux.Components.Mediator.Subscriptions subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    public Mouledoux.Callback.Callback Videos = null;
    public Mouledoux.Callback.Callback Resets = null;
    public Mouledoux.Callback.Callback Exterior = null;
    public Mouledoux.Callback.Callback Discovery = null;
    public Mouledoux.Callback.Callback Exhibit = null;
    public Mouledoux.Callback.Callback Alcove = null;
    public Mouledoux.Callback.Callback Planetarium = null;
    public Mouledoux.Callback.Callback Comparison = null;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        InitDataPaths();

        Videos += IncreaseVideos;
        Resets += IncreaseResets;
        Exterior += IncreaseExterior;
        Discovery += IncreaseDiscovery;
        Exhibit += IncreaseExhibit;
        Alcove += IncreaseAlcove;
        Planetarium += IncreasePlanetarium;
        Comparison += IncreasePlanetarium;

        subscriptions.Subscribe("VideoStart", Exterior);
        subscriptions.Subscribe("Reset", Resets);
        subscriptions.Subscribe("NavButtonClick:Exterior", Exterior);
        subscriptions.Subscribe("NavButtonClick:Discovery", Discovery);
        subscriptions.Subscribe("NavButtonClick:Exhibit", Exhibit);
        subscriptions.Subscribe("NavButtonClick:Alcove", Alcove);
        subscriptions.Subscribe("NavButtonClick:Planetarium", Planetarium);
        subscriptions.Subscribe("NavButtonClick:Comparison", Comparison);
    }

    private void Update()
    {
        // if(Input.GetKey(KeyCode.Escape))
        //     fileOpenTimer += Time.deltaTime;
        // else
        //     fileOpenTimer = 0f;

        // if(fileOpenTimer >= fileOpenTime)
        // {
        //     AppendToFile();
        //     fileOpenTime = 0f;
        //     OpenCSVLocation();
        // }
    }

    public void OnDestroy()
    {
        AppendToFile();
        subscriptions.UnsubscribeAll();
    }


    public void InitDataPaths()
    {
        if (!Directory.Exists(UnityEngine.Application.dataPath + "/Statistics/"))
        {
            Directory.CreateDirectory(UnityEngine.Application.dataPath + "/Statistics/");
        }

        csvFile = UnityEngine.Application.dataPath + "/Statistics/" + csvFile + ".csv";
    }


    public void AppendToFile()
    {
        string path = csvFile;

        System.IO.StreamWriter file = System.IO.File.AppendText(path);

        if (!File.Exists(path))
        {
            file.WriteLine("Date, Total Videos Played, Application Resets, Exterior, Discovery Room, Exhibit Hall, Alcove, Planetarium, Comparison");
        }

        string[] information = {Date(), videosPlayed.ToString(), resets.ToString(),
        exterior.ToString(), discovery.ToString(), exhibit.ToString(), alcove.ToString(), planetarium.ToString(), comparison.ToString()};

        string info = ConvertToCSVLine(information);

        file.WriteLine(info);
        file.Close();
    }


    public void OpenCSVLocation()
    {
        string winPath = (csvFile).Replace("/", "\\");
        System.Diagnostics.Process.Start("explorer.exe", ("/root," + winPath));
    }

    public static string Date()
    {
        return System.DateTime.Now.Year.ToString() + "/ " +
            System.DateTime.Now.Month.ToString() + "/ " +
            System.DateTime.Now.Day.ToString();
    }

    public string ConvertToCSVLine(string[] strings)
    {
        string returnString = "";

        foreach(string s in strings)
        {
            returnString += (s + ", ");
        }

        return returnString.Substring(0, returnString.Length -2);
    }

    private void IncreaseVideos(Mouledoux.Callback.Packet data)
    {
        videosPlayed++;
    }

    private void IncreaseResets(Mouledoux.Callback.Packet data)
    {
        resets++;
    }
    
    private void IncreaseExterior(Mouledoux.Callback.Packet data)
    {
        exterior++;
    }
    
    private void IncreaseDiscovery(Mouledoux.Callback.Packet data)
    {
        discovery++;
    }
    
    private void IncreaseExhibit(Mouledoux.Callback.Packet data)
    {
        exhibit++;
    }
     
    private void IncreaseAlcove(Mouledoux.Callback.Packet data)
    {
        alcove++;
    }    

    private void IncreasePlanetarium(Mouledoux.Callback.Packet data)
    {
        planetarium++;
    }
         
    private void IncreaseComparison(Mouledoux.Callback.Packet data)
    {
        comparison++;
    }
    
    
    public static string csvFile = "MetaData";

    private int videosPlayed, resets, exterior, discovery, exhibit, alcove, planetarium, comparison;
    private float fileOpenTimer = 0f;
    private float fileOpenTime = 5f;
}