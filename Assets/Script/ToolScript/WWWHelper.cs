using UnityEngine;
using System.Collections;


public class WWWHelper : MonoBehaviour 
{
	private static WWWHelper instance = null;

	private WWW m_www;
	private GameObject m_CallBackTarget = null;
	private bool m_bIsBeginRequest = false;
	private bool m_bIsDone = true;

	public bool IsBeginRequest
	{
		get { return m_bIsBeginRequest; }
		set { m_bIsBeginRequest = value; }
	}

	public bool IsDone
	{
		get { return m_bIsDone; }
		set { m_bIsDone = value; }
	}

	public static WWWHelper Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	public void GET(string url, GameObject callBackTarget)
	{
		if (m_bIsDone)
		{
			m_CallBackTarget = callBackTarget;
			this.m_www = new WWW(url);
			m_bIsBeginRequest = true;
			m_bIsDone = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_bIsBeginRequest)
		{
			if (this.m_www.isDone)
			{
				if (null != m_CallBackTarget)
					m_CallBackTarget.SendMessage("RequestDone",this.m_www.text);
				m_bIsDone = true;
				m_bIsBeginRequest = false;
			}
		}
	}
}