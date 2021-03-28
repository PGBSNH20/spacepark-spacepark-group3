using SpacePark.Config;
using SpacePark.Logic;
using Program.GUI;

namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GUI.GUI gui = new(AppConfig.GetConfig().Name);
            gui.LoadGUI();
        }
    }
}