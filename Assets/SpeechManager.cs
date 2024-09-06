using System;
using UnityEngine;
using UnityEngine.UI;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using TMPro;

public class SpeechManager : MonoBehaviour
{
    private SpeechRecognitionEngine recognizer;
    private SpeechSynthesizer synthesizer;
    public TMP_Text recognizedText; // UI Text to display recognized speech
    public Button speakButton; // Button to trigger text-to-speech

    private string recognizedString = "";

    void Start()
    {
        // Initialize recognizer for Speech to Text
        recognizer = new SpeechRecognitionEngine();
        recognizer.SetInputToDefaultAudioDevice();
        recognizer.LoadGrammar(new DictationGrammar());

        recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        recognizer.RecognizeAsync(RecognizeMode.Multiple);

        // Initialize synthesizer for Text to Speech
        synthesizer = new SpeechSynthesizer();

        // Add a listener to the speak button
        speakButton.onClick.AddListener(SpeakText);
    }

    // Handle recognized speech
    private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        recognizedString = e.Result.Text;
        Debug.Log("Recognized Speech: " + recognizedString);
        recognizedText.text = recognizedString; // Display recognized text
    }

    // Trigger Text to Speech when button is clicked
    public void SpeakText()
    {
        if (!string.IsNullOrEmpty(recognizedString))
        {
            synthesizer.SpeakAsync(recognizedString); // Speak the recognized text
            Debug.Log("Speaking: " + recognizedString);
        }
    }

    private void OnApplicationQuit()
    {
        // Clean up resources when the application quits
        if (recognizer != null)
        {
            recognizer.Dispose();
        }
        if (synthesizer != null)
        {
            synthesizer.Dispose();
        }
    }
}
