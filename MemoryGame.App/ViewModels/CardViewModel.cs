using PropertyChanged;
using MemoryGame.Core;
using Cirrious.MvvmCross.ViewModels;
using System;

namespace MemoryGame.App.ViewModels
{
    public class CardViewModel : MvxViewModel
    {
        private readonly int boardPosition;
        private Card<string> card;

        public CardViewModel(Card<string> card, int boardPosition)
        {
            this.card = card;
            this.card.StatusChanged += CardStatusChanged;
            this.Status = this.card.Status;
            this.boardPosition = boardPosition;
        }

        public CardStatus Status { get; set; }

        public int BoardPosition => boardPosition;

        public string Value => card.Value;

        void CardStatusChanged(object sender, EventArgs e)
        {
            Status = this.card.Status;
            RaisePropertyChanged(nameof(Status));
        }

    }
}
