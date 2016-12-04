using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

	// 默认屏幕大小
	float width = 1136.0f;
	float height = 640.0f;


	private int channelCount;
	public  IList<AudioSource> _channels;
	public AudioClip[] clips;//声音对象数组


	void Awake() {

		// 屏幕适配
		float orthographicSize = Camera.main.orthographicSize;
		orthographicSize *= (Screen.height / (float)Screen.width) / (height / width);
		Camera.main.orthographicSize = orthographicSize;



		channelCount = 10;
		_channels = new List<AudioSource>();
		for (int i = 0; i < channelCount; i++)
		{
			// 通过代码添加多个AudioSource组件，不同声音对应不同的频道，这样就可以同时播放多个声音
			_channels.Add(this.gameObject.AddComponent<AudioSource>());
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public void PlaySound (int id,bool loop, float vol)
	{
		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			return;
		}

		int c = FindChannel();
		if (c == -1)
		{
			return;
		}
		_channels[c].clip= clips [id];//播放的声音对象
		_channels[c].loop = loop;//是否循环
		_channels[c].volume = vol;//音量大笑傲
		_channels[c].Play();//播放
	}
	// 搜索一个空的频道来设置声音
	public int FindChannel ()
	{
		for (int i =0; i<channelCount;i++)
		{
			if (_channels[i] != null)
			{
				if (!_channels[i].isPlaying) return i;//不在播放
			}
		}
		return -1;
	}
	//暂停所有声音的播放
	public void StopSound()
	{
		foreach (AudioSource source in _channels)
		{
			source.mute = true;
		}

	}
	// 取消暂停
	public void ResumeSound()
	{
		foreach (AudioSource source in _channels)
		{
			if (source.isPlaying)
			{
				source.mute = false;
			}
		}
	}
	// 降低音量
	public void ChangePitch()
	{
		foreach (AudioSource source in _channels)
		{
			if (source.isPlaying)
			{
				source.pitch = 0.70f;
			}
		}
	}
	// 升高音量
	public void ResumePitch()
	{
		foreach (AudioSource source in _channels)
		{
			source.pitch = 1f;
		}
	}
}
