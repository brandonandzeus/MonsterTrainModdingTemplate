using BepInEx;
using HarmonyLib;
using MonsterTrainModdingTemplate.Champions;
using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.CardTraits;
using MonsterTrainModdingTemplate.CustomEffects.RoomModifiers;
using MonsterTrainModdingTemplate.Enhancers;
using MonsterTrainModdingTemplate.Misc;
using MonsterTrainModdingTemplate.ModifyExistingContent;
using MonsterTrainModdingTemplate.MonsterCards;
using MonsterTrainModdingTemplate.Relics;
using MonsterTrainModdingTemplate.SpellCards;
using MonsterTrainModdingTemplate.StatusEffects;
using System.Collections.Generic;
using Trainworks.BuildersV2;
using Trainworks.Interfaces;

namespace MonsterTrainModdingTemplate
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("tools.modding.trainworks")]
    public class TestPlugin : BaseUnityPlugin, IInitializable
    {
        public const string GUID = "com.name.package.generic";
        public const string NAME = "Test Plugin";
        public const string VERSION = "1.0.0";
        // This needs to be an unique identifier. Used to ensure IDs are unique.
        public const string CLANID = "TestClan";

        private void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        public void Initialize()
        {
            // Automatically find types in this assembly with a BuildAndRegister static method and call it.
            /*var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                var method = type.GetMethod("BuildAndRegister", BindingFlags.Static | BindingFlags.Public);
                if (method != null)
                {
                    _ = method.Invoke(null, null);
                }
            }*/

            // Or if you prefer to call them all manually.

            // Start with the clan.
            Clan.BuildAndRegister();
            // Then Starter cards if using custom made cards.
            Rustify.BuildAndRegister();
            // Then Champions
            Champion.BuildAndRegister();
            ExiledChampion.BuildAndRegister();
            // Subtypes
            Subtypes.BuildAndRegister();
            // Cards
            ModifyFrozenLance.Modify();
            NotHornBreak.BuildAndRegister();
            GiveEveryoneArmor.BuildAndRegister();
            PlayOtherCards.BuildAndRegister();
            IcyBoost.BuildAndRegister();
            PyreSharpener.BuildAndRegister();
            FrostFury.BuildAndRegister();
            BlueEyesWhiteDragon.BuildAndRegister();
            DragonCostume.BuildAndRegister();
            AppleMorsel.BuildAndRegister();
            TrinityBurst.BuildAndRegister();
            TrinityCharge.BuildAndRegister();
            FrostFangFerox.BuildAndRegister();
            EvergrowingCreeper.BuildAndRegister();
            // Relics
            Wimpcicle.BuildAndRegister();
            EmberRefunder.BuildAndRegister();
            EmberExchanger.BuildAndRegister();
            // Enhancers
            StealthyStone.BuildAndRegister();
            // StatusEffects
            Weaken.BuildAndRegister();
            // Banner
            ClanBanner.BuildAndRegister();

            // Custom Trait Tooltips.
            var traits = (List<string>)AccessTools.Field(typeof(TooltipContainer), "TraitsSupportedInTooltips").GetValue(null);
            traits.Add(nameof(CardTraitTrinity));
            // Custom RoomModifier TooltipTitle.
            BuilderUtils.ImportStandardLocalization(nameof(RoomStateFrostbiteDamageModifier), "Bitter Cold");
        }
    }
}
