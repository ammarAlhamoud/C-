//------------------------------------------------------------------------------
// <auto-enerated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel;


namespace Balls.Data.BGMDB.Designer
{

    /// <summary>
    /// Base class for all the table classes and it handling property change events.
    /// </summary>
    public class TableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        public void OnPropertyChanging<T>(Expression<Func<T>> expression)
        {
            if (null != PropertyChanging)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnPropertyChanging(string propertyName)
        {
            if (null != PropertyChanging)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public void set<T, K>(Expression<Func<T>> expresson, ref K _variable, K value)
        {
            if (_variable == null || !_variable.Equals(value))
            {
                OnPropertyChanging(expresson);
                _variable = value;
                OnPropertyChanged(expresson);
            }
        }
    }

    public class BGMDBDataContext : DataContext
    {
        public BGMDBDataContext(string connnectionString) : base(connnectionString) { }

        public Table<game> games;
        public Table<player> players;
        public Table<scorecard> scorecards;
        public Table<setting> settings;

    }

    [Table]
    public class game : TableBase
    {
        public game()
        {
            _games_FK_Games_Games_Recno = new EntitySet<game>(new Action<game>(this.attachFK_Games_GamesRecno), new Action<game>(this.detachFK_Games_GamesRecno));
            _scorecards_FK_ScoreCard_Games_Recno = new EntitySet<scorecard>(new Action<scorecard>(this.attachFK_ScoreCard_GamesRecno), new Action<scorecard>(this.detachFK_ScoreCard_GamesRecno));
        }

        private Int32 _Recno;
        
        //PrimaryKey Property
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public Int32 Recno
        {
            get { return _Recno; }
            set { base.set(() => Recno, ref _Recno, value); }
        }

        private String _Name;
        [Column]
        public String Name
        {
            get { return _Name; }
            set { base.set(() => Name, ref _Name, value); }
        }

        private EntityRef<game> _game_FK_Games_Games_Recno;

        [Association(Storage = "_game_FK_Games_Games_Recno", ThisKey = "Recno", OtherKey = "Recno", IsForeignKey = true)]
        public game game_FK_Games_Games_Recno
        {
            get { return _game_FK_Games_Games_Recno.Entity; }
            set
            {
                base.OnPropertyChanging(() => game_FK_Games_Games_Recno);
                _game_FK_Games_Games_Recno.Entity = value;
                if (null != value)
                {
                    Recno = value.Recno;
                }
                base.OnPropertyChanged(() => game_FK_Games_Games_Recno);

            }
        }

        private EntitySet<game> _games_FK_Games_Games_Recno;
        [Association(Storage = "_games_FK_Games_Games_Recno", ThisKey = "Recno", OtherKey = "Recno")]
        public EntitySet<game> games_FK_Games_Games_Recno
        {
            get { return this.games_FK_Games_Games_Recno; }
            set { this.games_FK_Games_Games_Recno.Assign(value); }
        }

        private void attachFK_Games_GamesRecno(game game)
        {
            base.OnPropertyChanging(() => game);
        }

        private void detachFK_Games_GamesRecno(game game)
        {
            base.OnPropertyChanging(() => game);
        }

        private EntitySet<scorecard> _scorecards_FK_ScoreCard_Games_Recno;
        [Association(Storage = "_scorecards_FK_ScoreCard_Games_Recno", ThisKey = "Recno", OtherKey = "GameRecno")]
        public EntitySet<scorecard> scorecards_FK_ScoreCard_Games_Recno
        {
            get { return this.scorecards_FK_ScoreCard_Games_Recno; }
            set { this.scorecards_FK_ScoreCard_Games_Recno.Assign(value); }
        }

        private void attachFK_ScoreCard_GamesRecno(scorecard scorecard)
        {
            base.OnPropertyChanging(() => scorecard);
        }

        private void detachFK_ScoreCard_GamesRecno(scorecard scorecard)
        {
            base.OnPropertyChanging(() => scorecard);
        }


    }

    [Table]
    public class player : TableBase
    {
        public player()
        {
            _scorecards_FK_ScoreCard_Palyers_Recno = new EntitySet<scorecard>(new Action<scorecard>(this.attachFK_ScoreCard_PalyersRecno), new Action<scorecard>(this.detachFK_ScoreCard_PalyersRecno));
        }

        private Int32 _Recno;
        
        //PrimaryKey Property
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public Int32 Recno
        {
            get { return _Recno; }
            set { base.set(() => Recno, ref _Recno, value); }
        }

        private String _Name;
        [Column]
        public String Name
        {
            get { return _Name; }
            set { base.set(() => Name, ref _Name, value); }
        }

