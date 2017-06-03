using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class SaveParams
{
    //Позиция
    public float posx = 0;
    public float posy = 0;
    public float posz = 0;

    //Поворот
    public float rotx = 0;
    public float roty = 0;
    public float rotz = 0;

    public List<int> objects = new List<int>();
}

public class SaveSystem : MonoBehaviour {

    [SerializeField]
    private string filename = "playersave";
    private Inventory invent;
    private SaveParams saveparams = new SaveParams();
    public LoadSys load;

    private void Start()
    {
        invent = GetComponent<Inventory>();
        load = GameObject.FindObjectOfType<LoadSys>();
        if (load.isload)

            Invoke("Load", 0.1f);
    }

    public void Save()
    {
        try {

            saveparams.posx = transform.position.x;
            saveparams.posy = transform.position.y;
            saveparams.posz = transform.position.z;

            saveparams.rotx = transform.rotation.x;
            saveparams.roty = transform.rotation.y;
            saveparams.rotz = transform.rotation.z;

            invent = GetComponent<Inventory>();

            foreach (var i in invent.list)
            {
                saveparams.objects.Add(i.id);
            }
            if (!Directory.Exists(Application.dataPath + "/saves"))
                Directory.CreateDirectory(Application.dataPath + "/saves");
            if (File.Exists(Application.dataPath + "/saves/" + filename + ".sv"))
                File.Delete(Application.dataPath + "/saves/" + filename + ".sv");
                FileStream file = new FileStream(Application.dataPath + "/saves/" + filename + ".sv", FileMode.Create);// Сохранение в заданную директорию
                XmlSerializer serializer = new XmlSerializer(typeof(SaveParams)); // Создание сериализатора
                serializer.Serialize(file, saveparams);// Сериализация(путь, объекты)
                Debug.Log("Файл сохранен");
        }
        catch
        {
            Debug.Log("Файл не сохранен");
        }
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/" + filename + ".sv"))
        {
            FileStream file = new FileStream(Application.dataPath + "/saves/" + filename + ".sv", FileMode.Open);
            XmlSerializer formator = new XmlSerializer(typeof(SaveParams));
            try
            {
                SaveParams sp = (SaveParams)formator.Deserialize(file);// Загрузка из файла в персонажа
                Quaternion rotation = Quaternion.Euler(sp.rotx, sp.roty, sp.rotz);
                transform.position = new Vector3(sp.posx,sp.posy,sp.posz);
                transform.rotation = rotation;
                Item[] itemobject = Resources.FindObjectsOfTypeAll<Item>();

                foreach (var items in sp.objects)
                {
                    for(int i = 0; i < itemobject.Length;i++)
                    {
                        if (itemobject[i].id == items)
                        {
                            invent.list.Add(itemobject[i]);
                            Debug.Log(itemobject[i].names);
                            break;
                        }
                    }
                }
                Debug.Log("Игра успешно загружена");
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
