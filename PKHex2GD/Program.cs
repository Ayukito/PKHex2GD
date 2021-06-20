using System;
using PKHeX.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace PKHex2GD
{
    class MainClass
    {
        

        public static void Main(string[] args)
        {

            var fileName = @"/Users/lynn/Downloads/test.gd";
            var SpeciesList = Util.GetSpeciesList("en");
            var AbilityList = Util.GetAbilitiesList("en");
            var Moveist = Util.GetMovesList("en");
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("extends Object");
                sw.WriteLine("class_name Pokedex");
                sw.WriteLine("");
                sw.WriteLine("var Dex: Array = [");
            }

            //var pk2 = new PK7
            //{
            //    Species = (int)Species.Chimchar,
            //};
            //var jPKX2 = (JObject)JToken.FromObject(pk2.PersonalInfo);
            //Console.WriteLine(jPKX2.ToString());
            for (int i = 0; i <= 649; i++)
            {
                //Console.WriteLine("Parsing Pokemon " + i + " out of 649");
                var pk = new PK7
                {
                    Species = i,
                    Form = 0,
                };
                //Console.WriteLine(pk.PersonalInfo.Stats[0]);
                //Console.WriteLine(pk.PersonalInfo.Stats[1]);
                //Console.WriteLine(pk.PersonalInfo.Stats[2]);
                //Console.WriteLine(pk.PersonalInfo.Stats[4]);
                //Console.WriteLine(pk.PersonalInfo.Stats[5]);
                //Console.WriteLine(pk.PersonalInfo.Stats[3]);

                var jPKX = (JObject)JToken.FromObject(pk.PersonalInfo);
                //Console.WriteLine(jPKX.ToString());
                //[exp_table[3], exp_table[1], exp_table[6], exp_table[4], exp_table[2], exp_table[5]]
                switch (pk.PersonalInfo.EXPGrowth)
                {
                    case 0:
                        pk.PersonalInfo.EXPGrowth = 3;
                        break;
                    case 1:
                        pk.PersonalInfo.EXPGrowth = 1;
                        break;
                    case 2:
                        pk.PersonalInfo.EXPGrowth = 6;
                        break;
                    case 3:
                        pk.PersonalInfo.EXPGrowth = 4;
                        break;
                    case 4:
                        pk.PersonalInfo.EXPGrowth = 2;
                        break;
                    case 5:
                        pk.PersonalInfo.EXPGrowth = 5;
                        break;
                }
                // Because PKHex doesn't use the standard order :|
                //
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("  {");
                    sw.WriteLine("      Species = Species." + SpeciesList[i].ToUpper().Replace("♀", "F").Replace("♂", "M").Replace("’", "").Replace(" ", "").Replace(".", "").Replace("-", "") + ", ");
                    sw.WriteLine("      Type1 = " + pk.PersonalInfo.Type1 + ",");
                    sw.WriteLine("      Type2 = " + pk.PersonalInfo.Type2 + ",");
                    sw.WriteLine("      Stats = [");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[0] + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[1] + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[2] + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[3] + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[4] + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.Stats[5] + ",");
                    sw.WriteLine("      ],");
                    sw.WriteLine("      EVGain = [");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_HP + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_ATK + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_DEF + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_SPE + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_SPA + ",");
                    sw.WriteLine("          " + pk.PersonalInfo.EV_SPD + ",");
                    sw.WriteLine("      ],");
                    sw.WriteLine("      CatchRate = " + pk.PersonalInfo.CatchRate + ",");
                    sw.WriteLine("      EvoStage = " + pk.PersonalInfo.EvoStage + ",");
                    sw.WriteLine("      Gender = " + pk.PersonalInfo.Gender + ",");
                    sw.WriteLine("      Genderless = " + pk.PersonalInfo.Genderless.ToString().ToLower() + ",");
                    sw.WriteLine("      FormCount = " + pk.PersonalInfo.FormCount + ",");
                    sw.WriteLine("      HatchCycles = " + pk.PersonalInfo.HatchCycles + ",");
                    sw.WriteLine("      BaseFriendship = " + pk.PersonalInfo.BaseFriendship + ",");
                    sw.WriteLine("      EXPGrowth = " + pk.PersonalInfo.EXPGrowth + ",");
                    sw.WriteLine("      EggGroup1 = " + pk.PersonalInfo.EggGroup1 + ",");
                    sw.WriteLine("      EggGroup2 = " + pk.PersonalInfo.EggGroup2 + ",");
                    sw.WriteLine("      Abilities = [");
                    sw.WriteLine("          Ability." + AbilityList[pk.PersonalInfo.Abilities[0]].ToUpper().Replace("—", "NONE").Replace(" ", "").Replace("'", "") + ",");
                    sw.WriteLine("          Ability." + AbilityList[pk.PersonalInfo.Abilities[1]].ToUpper().Replace("—", "NONE").Replace(" ", "").Replace("'", "") + ",");
                    sw.WriteLine("          Ability." + AbilityList[pk.PersonalInfo.Abilities[2]].ToUpper().Replace("—", "NONE").Replace(" ", "").Replace("'", "") + ",");
                    sw.WriteLine("      ],");
                    sw.WriteLine("      BaseEXP = " + pk.PersonalInfo.BaseEXP + ",");
                    sw.WriteLine("      Height = " + pk.PersonalInfo.Height + ",");
                    sw.WriteLine("      Weight = " + pk.PersonalInfo.Weight + ",");
                    sw.WriteLine("      TMs = [");
                    for (int tm = 0; tm <= 104; tm++)
                    {
                        if (pk.PersonalInfo.TMHM[tm] == true)
                        {
                            sw.WriteLine("          TMs["+tm+"],");
                        }
                    }
                    sw.WriteLine("      ],");
                    sw.WriteLine("  },");
                }
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("]");
            }
        }
    }
}
// TODO: Add other forms that change stats and types as separate dex entries. Megas, Giratina Oirigin form, Kyurem forms, Deoxys, Castform, etc.