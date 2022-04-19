using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
    private string Wel = "Welfarism";
    private string Pac = "Pacifism";
    private string Nep = "Nepotism";

    public Move mo;
    public int damage;
    
    private List<string> noDamage = new List<string>();
    private List<string> halfDamage = new List<string>();
    private List<string> moreDamage = new List<string>();


    public int TypeCheck(SaveMon mon, int attack){
        damage = attack;
        switch(mon.type1){
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
        }

        return damage;
    }

    void EffectiveInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                damage *= 2;
            }
        }
    }

    void ResistInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                damage *= 0;
            }
        }
    }

    void NullInteraction(List<string> list){
        foreach(string type in list){
            if(type == mo.Type){
                int newDamage = (int)(damage * 0.5f);
                damage = newDamage;
            }
        }
    }
}
