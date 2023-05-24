using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public interface ISaveable
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);
}

public static class SavingService
{
    private const string ACTIVE_SCENE_KEY = "activeScene";
    private const string SCENES_KEY = "scenes";
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    private const string ENCRYPTION_KEY = "24122001";

    public static void SaveGame (string fileName)
    {
        var result = new JsonData();

        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        if(allSaveableObjects.Count() > 0)
        {
            var savedObjects = new JsonData();

            foreach(var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;

                if (data.IsObject)
                {
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    var behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behaviour, "{0}'s save data is not a dictionnary. The object was not saved.", behaviour.name);
                }
            }

            result[OBJECTS_KEY] = savedObjects;
        }
        else
        {
            Debug.LogWarningFormat("The scene did not include any saveable objects.");
        }

        var openScenes = new JsonData();

        var sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }

        result[SCENES_KEY] = openScenes;

        result[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;

        var outputPath = Path.Combine(Application.persistentDataPath, fileName);

        var writer = new JsonWriter();
        writer.PrettyPrint = true;

        result.ToJson(writer);

        //save without encryption
        File.WriteAllText(outputPath, writer.ToString());

        //save with encrytpion
        //File.WriteAllText(outputPath, EncryptionUtils.Encrypt(writer.ToString(), ENCRYPTION_KEY));

        Debug.LogFormat("Wrote saved game at {0}", outputPath);

        result = null;
        System.GC.Collect();
    }

    static UnityEngine.Events.UnityAction<Scene, LoadSceneMode> LoadObjectsAfterSceneLoad;

    public static bool LoadGame(string fileName)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);

        if(File.Exists(dataPath) == false)
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return false;
        }

        var text = File.ReadAllText(dataPath);

        //load without decryption
        var data = JsonMapper.ToObject(text);

        //load with decrytpion
        //var decryptedText = EncryptionUtils.Decrypt(text, ENCRYPTION_KEY);
        //var data = JsonMapper.ToObject(decryptedText);

        if (data == null || data.IsObject == false)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSon object", dataPath);
            return false;
        }

        if (!data.ContainsKey("scenes"))
        {
            Debug.LogWarningFormat("Data at {0} does not contain any scenes; not loading any!", dataPath);
            return false;
        }

        var scenes = data[SCENES_KEY];

        int sceneCount = scenes.Count;

        if(sceneCount == 0)
        {
            Debug.LogWarningFormat("Data at {0} doesn't specify any scenes to load.", dataPath);
            return false;
        }

        for (int i = 0; i < sceneCount; i++)
        {
            var scene = (string)scenes[i];

            if(i == 0)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

        if (data.ContainsKey(ACTIVE_SCENE_KEY))
        {
            var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
            var activeScene = SceneManager.GetSceneByName(activeSceneName);

            if(activeScene.IsValid() == false)
            {
                Debug.LogErrorFormat("Data at {0} specifies an active scene that doesn't exist. Stopping loading here.", dataPath);
                return false;
            }

            SceneManager.SetActiveScene(activeScene);
        }
        else
        {
            Debug.LogWarningFormat("Data at {0} does not precise an active scene.", dataPath);
        }

        if (data.ContainsKey(OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];

            //LoadObjectsAfterSceneLoad = (scene, LoadSceneMode) =>
            //{
                var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);

                var objectsCount = objects.Count;

                for (int i = 0; i < objectsCount; i++)
                {
                    var objectData = objects[i];

                    var saveID = (string)objectData[SAVEID_KEY];

                    if (allLoadableObjects.ContainsKey(saveID))
                    {
                        var loadableObject = allLoadableObjects[saveID];

                        loadableObject.LoadFromData(objectData);
                    }
                }

                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;

                LoadObjectsAfterSceneLoad = null;

                System.GC.Collect();
            //};

            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }

        return true;
    }
}
