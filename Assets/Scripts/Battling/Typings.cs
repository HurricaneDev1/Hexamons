using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typings : MonoBehaviour
{
    private string Id = "Idealism";
    private string Rea = "Realism";
    private string Uto = "Utopianism";
    private string Com = "Communism";
    private string Cap = "Capitalism";
    private string Soc = "Socialism";
    private string Util = "Utilitarianism";
    private string Ego = "Egoism";
    private string Inte = "Intellectualism";
    private string Pac = "Pacifism";
    private string Nep = "Nepotism";

    private Move mo;
    private int damage;
    
    private List<string> noDamage = new List<string>();
    private List<string> halfDamage = new List<string>();
    private List<string> moreDamage = new List<string>();

    //Checks through all the types to see what a moves damage will be after type changes
    public int TypeCheck(SaveMon mon, int attack, Move mov){
        mo = mov;
        damage = attack;
        //Checks both types for type interactions, if both exist. Otherwise it will only do one
        TypeTables(mon.type1);
        if(mon.type2 != ""){
            TypeTables(mon.type2);
        }
        return damage;
    }

    void TypeTables(string type){
        switch(type){
            case "Idealism":
                noDamage = new List<string>(){Rea};
                halfDamage = new List<string>(){Id,Ego};
                moreDamage = new List<string>(){Uto,Com,Cap};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;   
            case "Realism":
                noDamage = new List<string>(){Id,Uto};
                halfDamage = new List<string>(){Com,Util,Ego,Pac,Nep};
                moreDamage = new List<string>(){Cap,Inte};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Utopianism":
                halfDamage = new List<string>(){Ego};
                moreDamage = new List<string>(){Rea,Uto,Com,Cap,Soc,Util,Inte,Pac,Nep};
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Communism":
                noDamage = new List<string>(){Uto};
                halfDamage = new List<string>(){Com,Ego};
                moreDamage = new List<string>(){Id,Rea,Cap,Soc,Util,Inte,Nep};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Capitalism":
                noDamage = new List<string>(){Uto};
                halfDamage = new List<string>(){Rea,Util,Ego,Inte};
                moreDamage = new List<string>(){Cap,Soc,Nep};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Socialism":
                noDamage = new List<string>(){Uto};
                halfDamage = new List<string>(){Ego};
                moreDamage = new List<string>(){Com};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Utilitarianism":
                noDamage = new List<string>(){Uto};
                halfDamage = new List<string>(){Id,Soc,Ego,Inte,Pac,Nep};
                moreDamage = new List<string>(){Com,Cap};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Egoism":
                noDamage = new List<string>(){Ego,Uto};
                halfDamage = new List<string>(){Id,Rea,Com,Soc,Util,Inte,Pac,Nep};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                break;
            case "Intellectualism":
                noDamage = new List<string>(){Id,Uto};
                halfDamage = new List<string>(){Com,Util,Ego};
                moreDamage = new List<string>(){Rea,Cap,Inte};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Welfarism":
                break;
            case "Pacifism":
                noDamage = new List<string>(){Pac};
                halfDamage = new List<string>(){Id,Com,Cap,Soc,Ego};
                moreDamage = new List<string>(){Inte};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
            case "Nepotism":
                noDamage = new List<string>(){Uto};
                halfDamage = new List<string>(){Com,Cap,Ego};
                moreDamage = new List<string>(){Rea,Util,Nep};
                NullInteraction(noDamage);
                ResistInteraction(halfDamage);
                EffectiveInteraction(moreDamage);
                break;
        }
    }

    //Increases damage if move is in list
    void EffectiveInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                damage *= 2;
            }
        }
    }

    //Decreases damage if move is in list
    void ResistInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                int newDamage = (int)(damage * 0.5f);
                damage = newDamage;
            }
        }
    }

    //Prevents damage if move is in list
    void NullInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                damage *= 0;
            }
        }
    }
}
