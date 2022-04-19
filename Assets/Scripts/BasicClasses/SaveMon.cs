using System.Collections.Generic;
[System.Serializable]

public class SaveMon 
{
    public string monName;
    public string picturePath;
    public int attack;
    public int defense;
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int intelligence;
    public string type1;
    public string type2;
    public List<Move> moves = new List<Move>();
}
