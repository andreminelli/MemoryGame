using Cirrious.MvvmCross.ViewModels;
using MemoryGame.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemoryGame.App.ViewModels
{
    public class BoardViewModel : MvxViewModel
    {
        Board<string> board;

        private TurnResult currentTurnResult;

        public IList<CardViewModel> Cards { get; }

        public ICommand TurnUp { get; }

        public BoardViewModel()
        {
            board = Board.From(new[] { "!", "N", ",", "K", "#", "v", "w", "z", "A" });
            board.TurnEnded += Board_TurnEnded;
            currentTurnResult = TurnResult.Pending;

            Cards = new List<CardViewModel>(board.Select((c,i) => new CardViewModel(c,i)));
            TurnUp = new MvxCommand<int>(async (p) => await TurnUpActionAsync(p));
        }

        async Task TurnUpActionAsync(int position)
        {
            if (currentTurnResult == TurnResult.Pending)
            {
                await board.TurnUp(position);
            }
        }

        async void Board_TurnEnded(object sender, TurnResultEventArgs args)
        {
            currentTurnResult = args.Result;

            using (args.GetDeferral())
            {
                if (currentTurnResult != TurnResult.Pending)
                {
                    await Task.Delay(2000);
                    currentTurnResult = TurnResult.Pending;
                }
            }
        }

    }
}
