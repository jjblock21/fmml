using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    /*
     * FireworkIds
Rockets
    Rocket_AmazingGranny_Purple
    Rocket_AmazingGranny_Yellow
    Rocket_ChuteBoi_Blue
    Rocket_ChuteBoi_Green
    Rocket_ChuteBoi_Purple
    Rocket_ChuteBoi_Red
    Rocket_Cobber905Blue
    Rocket_Cobber905Green
    Rocket_Cobber905Red
    Rocket_Cobber905White
    Rocket_Cobber905Yellow
    Rocket_FearOfTheDark
    Rocket_Lunar2021_Oxblaster
    Rocket_Lunar2021_Oxcaper
    Rocket_Slammer
    Rocket_Strobilicious
    Rocket_StrobingBob
    Rocket_ThicBilly
    Rocket_Viper
    Rocket_ViperToo
    Rocket_WorldStopper
    Rocket_Foxy
Cakes
    Cake_2021
    Cake_BeeHive
    Cake_Boom
    Cake_ChineseWall
    Cake_Criminalis
    Cake_Dragon
    Cake_FatBill
    Cake_Lunar2021_Oxcelurator
    Cake_Lunar2021_OxWannaGo
    Cake_PopperBoi
    Cake_ShogunCrackling
    Cake_TheMessenger
    Cake_BlueVelvet
    Cake_Hurry_Up
    Cake_PositiveNeon
    Cake_Sams_Gone_Wild
Firecrackers
    Firecracker_DoomBoom
    Firecracker_KokkenRulle
    Firecracker_LadyFingers
    Firecracker_MaleToes
    Firecracker_Snapper_Green
    Firecracker_Snapper_Purple
    Firecracker_Snapper_Red
    Firecracker_Snapper_White
    Firecracker_Snapper_Blue
Novelties
    RomanCandle_InsaneScreamer
    RomanCandle_MicoolaCandle
    RomanCandle_MoStick
    RomanCandle_SantaWasHere
    RomanCandle_WandOfWizards
    Whistler_ScreamingPal
    Zipper_Mozzie
    SmokeBomb_SmokeBall_Green
    SmokeBomb_SmokeBall_Purple
    SmokeBomb_SmokeBall_Red
    SmokeBomb_SmokeBall_White
    SmokeBomb_SmokeBall_Yellow
Special
    Special_Shell_Tim
    Special_Rocket_KarlSon [NOT IN THE GAME YET]
Tubes
    PreloadedTube_CodyBoom
    PreloadedTube_DraeBlast
    PreloadedTube_FoxUnlocked
    PreloadedTube_Imphentazia
    PreloadedTube_MrBeats
    PreloadedTube_Pawluten
    PreloadedTube_PewDiePew
    PreloadedTube_SepticEyeInTheSky
    PreloadedTube_CaptainSensible
Props
    Prop_Prototype_Crate
    Prop_Town_PropaneTank_01
    Prop_Town_PropaneTank_02
    Prop_Town_PropaneTank_Tall_01
    Prop_Town_PropaneTank_Tall_02
    Prop_Light_Spotlight_White
Fountains
    Fountain_Serious_Sam
    */
    public sealed class KnownFireworks
    {
        public KnownFireworks()
        {
            TimShell = "Special_Shell_Tim";
            KarlsonRocket = "Special_Rocket_KarlsSon";

            Rockets = new[]
            {
                "Rocket_AmazingGranny_Purple",
                "Rocket_AmazingGranny_Yellow",
                "Rocket_ChuteBoi_Blue",
                "Rocket_ChuteBoi_Green",
                "Rocket_ChuteBoi_Purple",
                "Rocket_ChuteBoi_Red",
                "Rocket_Cobber905Blue",
                "Rocket_Cobber905Green",
                "Rocket_Cobber905Red",
                "Rocket_Cobber905White",
                "Rocket_Cobber905Yellow",
                "Rocket_FearOfTheDark",
                "Rocket_Lunar2021_Oxblaster",
                "Rocket_Lunar2021_Oxcaper",
                "Rocket_Slammer",
                "Rocket_Strobilicious",
                "Rocket_StrobingBob",
                "Rocket_ThicBilly",
                "Rocket_Viper",
                "Rocket_ViperToo",
                "Rocket_WorldStopper",
                "Rocket_Foxy"
            };

            Cakes = new[]
            {
                "Cake_2021",
                "Cake_BeeHive",
                "Cake_Boom",
                "Cake_ChineseWall",
                "Cake_Criminalis",
                "Cake_Dragon",
                "Cake_FatBill",
                "Cake_Lunar2021_Oxcelurator",
                "Cake_Lunar2021_OxWannaGo",
                "Cake_PopperBoi",
                "Cake_ShogunCrackling",
                "Cake_TheMessenger",
                "Cake_BlueVelvet",
                "Cake_Hurry_Up",
                "Cake_PositiveNeon",
                "Cake_Sams_Gone_Wild"
            };

            Firecrackers = new[]
            {
                "Firecracker_DoomBoom",
                "Firecracker_KokkenRulle",
                "Firecracker_LadyFingers",
                "Firecracker_MaleToes",
                "Firecracker_Snapper_Green",
                "Firecracker_Snapper_Purple",
                "Firecracker_Snapper_Red",
                "Firecracker_Snapper_White",
                "Firecracker_Snapper_Blue"
            };

            Novelties = new[]
            {
                "RomanCandle_InsaneScreamer",
                "RomanCandle_MicoolaCandle",
                "RomanCandle_MoStick",
                "RomanCandle_SantaWasHere",
                "RomanCandle_WandOfWizards",
                "Whistler_ScreamingPal",
                "Zipper_Mozzie",
                "SmokeBomb_SmokeBall_Green",
                "SmokeBomb_SmokeBall_Purple",
                "SmokeBomb_SmokeBall_Red",
                "SmokeBomb_SmokeBall_White",
                "SmokeBomb_SmokeBall_Yellow"
            };

            Tubes = new[]
            {
                "PreloadedTube_CodyBoom",
                "PreloadedTube_DraeBlast",
                "PreloadedTube_FoxUnlocked",
                "PreloadedTube_Imphentazia",
                "PreloadedTube_MrBeats",
                "PreloadedTube_Pawluten",
                "PreloadedTube_PewDiePew",
                "PreloadedTube_SepticEyeInTheSky",
                "PreloadedTube_CaptainSensible",
                "Prop_Light_Spotlight_White"
            };

            Fountains = new[]
            {
                "Fountain_Serious_Sam"
            };
        }

        //TODO: Add new Fireworks

        public string TimShell { get; }
        public string KarlsonRocket { get; }
        public string[] Rockets { get; }
        public string[] Cakes { get; }
        public string[] Firecrackers { get; }
        public string[] Novelties { get; }
        public string[] Tubes { get; }
        public string[] Fountains { get; }
    }
}
