using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Application.ViewModels
{
    [ImplementPropertyChanged]
    public class BoardViewModel
    {
        public ObservableCollection<CardViewModel> Cards { get; }

        
    }
}
