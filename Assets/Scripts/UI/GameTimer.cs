using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
    
    public GameObject TimePanel;

    Image fillImg;
	float timeAmt = 180;
	[SerializeField] float time;

	void Start()
	{
		fillImg = this.GetComponent<Image> ();
		time = timeAmt;
	}
	void Update()
	{
		if (time > 0) {
			time -= Time.deltaTime;
			fillImg.fillAmount = time / timeAmt; // 9/ 10, 8/10.......0/10
		}
		TimeIsOver ();
	}

	void TimeIsOver()
	{
		if (time <= 0) 
		{
			Time.timeScale = 0;
			TimePanel.SetActive (true);

			Cursor.visible = true;
		}
	}
}
