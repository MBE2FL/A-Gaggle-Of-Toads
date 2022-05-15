using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Leaderboard")]
public class Leaderboard : ScriptableObject
{
	public List<KeyValuePair<string, long>> scores { get; } = new List<KeyValuePair<string, long>>();

	public void AddScore(string name, long score)
	{
		scores.Add(new KeyValuePair<string, long>(name, score));
		scores.Sort((i, k) => i.Value.CompareTo(k.Value));
	}

	public int Count { get => scores.Count; }
}
