
namespace PacificBattle.CombatResolution
{
    public sealed class SelectionCoordinator<T> where T : class
    {
        public Dictionary<T, T> Pairs => _pairs;
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
                _pairComplete = false;
                return true;
            }

            // Deselect same item on second click
            if (EqualityComparer<T>.Default.Equals(_pending, item))
            {
                _pending = null;
                _pairComplete = false;
                return true;
            }

            // Add second item and add pair to dictionary
            _pairs[_pending] = item;

            // End selection round
            _pending = null;
            _pairComplete = true;
            return true;
        }
    }
}
