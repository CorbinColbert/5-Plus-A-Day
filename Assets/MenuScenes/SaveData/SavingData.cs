using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingData 
{
    public static void SaveData(PlayerData Player)//PlayerData should be the script used for the player data in an active game
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerSaveData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(Player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerSaveData.txt";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream)as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
