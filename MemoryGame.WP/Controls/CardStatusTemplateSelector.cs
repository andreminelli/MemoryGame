using MemoryGame.App.ViewModels;
using MemoryGame.Core;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MemoryGame.WP.Controls
{
    public class CardStatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CardUp { get; set; }

        public DataTemplate CardDown { get; set; }

        public DataTemplate CardMatched { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var model = item as CardViewModel;

            PropertyChangedEventHandler lambda = null;
            lambda = (o, args) =>
            {
                if (args.PropertyName == "Status")
                {
                    model.PropertyChanged -= lambda;
                    var cc = (ContentControl)container;
                    cc.ContentTemplateSelector = null;
                    cc.ContentTemplateSelector = this;
                }
            };

            model.PropertyChanged += lambda;
            switch (model?.Status)
            {
                case CardStatus.Up: return CardUp;
                case CardStatus.Matched: return CardMatched;
                default:
                    return CardDown;
            }


        }
    }
}
