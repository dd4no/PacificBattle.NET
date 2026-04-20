
namespace PacificBattle.CombatResolution
{
    public sealed class SelectionCoordinator<T> where T : class
    {
        public IReadOnlyDictionary<T, T> Pairs => _pairs;
        public T? Pending => _pending;
        public bool PairComplete => _pairComplete;

        private readonly Dictionary<T, T> _pairs = [];
        private T? _pending;
        private bool _pairComplete;

        public void StartRound()
        {
            _pairs.Clear();
            _pending = null;
            _pairComplete = false;
        }

        public bool Click(T item)
        {
            // Select first item
            if (_pending == null)
            {
                _pending = item;
                return true;
            }

            // Deselect if clicked again
            if (EqualityComparer<T>.Default.Equals(_pending, item))
            {
                _pending = null;
                return true;
            }

            // Add Pairing to dictionary
            _pairs[_pending] = item;
            _pairComplete = true;
            _pending = null;
            return true;
        }
    }
}
