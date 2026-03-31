using UnityEngine;

public enum NarativePoint
{
    GirlStart,
    GirlFlowers,
    GirlWolf,
    MomStart,
    MomPath,
    MomBridge,
    MomTrail
}

public class GameManager : MonoBehaviour
{
    // game states: Reading, Collecting, Walking, Default [+ Hunting?]
    // NarrativePoints + game states:
    /*      Reading - GirlStart, MomStart,MomBridge [just showing text in batch, no collectibles on scene]
     *      Collecting - GirlFlowers, MomTrail [show text back-to-back on every collectible's trigger enter]
     *      Walking - GirlWolf, MomPath [-//- on every trigger zone enter, no collectibles on scene]
     *     [TODO: Hunting - MomHunting - action-based input] 
     */
    
    // NarrativeManager holds story sequence data + set's story point to share lines with UIManager.
    // GameManager - holds game states, which correspond with chosen narrative points [how to communicate with narrative manager?]
    // GM can be a state machine.
    /*
     * interface IGameState{
     * public void Enter();
     * public void Update();
     * public void Exit();}
     */
    
    /*
     * ReadingState{
     * Enter() { Freeze player, tell narrative manager to show line/-s }
     * Update() {} [no need for logic in Update since we will switch to Collecting/Walking relying on typewriter finishing and invoking an event]
     * Exit() { Unfreeze player, tell narrative manager to hide panel } }
     *
     * CollectingState{
     * Enter() { Tell collectibles manager to spawn/activate pool objects [should be either 1/5, closest one to player, on scene or all 5 (then pool is unnecessary?)] }
     * Update() {} [no need for logic in Update since we would rely on events to switch to ReadingState on trigger enter]
     * Exit() { Tell spawner to deactivate collectibles on scene/ collected one we already seen the text for } }
     *
     * WalkingState{
     * Enter() { Unfreeze player? althought it's already done via event to unfreeze player...}
     * Update() { nothing, we move in PlayerBehaviour..}
     * Exit() { idk..}
     * maybe states are not fleshed out enough to require a whole ass state machine... Maybe just keep logic in modules, like UI, player, collectibles, utilities?
     * But then NarrativeManager is the director who sends events to signal we can progress with the game as the narrative line has been shown? Ugh.
     */
}
