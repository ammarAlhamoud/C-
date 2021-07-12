using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Interfaces;
using Balls.Business.Services;
using Balls.Common.Models;
using Balls.Common.Infrastructure.UI.Controls;
using Balls.Common.Infrastructure;

namespace Balls.UI.ViewModel
{
    public class ReportViewModel : ViewModelBase, IReportViewModel
    {

        #region Variables & Properties

        public ScoreCardService ScoreCardService
        {
            get
            {
                return new ScoreCardService();
            }
        }

        private List<PlayerModel> _listOfPlayerDetails;
        public List<PlayerModel> ListOfPlayerDetails
        {
            get { return _listOfPlayerDetails; }
            set
            {
                _listOfPlayerDetails = value;
                this.OnPropertyChanged(() => ListOfPlayerDetails);
            }
        }

        private DelegateCommand _counterCommand;
        public DelegateCommand CounterCommand
        {
            get
            {
                return _counterCommand ??
                (_counterCommand = new DelegateCommand(CounterClick, CanCounterClick));

            }
        }

        private DelegateCommand _balancerCommand;
        public DelegateCommand BalancerCommand
        {
            get
            {
                return _balancerCommand ??
                (_balancerCommand = new DelegateCommand(BalancerClick, CanBalancerClick));

            }
        }

        public Common.Interfaces.Base.IBaseView View
        {
            get;
            set;
        }
        #endregion

        #region ctor
        public ReportViewModel(IReportView view)
        {
            this.View = view;
            this.View.SetViewModel(this);

        }
        #endregion

        #region Methods

        public void LoadData(string uri)
        {
            ListOfPlayerDetails = ScoreCardService.GetAllPlayerDetails();
        }

        private bool CanCounterClick(object param)
        {
            return true;
        }

        private int GetLevel(int recno, int gameRecno)
        {
            var playerModel = ListOfPlayerDetails.Where(x => x.Recno == recno).Select(x => x).First();
            var level = playerModel.ScoreCards[gameRecno - 1].Level;
            "".APPPageData().IsoSetData(playerModel);
            if (level == 0)
                level++;

            return level;
        }

        private void CounterClick(object param)
        {
            string.Format("{0}?{1}", XAML.BallCounterGame, GetLevel(param.ToString().ToInt(), 1)).Navigate();
        }

        private bool CanBalancerClick(object param)
        {
            return true;
        }

        private void BalancerClick(object param)
        {
            string.Format("{0}?{1}", XAML.GamePage, GetLevel(param.ToString().ToInt(), 2)).Navigate();
        }
        #endregion

    }
}
