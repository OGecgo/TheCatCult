public interface IPauseController
{
    // change global pause value
    public bool isPausedGlobal {get;} 
    public void PauseGame();
    public void UnpauseGame();

    // change local pause value
    public void PauseObjFeature(IPauseFeatures obj);
    public void UnpauseObjFeature(IPauseFeatures obj);
    public void PauseObjUpdate(IPauseUpdate obj);
    public void UnpauseObjUpdate(IPauseUpdate obj);
}
