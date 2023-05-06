using MonsterTrainModdingTemplate.CustomEffects.CardTraits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingTemplate.CustomEffects.CardEffects
{
    class CardEffectFillTrinity : CardEffectBase
    {
        public override bool CanPlayAfterBossDead => false;
        // Important to set to false! In this case we don't want to apply this effect in preview mode.
        // Otherwise, when the card with this effect is drawn, the cards are immediately upgraded...
        public override bool CanApplyInPreviewMode => false;

        // This determines if you can play the card. You normally check for things such as if there's a valid target.
        // Here, the card is always playable.
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            return true;
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            // Get your current hand.
            List<CardState> hand = cardEffectParams.cardManager.GetHand();
            foreach (CardState card in hand)
            {
                foreach (CardTraitState trait in card.GetTraitStates())
                {
                    if (trait is CardTraitTrinity)
                    {
                        // Set trait to activate Trinity.
                        trait.SetParamInt(cardEffectState.GetParamInt());
                        // Kick the card the update its text.
                        card.UpdateCardBodyText();
                        // Also refresh the card.
                        cardEffectParams.cardManager?.RefreshCardInHand(card, cleanupTweens: false);
                        // And show the Enhance effect on the card. This is a method call on the corresponding CardUI object.
                        cardEffectParams.cardManager.GetCardInHand(card)?.ShowEnhanceFX();
                        break;
                    }
                }
            }

            // Also do this for all cards in the draw pile.
            foreach (CardState card in cardEffectParams.cardManager.GetDrawPile())
            {
                foreach (var trait in card.GetTraitStates())
                {
                    if (trait is CardTraitTrinity)
                    {
                        trait.SetParamInt(cardEffectState.GetParamInt());
                        // No need to refresh these cards as they are in the draw pile.
                        break;
                    }
                }
            }

            // No other IEnumerator methods were called, so we yield break.
            yield break;
        }
    }
}
