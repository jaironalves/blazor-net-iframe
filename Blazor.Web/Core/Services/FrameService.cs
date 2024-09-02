using Blazor.Web.Core.Domain;

namespace Blazor.Web.Core.Services
{
    public class FrameService : IFrameObservable
    {
        private readonly IConfiguration _configuration;
        private readonly List<IFrameObserver> _observers = [];

        public List<FrameScript> Frames { get; private set; }

        public FrameService(IConfiguration configuration)
        {
            _configuration = configuration;
            LoadIframeSettings();
            StartTimers();
        }

        private void LoadIframeSettings()
        {            
            Frames = _configuration.GetSection("FrameScripts").Get<List<FrameScript>>();
        }

        private void StartTimers()
        {
            foreach (var frame in Frames)
            {
                Timer timer = new(async _ =>
                {
                    await UpdateClienteCountAsync(frame);
                }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            }
        }

        private static Task UpdateClienteCountAsync(FrameScript frameScript)
        {
            try
            {                
                var client = new HttpClient();
                //var response = await client.GetStringAsync("https://example.com/service.asmx/GetClienteCount");
                frameScript.ClientCount = new Random(1).Next(1, 3000); //int.Parse(response);                
                    //int.Parse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter a contagem de clientes: {ex.Message}");                
            }

            return Task.CompletedTask;
        }

        public void Subscribe(IFrameObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubscribe(IFrameObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
