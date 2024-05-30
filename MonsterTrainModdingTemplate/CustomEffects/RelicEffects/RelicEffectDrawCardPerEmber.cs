using System.Collections;

namespace MonsterTrainModdingTemplate.CustomEffects.RelicEffects
{
    // Implement IEndTurnPreHandDiscardRelicEffect
    // Inputs ParamInt: Number of cards to draw next turn per ember.
    class RelicEffectDrawCardPerEmber : RelicEffectBase, IEndTurnPreHandDiscardRelicEffect, IRelicEffect
    {
        private int emberPerCard;
        public override bool CanApplyInPreviewMode => false;
        public override bool CanShowNotifications => false;

        public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
        {
            // If you override this method be sure to call the base class's Initialize method otherwise you will get a null reference exception.
            base.Initialize(relicState, relicData, relicEffectData);
            // Fetch parameters here.
            emberPerCard = relicEffectData.GetParamInt();
        }

        public bool TestEffect(EndTurnPreHandDiscardRelicEffectParams relicEffectParams)
        {
            return relicEffectParams.playerManager.GetEnergy() > emberPerCard;
        }

        public IEnumerator ApplyEffect(EndTurnPreHandDiscardRelicEffectParams relicEffectParams)
        {
            int cards = relicEffectParams.playerManager.GetEnergy() / emberPerCard;
            relicEffectParams.cardManager.AdjustDrawCountModifier(cards);
            yield break;
        }
    }
}
