using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Search : MonoBehaviour {
	[Serializable]
	private class Foo {
		public string[] services;
	}
	
	private const string ServicesEndpoint = "http://whiskeybox.local/services";

	private readonly List<string> _searchKeywords = new() {
		"---------------------------------------",
	};

	[SerializeField]
	private Button searchButton;

	[SerializeField]
	private TMP_Dropdown searchDropdown;

	[SerializeField]
	private TMP_Text responseMessage;

	private IEnumerator FetchServiceKeywords(Action callback) {
		UnityWebRequest http = UnityWebRequest.Get(ServicesEndpoint);
		yield return http.SendWebRequest();

		if (http.result != UnityWebRequest.Result.Success) {
			Debug.LogError(http.error);
		} else {
			string responseText = http.downloadHandler.text;
			Debug.Log("WhiskeyCMS Service has responded successfully with: " + responseText);
			Foo responseJson = JsonUtility.FromJson<Foo>(responseText);
			_searchKeywords.AddRange(responseJson.services);
			callback?.Invoke();
		}
	}

	private void PopulateSearchKeywords() {
		List<TMP_Dropdown.OptionData> searchOptionData = _searchKeywords.Select(searchKeyword => new TMP_Dropdown.OptionData(searchKeyword)).ToList();
		TMP_Dropdown searchDropdownComp = searchDropdown.GetComponent<TMP_Dropdown>();
		searchDropdownComp.options.Clear();
		searchDropdownComp.options.AddRange(searchOptionData);
		searchDropdownComp.value = -1;
	}

	private void Start() {
		StartCoroutine(FetchServiceKeywords(PopulateSearchKeywords)); // Run in background async.
	}
}
