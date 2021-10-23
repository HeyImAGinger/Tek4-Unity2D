using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Class.Utils;

public class UI_input : MonoBehaviour
{
    private static UI_input instance;
    private Button_UI button_ok;
    private TextMeshProUGUI textmesh;
    private TMP_InputField inputfield;

    private void Awake() {
        //get components//
        instance = this;
        button_ok = transform.Find("okButton").GetComponent<Button_UI>();
        textmesh = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        inputfield = transform.Find("InputField").GetComponent<TMP_InputField>();
    }

    private void Update() {
        //check input Enter input//
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
            button_ok.ClickFunc();
        }
    }

    //Display and manage InputField//
    private void Show(string title, string input, string characters, int nb, Action<string> button) {
        gameObject.SetActive(true);

        textmesh.text = title;

        inputfield.characterLimit = nb;
        inputfield.onValidateInput = (string text, int i, char str) => {
            return checkField(characters, str);
        };

        inputfield.text = input;
        button_ok.ClickFunc = () => {
            Hide();
            button(inputfield.text);
        };
    }

    //hide gameobject//
    private void Hide() {
        gameObject.SetActive(false);
    }

    //check if the input is good//
    private char checkField(string characters, char str) {
        return ((characters.IndexOf(str) != -1) ? str : '\0');
    }

    //instance show//
    public static void StaticShow(string title, string input, string characters, int nb, Action<string> button) {
        instance.Show(title, input, characters, nb, button);
    }
}
