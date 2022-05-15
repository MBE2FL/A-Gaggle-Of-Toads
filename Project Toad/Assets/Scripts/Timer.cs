using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	public TMP_Text text;

	TimeSpan tim;
	DateTime start, lastTime;
	bool endb = true;

	private void OnEnable()
	{
		Begin();
	}


	public TimeSpan timeSpan
	{
		get
		{
			if(endb)
				return lastTime - start;
			else
				return (lastTime = DateTime.Now) - start;
		}
	}
	DateTime stopedTime;

	private void Update() => text.text = text.text.Substring(0, text.text.IndexOf(":") + 1) + " " + timeSpan.ToString(@"mm\:ss");

	/// <summary>
	/// timer counts from when this function is called
	/// </summary>
	public void Begin()
	{
		start = DateTime.Now;
		endb = false;
	}


	/// <summary>
	/// timer will stop counting when this is called
	/// </summary>
	public void End()
	{
		endb = true;
		stopedTime = DateTime.Now;
	}


	/// <summary>
	/// will resume from wherever the timer left off
	/// </summary>
	public void Resume()
	{
		endb = false;

		if(stopedTime != null)
			RemoveTime(DateTime.Now - stopedTime);
	}

	public void AddTime(int hours, int minutes, int sec)
	{
		start -= new TimeSpan(hours, minutes, sec);
	}
	public void RemoveTime(int hours, int minutes, int sec)
	{
		start += new TimeSpan(hours, minutes, sec);
	}
	public void RemoveTime(TimeSpan time)
	{
		if(time != null)
			start += time;
	}
}
