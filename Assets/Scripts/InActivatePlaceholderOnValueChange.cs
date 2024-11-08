using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InActivatePlaceholderOnValueChange : MonoBehaviour, IPointerClickHandler {
	private const float DebounceTime = 0.2f; // Time in seconds to debounce

	private Coroutine _debounceCoroutine;

	[SerializeField]
	private TMP_InputField placeholderField;

	[SerializeField]
	private TMP_Dropdown dropdownField;

	private void Start() {
		if (placeholderField == null) {
			placeholderField = GetComponent<TMP_InputField>();
		}

		if (dropdownField == null) {
			dropdownField = GetComponent<TMP_Dropdown>();
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		PointerEventData pseudoPointer = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute(dropdownField.gameObject, pseudoPointer, ExecuteEvents.pointerClickHandler);
	}

	public void AfterFetchJsonResource() {
		dropdownField.onValueChanged.AddListener(OnDropdownValueChanged);
	}

	private void OnDropdownValueChanged(int value) {
		if (_debounceCoroutine != null) {
			StopCoroutine(_debounceCoroutine);
		}

		_debounceCoroutine = StartCoroutine(DebounceCoroutine(value));
	}

	private IEnumerator DebounceCoroutine(int value) {
		yield return new WaitForSeconds(DebounceTime);
		InActivateSearchPlaceholder(value);
	}

	private void InActivateSearchPlaceholder(int valueSelected) {
		placeholderField.gameObject.SetActive(false);
		placeholderField.interactable = false;
		dropdownField.options.RemoveAt(0);
		dropdownField.onValueChanged.RemoveListener(OnDropdownValueChanged);
		dropdownField.value = (valueSelected - 1);
	}
}
