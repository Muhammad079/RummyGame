using UnityEngine;

public class Shiny_Effect_Pauser : MonoBehaviour
{
    private void OnEnable()
    {
        if (MainMenuStats.instance)
            MainMenuStats.instance.PauseShineEffect();
    }
    internal void Pause()
    {
        if (MainMenuStats.instance)
            MainMenuStats.instance.PauseShineEffect();
    }

    internal void StartShine()
    {
        if (MainMenuStats.instance)
            MainMenuStats.instance.StartShineEffect();
    }
}
