using StocksInvesthink.Services.Commands.Interfaces;

namespace StocksInvesthink.Services.Commands
{
    //Sauvegarde et exécute les commandes d'analyse de manière séquentielle
    public class AnalysisCommandInvoker
    {
        private readonly List<IAnalysisCommand> _commands = new();

        // Ajouter une commande à la file d'exécution
        public void AddCommand(IAnalysisCommand command)
        {
            _commands.Add(command);
        }

        // Exécute toutes les commandes enregistrées
        public async Task ExecuteAllAsync()
        {
            foreach (var command in _commands)
            {
                await command.ExecuteAsync();
            }
        }
    }
}
