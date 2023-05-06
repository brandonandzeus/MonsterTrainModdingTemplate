using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using static CharacterTriggerData;
using static RimLight;

namespace MonsterTrainModdingTemplate.SpellCards
{
    public class PyreSharpener
    {
        public static readonly string ID = TestPlugin.CLANID + "_PyreSharpener";
        public static readonly string TriggerID = TestPlugin.CLANID + "_PyreSharpenerReserve";

        public static void BuildAndRegister()
        {
            var sharpenPyre = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectBuffDamage),
                TargetMode = TargetMode.Pyre,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = 3,
            };

            var effect = new CardTriggerEffectDataBuilder
            {
                TriggerID = TriggerID,
                Trigger = CardTriggerType.OnUnplayed,
                Description = "Apply +[effect0.power][attack] to your Pyre.",
                CardEffectBuilders = {sharpenPyre},
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Pyre Sharpener",
                CostType = CardData.CostType.NonPlayable,
                Cost = 0,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = VanillaClanIDs.Stygian,
                // Image credit from Stockvault stockvault-melting-ice-cube294324.
                AssetPath = "assets/pyresharpener.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                TriggerBuilders = {effect},
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitUnplayable),
                    }
                }

            }.BuildAndRegister();
        }
    }
}