using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public Game()
        {
            InputService = new InputService();
        }
    }
}