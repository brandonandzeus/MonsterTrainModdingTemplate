using System.Collections;

namespace MonsterTrainModdingTemplate.CustomEffects.CardTraits
{
    public class CardTraitTrinity : CardTraitState
    {
        public override IEnumerator OnCardPlayed(CardState cardState, CardManager cardManager, RoomManager roomManager)
        {
            // If Trinity effect was played reset ParamInt
            // otherwise increment ParamInt
            if (IsTrinity())
                SetParamInt(0);
            else
                SetParamInt(GetParamInt() + 1);

            // Required for IEnumerator methods if you don't call another IEnumerator method
            yield break;
        }

        // Method that can overrides the amount of damage done.
        public override int OnApplyingDamage(ApplyingDamageParameters damageParams)
        {
            // If trinity effect is active increase the damage by 50.
            if (IsTrinity())
                damageParams.damage += 50;
            return damageParams.damage;
        }

        // Method that can modify the cost of a card.
        public override int GetModifiedCost(int cost, CardState thisCard, CardStatistics cardStats, MonsterManager monsterManager)
        {
            // Make it cost 0 if Trinity effect is active
            if (IsTrinity())
            {
                return 0;
            }
            return cost;
        }

        // Helper to determine when the Trinity Effect is active.
        public bool IsTrinity()
        {
            return GetParamInt() == 2;
        }

        // This method is how CardTraits add themselves to the card.
        public override string GetCardText()
        {
            // If you don't intend on providing localization. You can just return the text.
            return string.Format("<b>Trinity {0}</b>", GetParamInt() + 1);

            // Highly recommended to create a LocalizationKey
            //return string.Format(LocalizeTraitKey("CardTraitTrinity_CardText"), GetParamInt());
        }

        // This method provides a TooltipTile. If you intend on showing a Tooltip the CardTraitClass Name needs to be whitelisted for it.
        // See: TestPlugin.SetupTraits
        public override string GetCardTooltipTitle()
        {
            return "Trinity";
            // Localization version
            //return LocalizeTraitKey("CardTraitTrinity_TooltipTitle");
        }

        // This method provides the TooltipText. Again if you intend on showing a Tooltip the CardTraitClass Name needs to be whitelisted for it.
        // See: TestPlugin.SetupTraits
        public override string GetCardTooltipText()
        {
            int times = 2 - GetParamInt();
            if (!IsTrinity())
                return string.Format("When this card is played {0} more times, the card will cost 0 ember and have an additional 50 Magic Power", times);
            else
                return string.Format("This card costs 0 ember and has an additional 50 Magic Power");
            // return string.Format("CardTraitTrinity_TooltipText".Localize(), GetParamInt());
        }

        // This menthod provides the TooltipId. It is used to prevent the generated tooltip from being displayed multiple times.
        public override string GetCardTooltipId()
        {
            return "CardTraitTrinity";
        }

        // Much like CarTraits can add themselves to the Card's text it can also append text to the CardEffect.
        // Here we use this to indicate when Trinity is active to display that the card will have 50 magic power.
        public override string GetCurrentEffectText(CardStatistics cardStatistics, SaveManager saveManager, RelicManager relicManager)
        {
            if (IsTrinity())
                return "<b>+50 Magic Power</b>";
            else
                return string.Empty;
        }
    }
}
