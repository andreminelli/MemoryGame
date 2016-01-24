using PropertyChanged;
using MemoryGame.Core;
using Cirrious.MvvmCross.ViewModels;

namespace MemoryGame.App.ViewModels
{
    [ImplementPropertyChanged]
    public class CardViewModel : MvxViewModel
    {
        private Card<string> card;

        public CardViewModel(Card<string> card)
        {
            this.card = card;
        }

        public string Value => card.Value;
    }
}
