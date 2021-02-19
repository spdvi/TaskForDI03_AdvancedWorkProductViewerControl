using System;

namespace ProductViewControl
{
    public class SizeButtonClickedEventArgs: EventArgs
    {
        public int ProductId { get; set; }
    }
}