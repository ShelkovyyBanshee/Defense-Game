using Pausing;


namespace DefenseGame
{
    public interface IPauseController
    {
        public void AddPauseRequest();
        public void RemovePauseRequest();
        public void InitPauseComponent(PausingSystemProvider provider);
    }
}