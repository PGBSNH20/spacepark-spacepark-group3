using SpacePark.Config;

namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConsoleGUI gui = new();
            gui.LoadGUI(AppConfig.GetConfig().Name);
        }
    }
}