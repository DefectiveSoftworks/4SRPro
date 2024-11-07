using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Greetings : MonoBehaviour {
	[SerializeField]
	private string greetingMessage = "Hello, world!";

	[SerializeField]
	private Button testButton;

	[SerializeField]
	private TMP_InputField testTextInput;

	private void Start() {
		Button testButtonComp = testButton.GetComponent<Button>();
		testButtonComp.onClick.AddListener(Greet);
	}

	private void Greet() {
		Debug.Log(greetingMessage);

		TMP_InputField textInputComp = testTextInput.GetComponent<TMP_InputField>();
		string text = textInputComp.text;
		Debug.Log("You have typed: " + text);
	}
}
