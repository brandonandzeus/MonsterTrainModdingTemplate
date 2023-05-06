using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.CardEffects;
using MonsterTrainModdingTemplate.CustomEffects.CardTraits;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.Managers;
using static RimLight;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class TrinityCharge
    {
        public static readonly string ID = TestPlugin.CLANID + "_TrinityCharge";

        public static void BuildAndRegister()
        {
            // Use the new CardEffect
            var effect = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectFillTrinity),
                // Param to set Trinity Traits to.
                ParamInt = 2,
                // Popup a nice tooltip explaining the Trinity keyword.
                AdditionalTooltips =
                {
                    new AdditionalTooltipData
                    {
                        // This must be a localized text key
                        titleKey = "CardTraitTrinity_CardTooltipTitle",
                        descriptionKey = "CardTraitTrinity_CardTooltipText",
                        // This is the default.
                        // style = TooltipDesigner.TooltipDesignType.Keyword
                        isStatusTooltip = false,
                        isTriggerTooltip = false,
                        isTipTooltip = false,
                    }
                }
            };

            // Simple localization for one language only.
            BuilderUtils.ImportStandardLocalization("CardTraitTrinity_CardTooltipTitle", "Trinity");
            BuilderUtils.ImportStandardLocalization("CardTraitTrinity_CardTooltipText", "Every 3rd time a Trinity card is played, the card will cost 0 ember and have an additional 50 Magic Power");

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Trinity Charge",
                Description = "Fully charge <b>Trinity</b> cards in hand and in the draw pile",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = Clan.ID,
                AssetPath = "assets/trinitycharge.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    effect
                },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitExhaustState),
                    }
                },
                
            }.BuildAndRegister();
        }
    }
}