        private EntitySet<scorecard> _scorecards_FK_ScoreCard_Palyers_Recno;
        [Association(Storage = "_scorecards_FK_ScoreCard_Palyers_Recno", ThisKey = "Recno", OtherKey = "PalyerRecno")]
        public EntitySet<scorecard> scorecards_FK_ScoreCard_Palyers_Recno
        {
            get { return this.scorecards_FK_ScoreCard_Palyers_Recno; }
            set { this.scorecards_FK_ScoreCard_Palyers_Recno.Assign(value); }
        }

        private void attachFK_ScoreCard_PalyersRecno(scorecard scorecard)
        {
            base.OnPropertyChanging(() => scorecard);
        }

        private void detachFK_ScoreCard_PalyersRecno(scorecard scorecard)
        {
            base.OnPropertyChanging(() => scorecard);
        }


    }

    [Table]
    public class scorecard : TableBase
    {
        public scorecard()
        {
        }

        private Int32 _Recno;
        
        //PrimaryKey Property
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public Int32 Recno
        {
            get { return _Recno; }
            set { base.set(() => Recno, ref _Recno, value); }
        }

        private Int32 _PalyerRecno;
        [Column]
        public Int32 PalyerRecno
        {
            get { return _PalyerRecno; }
            set { base.set(() => PalyerRecno, ref _PalyerRecno, value); }
        }

        private Int32 _GameRecno;
        [Column]
        public Int32 GameRecno
        {
            get { return _GameRecno; }
            set { base.set(() => GameRecno, ref _GameRecno, value); }
        }

        private Int32 _Level;
        [Column]
        public Int32 Level
        {
            get { return _Level; }
            set { base.set(() => Level, ref _Level, value); }
        }

        private Int64 _Score;
        [Column]
        public Int64 Score
        {
            get { return _Score; }
            set { base.set(() => Score, ref _Score, value); }
        }

        private Int32 _Attempt;
        [Column]
        public Int32 Attempt
        {
            get { return _Attempt; }
            set { base.set(() => Attempt, ref _Attempt, value); }
        }

        private DateTime _UpdatedOn;
        [Column]
        public DateTime UpdatedOn
        {
            get { return _UpdatedOn; }
            set { base.set(() => UpdatedOn, ref _UpdatedOn, value); }
        }

        private EntityRef<player> _player_FK_ScoreCard_Palyers_PalyerRecno;

        [Association(Storage = "_player_FK_ScoreCard_Palyers_PalyerRecno", ThisKey = "PalyerRecno", OtherKey = "Recno", IsForeignKey = true)]
        public player player_FK_ScoreCard_Palyers_PalyerRecno
        {
            get { return _player_FK_ScoreCard_Palyers_PalyerRecno.Entity; }
            set
            {
                base.OnPropertyChanging(() => player_FK_ScoreCard_Palyers_PalyerRecno);
                _player_FK_ScoreCard_Palyers_PalyerRecno.Entity = value;
                if (null != value)
                {
                    PalyerRecno = value.Recno;
                }
                base.OnPropertyChanged(() => player_FK_ScoreCard_Palyers_PalyerRecno);

            }
        }

        private EntityRef<game> _game_FK_ScoreCard_Games_GameRecno;

        [Association(Storage = "_game_FK_ScoreCard_Games_GameRecno", ThisKey = "GameRecno", OtherKey = "Recno", IsForeignKey = true)]
        public game game_FK_ScoreCard_Games_GameRecno
        {
            get { return _game_FK_ScoreCard_Games_GameRecno.Entity; }
            set
            {
                base.OnPropertyChanging(() => game_FK_ScoreCard_Games_GameRecno);
                _game_FK_ScoreCard_Games_GameRecno.Entity = value;
                if (null != value)
                {
                    GameRecno = value.Recno;
                }
                base.OnPropertyChanged(() => game_FK_ScoreCard_Games_GameRecno);

            }
        }


    }

    [Table]
    public class setting : TableBase
    {
        public setting()
        {
        }

        private Int32 _Recno;
        
        //PrimaryKey Property
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public Int32 Recno
        {
            get { return _Recno; }
            set { base.set(() => Recno, ref _Recno, value); }
        }

        private Boolean _IsAudioEnabled;
        [Column]
        public Boolean IsAudioEnabled
        {
            get { return _IsAudioEnabled; }
            set { base.set(() => IsAudioEnabled, ref _IsAudioEnabled, value); }
        }

        private Double _Volume;
        [Column]
        public Double Volume
        {
            get { return _Volume; }
            set { base.set(() => Volume, ref _Volume, value); }
        }


    }


}
