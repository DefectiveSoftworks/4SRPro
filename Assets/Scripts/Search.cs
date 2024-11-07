using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour {
	private readonly string[] _searchKeywords = {
		"---------------------------------------",
		"Roofing",
		"Soffit/Fascia",
		"Gutters",
		"Siding",
		"Drywall",
		"Painting",
		"Flooring",
		"Demolition",
		"Local Moving",
		"Pressure Washing",
	};

	[SerializeField]
	private string greetingMessage = "Hello, world!";

	[SerializeField]
	private Button searchButton;

	[SerializeField]
	private TMP_Dropdown searchDropdown;

	private List<TMP_Dropdown.OptionData> GetSearchKeywords() {
		return _searchKeywords.Select(searchKeyword => new TMP_Dropdown.OptionData(searchKeyword)).ToList();
	}

	private void Start() {
		List<TMP_Dropdown.OptionData> searchOptionData = GetSearchKeywords();
		TMP_Dropdown searchDropdownComp = searchDropdown.GetComponent<TMP_Dropdown>();
		searchDropdownComp.options.Clear();
		searchDropdownComp.options.AddRange(searchOptionData);
		searchDropdownComp.value = -1;

		Button searchButtonComp = searchButton.GetComponent<Button>();
		searchButtonComp.onClick.AddListener(Greet);
	}

	private void Greet() {
		Debug.Log(greetingMessage);
	}
}
