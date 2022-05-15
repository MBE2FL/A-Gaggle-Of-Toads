using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShowLeaderboard : MonoBehaviour
{

	[SerializeField] Leaderboard leaderboard;

	ScrollRect rect;
	CanvasGroup fader;
	void Awake()
	{
		rect = GetComponentInChildren<ScrollRect>();
		fader = GetComponentInChildren<CanvasGroup>();
	}

	public void Show() { fader.alpha = .5f; }
	public void Hide() { fader.alpha = 0; }

	void Update()
	{
		UpdateVisuals();
	}

	int lastCount = 0;
	void UpdateVisuals()
	{
		if(lastCount == leaderboard.Count) return;
		lastCount = leaderboard.Count;

		var list = rect.content.GetComponentsInChildren<TMP_Text>();
		for(int a = 0; a < list.Length; ++a)
			Destroy(list[a].gameObject);

		for(int a = 0; a < leaderboard.Count; ++a)
		{
			var thing = new GameObject();
			var obj = Instantiate(thing, rect.content.transform);
			var txt = obj.AddComponent<TextMeshProUGUI>();
			txt.text = $"{leaderboard.scores[a].Key} - {new TimeSpan(leaderboard.scores[a].Value).ToString(@"mm\:ss")}";

			{
				var rect = obj.GetComponent<RectTransform>();
				rect.pivot = new Vector2(0, 1);
				rect.anchorMin = new Vector2(0, 1);
				rect.anchorMax = new Vector2(0, 1);
				rect.anchoredPosition = new Vector2(0, a * -rect.sizeDelta.y);
				this.rect.content.sizeDelta = new Vector2(Mathf.Max(this.rect.content.sizeDelta.x, rect.sizeDelta.x),
				Mathf.Max(Mathf.Abs(rect.anchoredPosition.y) + rect.sizeDelta.y, this.rect.content.sizeDelta.y));

				txt.enableAutoSizing = true;
				txt.color = Color.white;
			}

			Destroy(thing);
		}


	}
}
