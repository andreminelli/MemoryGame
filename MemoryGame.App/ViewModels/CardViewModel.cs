using PropertyChanged;
using MemoryGame.Core;

namespace MemoryGame.App.ViewModels
{
    [ImplementPropertyChanged]
    public class CardViewModel
    {
        private Card<string> card;

        public CardViewModel(Card<string> card)
        {
            this.card = card;
        }

        public string Value => card.Value;
    }
}
