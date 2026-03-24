namespace StocksInvesthink.Services.Commands.Interfaces
{
    public interface IAnalysisCommand
    {
        //Toutes les commandes d'analyse doivent implémenter cette méthode pour exécuter l'analyse de manière asynchrone
        Task ExecuteAsync(); 
    }
}
