using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Search : MonoBehaviour {
	private const string ServicesEndpoint = "http://whiskeybox.local/foo";

	private readonly string[] _searchKeywords = {
		"---------------------------------------",
	};

	[SerializeField]
	private Button searchButton;

	[SerializeField]
	private TMP_Dropdown searchDropdown;

	[SerializeField]
	private TMP_Text responseMessage;

	private IEnumerator FetchServiceKeywords() {
		UnityWebRequest http = UnityWebRequest.Get(ServicesEndpoint);
		yield return http.SendWebRequest();

		if (http.result != UnityWebRequest.Result.Success) {
			Debug.LogError(http.error);
		} else {
			string responseText = http.downloadHandler.text;
			Debug.Log("WhiskeyCMS Service has responded successfully with: " + responseText);
			responseMessage.text = responseText;
		}
	}

	private List<TMP_Dropdown.OptionData> GetSearchKeywords() {
		return _searchKeywords.Select(searchKeyword => new TMP_Dropdown.OptionData(searchKeyword)).ToList();
	}

	private void Start() {
		List<TMP_Dropdown.OptionData> searchOptionData = GetSearchKeywords();
		TMP_Dropdown searchDropdownComp = searchDropdown.GetComponent<TMP_Dropdown>();
		searchDropdownComp.options.Clear();
		searchDropdownComp.options.AddRange(searchOptionData);
		searchDropdownComp.value = -1;
		
		searchButton.GetComponent<Button>().onClick.AddListener(FetchDataFromWhiskeyCMS);
	}

	private void FetchDataFromWhiskeyCMS() {
		StartCoroutine(FetchServiceKeywords()); // Run in background async.
	}
}
