using StocksInvesthink.Services.Commands.Interfaces;

namespace StocksInvesthink.Services.Commands
{
    // Commande qui recalcule les indicateurs et régénère les signaux
    public class RunIndicatorsAndSignalsCommand : IAnalysisCommand
    {
        private readonly IndicatorService _indicatorService;
        private readonly SignalService _signalService;
        private readonly int _userId;
        private readonly int _stockId;

        public RunIndicatorsAndSignalsCommand(
            IndicatorService indicatorService,
            SignalService signalService,
            int userId,
            int stockId)
        {
            _indicatorService = indicatorService;
            _signalService = signalService;
            _userId = userId;
            _stockId = stockId;
        }

        public async Task ExecuteAsync()
        {
            // Calculer les indicateurs techniques
            await _indicatorService.GenerateSmaAsync(_userId, _stockId);
            await _indicatorService.GenerateEmaAsync(_userId, _stockId);
            await _indicatorService.GenerateRsiAsync(_userId, _stockId);

            // Générer les signaux techniques
            await _signalService.GenerateSignalsFromSmaAsync(_userId, _stockId);
            await _signalService.GenerateSignalsFromEmaAsync(_userId, _stockId);
            await _signalService.GenerateSignalsFromRsiAsync(_userId, _stockId);
        }
    }
}
