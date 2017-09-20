using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour {

	public GameObject EndPanel;
	public GameObject EnterName;

	public Text Name;
	public bool CheckPanel;

	private BallController ChController;
	private string ConnectionString;

	public void Start () {
		
		Cursor.visible = false;

		ConnectionString = "URI=file:" + Application.dataPath + "/CubeWorld.sqlite";

        CreateTable();

		ChController = this.gameObject.GetComponent<BallController> ();
	}

	void Update(){
		
	}

    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("EndPoint")) 
		{
            print("Endpoint");
			EndPanel.SetActive(true);
			Time.timeScale = 0;
			CheckPanel = true;
			Cursor.visible = true;
		}
	}

	public void SaveScore()
	{
		EnterName.SetActive (true);
	}

	public void ExitButton()
	{
		EnterName.SetActive (false);
	}

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                //string sqlQuery = String.Format("insert into HighScores(Name,Score) values (\"{0}\", \"{1}\")", name, newScore);
                string sqlQuery = String.Format("CREATE TABLE if not exists HighScores (PlayerID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , Name TEXT NOT NULL , Score INTEGER NOT NULL , Date DATETIME NOT NULL  DEFAULT CURRENT_DATE)");
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertScore(string name, int newScore)
	{
		 using (IDbConnection dbConnection = new SqliteConnection (ConnectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
				
				string sqlQuery = String.Format ("insert into HighScores(Name,Score) values (\"{0}\", \"{1}\")", name, newScore);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	public void EnterScore()
	{
		InsertScore(Name.text,ChController.Count);
		print (Name.text + ": " + ChController.Count);
		Name.text = string.Empty;
	}
}
