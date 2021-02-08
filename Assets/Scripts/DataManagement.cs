using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    //static used through whole game
    public static DataManagement datamanagement;

    public int highScore;

    //if there is no object in the scene use this object
    private void Awake()
    {
        if (datamanagement == null)
        {
            DontDestroyOnLoad(gameObject);
            datamanagement = this;
        }
        else if (datamanagement != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        //creates binary formatter
        BinaryFormatter BinForm = new BinaryFormatter ();
        //creates file
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        //creates container for data
        gameData data = new gameData();
        data.highscore = highScore;
        //serilises
        BinForm.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists (Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highscore;
        }
    }
}

[Serializable]
class gameData 
{
    public int highscore;
}
