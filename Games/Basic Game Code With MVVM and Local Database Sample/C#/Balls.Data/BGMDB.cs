//------------------------------------------------------------------------------
// <auto-enerated>
//    This code was generated from a template.
//
//    This class helps to initiate the database into isostore and create initial data into the table.
//    Insert actual data in the below 'backgroundWorker_DoWork' section to code run successfully.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.ComponentModel;
using Balls.Data.BGMDB.Designer;

namespace Balls.Data
{
    public sealed class BGMDBDataContextHelper
    {
        /// <summary>
        /// Database file name
        /// </summary>
        public static readonly string ConnectionString = "Data Source=isostore:/BGMDB.sdf";


        public delegate void DabaseCreatedEventHandler(object sender);
        public event DabaseCreatedEventHandler DatabaseCreated;

        private static readonly BGMDBDataContextHelper _BGMDBDataContextHelper = new BGMDBDataContextHelper();

        private BGMDBDataContextHelper() { }

        public static BGMDBDataContextHelper Instance()
        {
            return _BGMDBDataContextHelper;
        }

        public bool CreateInitialData()
        {

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);

            backgroundWorker.RunWorkerAsync();

            return true;
        }
        public bool ResetDatabase()
        {
            if (DeleteDatabase())
                CreateDatabase();


            return true;
        }

        /// <summary>
        /// This method create new database if not exist and also help code for inserting initial data in the table.
        /// </summary>
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateDatabase();
        }


        private bool DeleteDatabase()
        {
            using (BGMDBDataContext context = new BGMDBDataContext(ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    context.DeleteDatabase();
                }
            }

            return true;
        }

        public bool IsExists()
        {
            bool isExists = false;
            using (BGMDBDataContext context = new BGMDBDataContext(ConnectionString))
            {
                isExists = context.DatabaseExists();
            }
            return isExists;
        }

        private void CreateDatabase()
        {
            using (BGMDBDataContext context = new BGMDBDataContext(ConnectionString))
            {
                if (!context.DatabaseExists())
                {

                    context.CreateDatabase();


                    #region Insert actual data in this section

                    //context.scorecards.InsertOnSubmit(
                    //    new scorecard
                    //    {
                    //        Recno = new Int32(),
                    //        Name = new String('S', 1),
                    //        Game = new Int32(),
                    //        Level = new Int32(),
                    //        Score = new Int64(),
                    //        Attempt = new Int32()
                    //    });

                    context.settings.InsertOnSubmit(
                        new setting
                        {
                            Recno = 1,
                            IsAudioEnabled = true,
                            Volume = 1
                        });


                    context.games.InsertOnSubmit(
                       new game
                       {
                           Recno = 1,
                           Name = "Ball Counter"
                       });

                    context.games.InsertOnSubmit(
                        new game
                        {
                            Recno = 2,
                            Name = "Ball Balancer"
                        });


                    #endregion


                    try
                    {
                        context.SubmitChanges();
                        if (null != DatabaseCreated)
                            DatabaseCreated("Success");
                    }
                    catch (Exception)
                    {
                        //Initial Data insertion is unsuccessful then delete the database.
                        context.DeleteDatabase();
                        throw;
                    }
                }
            }
        }
    }
}
