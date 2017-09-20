using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class HighScore
{
    public int Score { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public int ID { get; set; }

    public HighScore(int id, int score, string name, DateTime date)
    {
        this.Score = score;
        this.Name = name;
        this.ID = id;
        this.Date = date;
    }
}
