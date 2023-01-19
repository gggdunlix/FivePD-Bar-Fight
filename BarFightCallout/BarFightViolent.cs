using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



namespace BarFightViolent
{
    [CalloutProperties("Bar Fight (Violent)", "GGGDunlix", "0.2.1")]
    public class BarFightV : Callout
    {
        Ped suspect, suspect2;

        private Vector3[] coordinates = {
            new Vector3(-1390f, -585f, 30f),
            new Vector3(-556f, 285f, 82f),
            new Vector3(1214f, -415f, 67f),
            new Vector3(498.7759f, -1538.609f, 29.26939f),
            new Vector3(-300.3598f, 6255.368f, 31.52869f),
            new Vector3(-262.1636f, 6290.942f, 31.48914f),
            new Vector3(-122.5331f, 6390.06f, 32.17758f),
            new Vector3(126.7416f, -1285.566f, 29.28325f),

        };

        public BarFightV()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(1).First();

            InitInfo(location);
            ShortName = "Bar Fight";
            CalloutDescription = "2 Assailants at a bar are fighting each other with weapons. Respond in Code 3.";
            ResponseCode = 3;
            StartDistance = 60f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
            
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            
            List<WeaponHash> weapons = new List<WeaponHash>() {
                WeaponHash.Knife,
                WeaponHash.Bottle,
                WeaponHash.Bat,
                WeaponHash.Switchblade,
                WeaponHash.Knuckle
            
            }

            suspect.Weapons.Give(weapons.SelectRandom(), 1, true, true);
            suspect2.Weapons.Give(weapons.SelectRandom(), 1, true, true);
            suspect.Task.FightAgainst(suspect2);
            suspect2.Task.FightAgainst(suspect);
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect.Armor = 4000;
            suspect2.Armor = 4000;
            
        }
    }


}
