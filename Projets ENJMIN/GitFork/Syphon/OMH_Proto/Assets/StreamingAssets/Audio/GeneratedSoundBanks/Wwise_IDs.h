/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_AMB_BLENDTEST = 2118589750U;
        static const AkUniqueID PLAY_AMB_INMENU_CREDITS = 2230101518U;
        static const AkUniqueID PLAY_AMB_INMENU_MAINMENU = 518201918U;
        static const AkUniqueID PLAY_AMB_MUSIC_MAINMENU_V1 = 241809481U;
        static const AkUniqueID PLAY_SFX_INMENU_OPTIONSDISPLAY = 3936488577U;
        static const AkUniqueID PLAY_SFX_MENU_CHANGEPAGEBACK = 3919501610U;
        static const AkUniqueID PLAY_SFX_MENU_CHANGEPAGESWOOSH = 1842550574U;
        static const AkUniqueID PLAY_START_AMB_MUS_INMENU_MAINMENU = 1403299731U;
        static const AkUniqueID PLAY_TESTSHOOT = 2635067583U;
        static const AkUniqueID PLAY_UI_MENU_BACK = 777803296U;
        static const AkUniqueID PLAY_UI_MENU_CONFIRM = 3500366795U;
        static const AkUniqueID PLAY_UI_MENU_CONFIRM_SWITCHPERKWEAPON = 1024592138U;
        static const AkUniqueID PLAY_UI_MENU_ERROR = 1225634717U;
        static const AkUniqueID PLAY_UI_MENU_HOOVER = 1620222164U;
        static const AkUniqueID PLAY_UI_MENU_SELECT = 1416073485U;
        static const AkUniqueID PLAY_UI_MENU_STARTGAME = 4232330581U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATE

        namespace PLAYERSTATE
        {
            static const AkUniqueID GROUP = 3285234865U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERSTATE

        namespace ROOMSTATE
        {
            static const AkUniqueID GROUP = 185713839U;

            namespace STATE
            {
                static const AkUniqueID INDOOR = 340398852U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OUTDOOR = 144697359U;
            } // namespace STATE
        } // namespace ROOMSTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GENERATORENERGY
        {
            static const AkUniqueID GROUP = 2554413648U;

            namespace SWITCH
            {
                static const AkUniqueID FULLENERGY = 2592180566U;
                static const AkUniqueID LOWENERGY = 1772229705U;
                static const AkUniqueID NEARDESTROYED = 2815309952U;
            } // namespace SWITCH
        } // namespace GENERATORENERGY

        namespace GROUNDMATERIALS
        {
            static const AkUniqueID GROUP = 1431031706U;

            namespace SWITCH
            {
                static const AkUniqueID GLASS = 2449969375U;
                static const AkUniqueID METAL = 2473969246U;
            } // namespace SWITCH
        } // namespace GROUNDMATERIALS

        namespace GROUNDWETNESS
        {
            static const AkUniqueID GROUP = 659943165U;

            namespace SWITCH
            {
                static const AkUniqueID DAMP = 1842026663U;
                static const AkUniqueID DRY = 630539344U;
                static const AkUniqueID SOAKED = 1905651656U;
            } // namespace SWITCH
        } // namespace GROUNDWETNESS

        namespace PLAYERHEALTH
        {
            static const AkUniqueID GROUP = 151362964U;

            namespace SWITCH
            {
                static const AkUniqueID FULLHEALTH = 2429688720U;
                static const AkUniqueID LOWHEALTH = 1017222595U;
                static const AkUniqueID MIDHEALTH = 4094608319U;
                static const AkUniqueID NEARDEATH = 898449699U;
            } // namespace SWITCH
        } // namespace PLAYERHEALTH

        namespace PLAYERSPEEDSWITCH
        {
            static const AkUniqueID GROUP = 2051106367U;

            namespace SWITCH
            {
                static const AkUniqueID FIRING = 860902532U;
                static const AkUniqueID RUNNING = 3863236874U;
                static const AkUniqueID WALKING = 340271938U;
            } // namespace SWITCH
        } // namespace PLAYERSPEEDSWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DENSITY_FA_SSGRAIN = 2715217995U;
        static const AkUniqueID IMMERSION_FA_SSGRAIN = 2481728872U;
        static const AkUniqueID INSTRUMENT_FA_SSGRAIN = 2317409760U;
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID PROXIMITY_FA_SSGRAIN = 1791284502U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID RPM_FA_SSGRAIN = 1656280998U;
        static const AkUniqueID RTPC_DISTANCE = 262290038U;
        static const AkUniqueID RTPC_GROUNDWETNESS = 870672907U;
        static const AkUniqueID RTPC_LPMUSICMENU = 3992807061U;
        static const AkUniqueID RTPC_PLAYERHEALTH = 3975082306U;
        static const AkUniqueID RTPC_PLAYERSPEED = 2653406601U;
        static const AkUniqueID RTPC_TOD = 3588715232U;
        static const AkUniqueID SIMULATION_FA_SSGRAIN = 2428833394U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINSB = 1328360421U;
        static const AkUniqueID TESTSB = 3331313354U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID _2DAMBIENCE = 309309195U;
        static const AkUniqueID _2DAMBIENTSBEDS = 473286920U;
        static const AkUniqueID _3DAMBIENCE = 1301074112U;
        static const AkUniqueID AMB_MASTER = 3073528060U;
        static const AkUniqueID AMBIENTSBEDS = 4038353046U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MOTION_FACTORY_BUS = 985987111U;
        static const AkUniqueID MUS_INGAME = 562626134U;
        static const AkUniqueID MUS_INMENU = 3498363069U;
        static const AkUniqueID MUS_MASTER = 2086042571U;
        static const AkUniqueID PLAYER_EQUIPMENT = 575576591U;
        static const AkUniqueID PLAYER_FOOTSTEPS = 1730208058U;
        static const AkUniqueID PLAYER_LOCOMOTION = 1375983526U;
        static const AkUniqueID PLAYER_MASTER = 2747691115U;
        static const AkUniqueID PLAYER_SFX = 817096458U;
        static const AkUniqueID SFX_MASTER = 3205032327U;
        static const AkUniqueID UI_MASTER = 3075009468U;
        static const AkUniqueID VO_MASTER = 303617989U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID BIGROOM = 1495464960U;
        static const AkUniqueID CORRIDORS = 1938385722U;
        static const AkUniqueID REVERBS = 3545700988U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID DEFAULT_MOTION_DEVICE = 4230635974U;
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
