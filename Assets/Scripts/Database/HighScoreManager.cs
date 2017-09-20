using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

    private string connectionString;

    private List<HighScore> highScores = new List<HighScore>();

    public GameObject scorePrefab;

    public Transform scoreParent;

    public int topRanks;

    public int saveScores;

    public InputField enterName;

    public GameObject nameDialog;

	// Use this for initialization
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/CubeWorld.sqlite";

       CreateTable();

       DeleteExtraScore();

       // InsertScore("Test", 350);

        ShowScores();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            nameDialog.SetActive(!nameDialog.activeSelf);
        }
	}

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
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

    public void EnterName()
    {
        if (enterName.text != string.Empty)
        {
            int score = UnityEngine.Random.Range(1, 500);
            InsertScore(enterName.text, score);
            enterName.text = string.Empty;

            ShowScores();
        }
    }

    private void InsertScore(string name, int newScore)
    {
        GetScores();
        int hsCount = highScores.Count;

        if (highScores.Count > 0)
        {
            HighScore lowestScore = highScores[highScores.Count - 1];
            if (lowestScore!= null && saveScores > 0 && highScores.Count >= saveScores && newScore > lowestScore.Score)
            {
                DeleteScore(lowestScore.ID);
                hsCount--;
            }
        }
        if (hsCount < saveScores)
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("insert into HighScores(Name,Score) values (\"{0}\", \"{1}\")", name, newScore);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
    }

    void GetScores()
    {
        highScores.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM ( SELECT* FROM HighScores ORDER BY PlayerID) sub ORDER BY Score DESC";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScores.Add(new HighScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetDateTime(3)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void DeleteScore(int id)
    {
        // DELETE FROM HighScores Where PlayerID = '3'
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM HighScores Where PlayerID = \"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void ShowScores()
    {
        GetScores();

        foreach(GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }

        for (int i = 0; i < topRanks; i++)
        {
            if (i <= highScores.Count - 1)
            {
                GameObject tmpObjec = Instantiate(scorePrefab);

                HighScore tmpScore = highScores[i];

                tmpObjec.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

                tmpObjec.transform.SetParent(scoreParent);

                tmpObjec.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void DeleteExtraScore()
    {
        GetScores();

        if(saveScores <= highScores.Count)
        {
            int deleteCount = highScores.Count - saveScores;
            highScores.Reverse();

            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    for (int i = 0; i < deleteCount; i++)
                    {
                        string sqlQuery = String.Format("DELETE FROM HighScores Where PlayerID = \"{0}\"", highScores[i].ID);

                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();
                    }
                    dbConnection.Close();
                }
            }
        }
    }
}
