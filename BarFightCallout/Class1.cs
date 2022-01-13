using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;



namespace BarFightNonviolentCallout
{
    [CalloutProperties("Bar Fight (Nonviolent)", "GGGDunlix", "0.0.2")]
    public class BarFightNV : Callout
    {
        Ped suspect, suspect2;

        public BarFightNV()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                InitInfo(new Vector3(-1390f, -585f, 30f));

            }
            else if (x >= 40 && x <= 60)
            {
                InitInfo(new Vector3(-556f, 285f, 82f));
            }
            else if (x >= 60)
            {
                InitInfo(new Vector3(1214f, -415f, 67f));
            }
            ShortName = "Bar Fight";
            CalloutDescription = "2 Assailants at a bar are fighting each other. Respond in Code 2.";
            ResponseCode = 2;
            StartDistance = 60f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.Task.FightAgainst(suspect2);
            suspect2.Task.FightAgainst(suspect);
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect.Armor = 4000;
            suspect2.Armor = 4000;
            
        }
    }


}
