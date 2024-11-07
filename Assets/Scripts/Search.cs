using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour {
	private string[] _searchKeywords = {
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

	private void Start() {
		Button searchButtonComp = searchButton.GetComponent<Button>();
		searchButtonComp.onClick.AddListener(Greet);
	}

	private void Greet() {
		Debug.Log(greetingMessage);
	}
}
