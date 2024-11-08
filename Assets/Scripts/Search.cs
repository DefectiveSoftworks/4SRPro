using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Search : MonoBehaviour {
	[Serializable]
	private class Foo {
		public string[] services;
	}
	
	private const string ServicesEndpoint = "http://whiskeybox.local/services";

	[SerializeField]
	private Button searchButton;

	[SerializeField]
	private TMP_Dropdown searchDropdown;

	[SerializeField]
	private TMP_InputField searchPlaceholder;

	private IEnumerator FetchServiceKeywords(Action<List<string>> callback) {
		UnityWebRequest http = UnityWebRequest.Get(ServicesEndpoint);
		yield return http.SendWebRequest();

		if (http.result != UnityWebRequest.Result.Success) {
			Debug.LogError(http.error);
		} else {
			string responseText = http.downloadHandler.text;
			Debug.Log("WhiskeyCMS Service has responded successfully with: " + responseText);
			Foo responseJson = JsonUtility.FromJson<Foo>(responseText);
			List<string> searchKeywords = responseJson.services.ToList();
			searchKeywords.Insert(0, "---------------------------------------");
			callback?.Invoke(searchKeywords);
		}
	}

	private void PopulateSearchKeywords(List<string> searchKeywords) {
		List<TMP_Dropdown.OptionData> searchOptionData = 
			searchKeywords.Select(searchKeyword => new TMP_Dropdown.OptionData(searchKeyword)).ToList();
		TMP_Dropdown searchDropdownComp = searchDropdown.GetComponent<TMP_Dropdown>();
		searchDropdownComp.options.Clear();
		searchDropdownComp.options.AddRange(searchOptionData);
		searchDropdownComp.value = -1;
		searchPlaceholder.GetComponent<InActivatePlaceholderOnValueChange>().AfterFetchJsonResource();
	}

	private void Start() {
		StartCoroutine(FetchServiceKeywords(PopulateSearchKeywords)); // Run in background async.
	}
}
