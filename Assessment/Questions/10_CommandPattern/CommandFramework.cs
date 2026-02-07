namespace CommandPattern
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class ShoppingCart
    {
        public List<string> Items { get; } = new();
        public decimal TotalDiscount { get; set; }
    }

    public class AddItemCommand : ICommand
    {
        private readonly ShoppingCart _cart;
        private readonly string _item;

        public AddItemCommand(ShoppingCart cart, string item)
        {
            _cart = cart;
            _item = item;
        }

        public void Execute()
        {
            _cart.Items.Add(_item);
        }

        public void Undo()
        {
            _cart.Items.Remove(_item);
        }
    }

    public class RemoveItemCommand : ICommand
    {
        private readonly ShoppingCart _cart;
        private readonly string _item;
        private int _removedIndex = -1;

        public RemoveItemCommand(ShoppingCart cart, string item)
        {
            _cart = cart;
            _item = item;
        }

        public void Execute()
        {
            _removedIndex = _cart.Items.IndexOf(_item);
            if (_removedIndex >= 0)
            {
                _cart.Items.RemoveAt(_removedIndex);
            }
        }

        public void Undo()
        {
            if (_removedIndex >= 0)
            {
                _cart.Items.Insert(_removedIndex, _item);
            }
        }
    }

    public class ApplyDiscountCommand : ICommand
    {
        private readonly ShoppingCart _cart;
        private readonly decimal _discount;
        private decimal _previousDiscount;

        public ApplyDiscountCommand(ShoppingCart cart, decimal discount)
        {
            _cart = cart;
            _discount = discount;
        }

        public void Execute()
        {
            _previousDiscount = _cart.TotalDiscount;
            _cart.TotalDiscount += _discount;
        }

        public void Undo()
        {
            _cart.TotalDiscount = _previousDiscount;
        }
    }

    public class CommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
            }
        }
    }
}
