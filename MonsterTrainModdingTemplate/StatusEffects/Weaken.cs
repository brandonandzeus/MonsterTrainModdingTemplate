using MonsterTrainModdingTemplate.CustomEffects.StatusEffects;
using Trainworks.BuildersV2;

namespace MonsterTrainModdingTemplate.StatusEffects
{
    class Weaken
    {
        public static void BuildAndRegister()
        {
            // Register the status effect with StatusEffectManager
            new StatusEffectDataBuilder
            {
                // Status Effect Subclass to use to run the effect.
                StatusEffectStateType = typeof(StatusEffectWeakenState),
                StatusID = StatusEffectWeakenState.StatusId,
                Name = "Weaken",
                Description = "Unit takes additional damage equal to the number of weaken stacks when attacked",
                DisplayCategory = StatusEffectData.DisplayCategory.Negative,
                // Determines when to test to trigger the status effect.
                TriggerStage = StatusEffectData.TriggerStage.OnPreAttacked,
                RemoveAtEndOfTurn = false,
                // Free image from flaticon. Attribution <a href="https://www.flaticon.com/free-icons/weakness" title="weakness icons">Weakness icons created by Freepik - Flaticon</a>
                // This should be a black and white image sized 24x24.
                IconPath = "assets/status_weakness.png",
                ParamInt = 1,
            }.Build();
        }
    }
}
