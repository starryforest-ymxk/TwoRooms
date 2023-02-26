using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class MusicMgr : BaseMgr<MusicMgr>
{
    [Header("音量")]
    private float BGMVolume = 1f;
    private float SoundVolume = 1f;
    [Header("音效单位")]
    private AudioSource BGMSource = null;                           //BGM
    private GameObject SoundObj = null;                             //通用音效物体
    private List<AudioSource> soundList = new List<AudioSource>();  //通用音效组件列表    

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);  //注册update
    }
    public void MyUpdate()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)  //及时移除不再播放的音效
        {
            if (soundList == null || soundList[i] == null) continue;
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }        
    }
    #region BGM方法
    public void PlayBGM(string name)
    {
        if (BGMSource == null)
        {
            GameObject BGMObj = new GameObject("BGMObject");
            GameObject.DontDestroyOnLoad(BGMObj);
            BGMSource = BGMObj.AddComponent<AudioSource>();
        }
        ResMgr.GetInstance().AsyncLoad<AudioClip>("Music/BGM/" + name, (clip) =>
        {
            BGMSource.clip = clip;      //进行音频的赋值
            BGMSource.loop = true;      //确保循环
            BGMSource.volume = BGMVolume;    //设定音量
            BGMSource.Play();     //开始播放
        });
    }
    public void PauseBGM()
    {
        if (BGMSource == null)
        { return; }
        BGMSource.Pause();
    }
    public void StopBGM()
    {
        if (BGMSource == null)
        { return; }
        BGMSource.Stop();
    }
    public void ChangeBGMValue(float volume)
    {
        BGMVolume = volume;     //这保证了在没有播放BGM的时候也可以调整音量
        if (BGMSource == null)
        { return; }
        BGMSource.volume = BGMVolume;   //是否需要某种放入update的方法保证BGM一旦存在就保证volume和BGMvolume相等？
    }
    #endregion
    #region 音效方法
    /// <summary>
    /// 播放通用音效
    /// </summary>
    /// <param name="name">音频在Resources/Music/Sounds/路径下的文件名或者路径</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="callback">回调函数</param>
    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback = null)
    {
        if (SoundObj == null)
        {
            SoundObj = new GameObject();
            SoundObj.name = "Sounds";
        }
        AudioSource source = SoundObj.AddComponent<AudioSource>();  //新的音源被挂载在这个公用物品上   并没有被赋予特殊的名字
        ResMgr.GetInstance().AsyncLoad<AudioClip>("Music/Sounds/" + name, (clip) =>
        {
            source.clip = clip;
            source.volume = SoundVolume;
            source.loop = isLoop;
            source.Play();
            soundList.Add(source);      //加入到一个集合之中，方便后续的音量和停止的管理
            if (callback != null)
            { callback(source); }
        });
    } 
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// 改变音效总音量
    /// </summary>
    /// <param name="volume">音量</param>
    public void ChangeSoundVolume(float volume)
    {
        SoundVolume = volume;
        foreach (var item in soundList)
        {
            item.volume = SoundVolume;
        }
    }
    #endregion

}
