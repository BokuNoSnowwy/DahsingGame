using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Audio;

public class SettingsSaver : SaveableBehaviour
{
    private const string LOCAL_SOUND_VOLUME = "localSound";
    private const string LOCAL_MUSIC_VOLUME = "localMusic";

    [SerializeField] private AudioMixer audioMixer;

    private JsonData SerializeValue(object obj)
    {
        return JsonMapper.ToObject(JsonUtility.ToJson(obj));
    }

    private T DeserializeValue<T>(JsonData data)
    {
        return JsonUtility.FromJson<T>(data.ToJson());
    }

    public override JsonData SavedData
    {
        get
        {
            float soundValue;
            audioMixer.GetFloat("Sound", out soundValue);
            float musicValue;
            audioMixer.GetFloat("Music", out musicValue);

            var result = new JsonData();

            result[LOCAL_SOUND_VOLUME] = soundValue;

            result[LOCAL_MUSIC_VOLUME] = musicValue;

            return result;
        }
    }

    public override void LoadFromData(JsonData data)
    {
        

        if (data.ContainsKey(LOCAL_SOUND_VOLUME))
        {
            float soundVolume;
            soundVolume = (float)(double)data[LOCAL_SOUND_VOLUME];
            audioMixer.SetFloat("Sound", soundVolume);
        }

        if (data.ContainsKey(LOCAL_MUSIC_VOLUME))
        {
            float musicVolume;
            musicVolume = (float)(double)data[LOCAL_MUSIC_VOLUME];
            audioMixer.SetFloat("Music", musicVolume);
        }
    }
}
