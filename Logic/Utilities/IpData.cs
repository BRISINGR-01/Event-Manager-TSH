namespace Logic.Utilities
{

    public class IpData
    {
        public int Count;
        public CancellationTokenSource tokenSource;
        private readonly Action Action;

        public IpData(Action action)
        {
            Count = 0;
            tokenSource = new CancellationTokenSource();
            Action = action;
            PrepareDispose();
        }
        private void PrepareDispose()
        {
            new Thread(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(20), tokenSource.Token);
                }
                catch
                {
                    return;
                }

                Remove();
            }).Start();
        }
        public void Update()
        {
            Count++;

            if (tokenSource.IsCancellationRequested) return;
            tokenSource.Cancel();
            if (!tokenSource.TryReset()) tokenSource = new CancellationTokenSource();

            PrepareDispose();
        }
        public void Remove()
        {
            if (tokenSource.IsCancellationRequested) return;

            tokenSource.Cancel();
            tokenSource.Dispose();
            Action();
        }
    }
}
