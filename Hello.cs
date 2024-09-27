using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello : MonoBehaviour {

	float limValue;


	// Use this for initialization
	void Start () {
	secCounter = 0;

        y0 = mouth0.localPosition.y;
        y1 = mouth1.localPosition.y;

        freqData = new float[nSamples];
        //source_clip = SetFace.voiceOver;
        GetComponent<AudioSource> ().clip = Rec_voice.instance.voiceOver;
        GetComponent<AudioSource> ().Play ();
        video_Length = Mathf.CeilToInt (source_clip.length);

        //Debug.Log (y0);
        //  DebugConsole.Log (y0.ToString ());


        //  Debug.Log (Application.persistentDataPath);

        StartCoroutine (Timer ());
        StartCoroutine (recordScreen ());

    }

      IEnumerator Timer ()
    {
        while (secCounter < video_Length) {
            yield return new WaitForSeconds (1f);
            secCounter += 1;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		float band_vol = BandVol (freqLow, freqHigh);
        	float val = MovingAverage (band_vol) * volume;
        	//limValue = val;//Mathf.Clamp (val, 0, 0.1f);
        	//limValue = Mathf.Clamp (val, 0, 10f);
        	//check new lip movement abd set clamp val
        	limValue = Mathf.Clamp (val, 0, 25f);
        	//Debug.Log (y0 - limValue);
        	if (Input.GetKeyDown (KeyCode.Escape)) {
        	    Application.Quit ();
        	}
        	mouth0.position = new Vector3 (mouth0.position.x, y0 - MovingAverage (band_vol) * volume, mouth0.position.z);
        	mouth1.position = new Vector3 (mouth1.position.x, y1 + MovingAverage (band_vol) * volume * 0.3f, mouth1.position.z);

		
	}
}
