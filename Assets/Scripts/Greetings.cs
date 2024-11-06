using UnityEngine;
using UnityEngine.UI;

public class Greetings : MonoBehaviour {
	[SerializeField]
	private string greetingMessage = "Hello, world!";

	[SerializeField]
	private Button testButton;

	private void Start() {
		Button testButtonComp = testButton.GetComponent<Button>();
		testButtonComp.onClick.AddListener(Greet);
	}

	private void Greet() {
		Debug.Log(greetingMessage);
	}
}
