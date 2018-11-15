using Tharga.Toolkit.Console;
using Tharga.Toolkit.Console.Commands;
using Tharga.Toolkit.Console.Consoles;

namespace Hubcap.TestClient
{
    internal class Program
    {
        private readonly Game _game = new Game();

        private Program(string[] args)
        {
            using (var console = new ClientConsole())
            {
                var rootCommand = new RootCommand(console);
                rootCommand.RegisterCommand(new StartGameConsoleCommand(_game));
                rootCommand.RegisterCommand(new GetBoardCommand(_game));
                rootCommand.RegisterCommand(new MoveCommand(_game));

                var engine = new CommandEngine(rootCommand);
                engine.Start(args);
            }
        }

        private static void Main(string[] args)
        {
            new Program(args);
        }
    }
}