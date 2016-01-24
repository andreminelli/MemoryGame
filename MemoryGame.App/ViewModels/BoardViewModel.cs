using Cirrious.MvvmCross.ViewModels;
using MemoryGame.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.App.ViewModels
{
    [ImplementPropertyChanged]
    public class BoardViewModel : MvxViewModel
    {
        Board<string> board;

        public ObservableCollection<CardViewModel> Cards { get; }

        public BoardViewModel()
        {
            board = Board.From(new[] { "!", "N", ",", "k", "b", "v", "w", "z", "A" });
            Cards = new ObservableCollection<CardViewModel>(board.Select(c => new CardViewModel(c)));
        }
    }
}
