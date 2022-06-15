namespace mini_omega
{
    public interface IVideoOutputStrategy
    {
         void Output(int width, double framerate);
    }
}