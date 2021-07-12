using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Models;
using Balls.Data.BGMDB.Designer;
using Balls.Data;

namespace Balls.Business.Services
{
    /// <summary>
    /// This class helps to get score information of players
    /// </summary>
    public class ScoreCardService
    {
        /// <summary>
        /// Get all players name with record number
        /// </summary>
        /// <returns></returns>
        public List<PlayerModel> GetAllPlayers()
        {
            List<PlayerModel> listOfPlayers = null;


            using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
            {
                listOfPlayers = (from result in context.players
                                 select new PlayerModel
                                 {
                                     Recno = result.Recno,
                                     Name = result.Name,
                                 }).ToList();
            }



            return listOfPlayers;
        }


        /// <summary>
        /// Get all player information wth scorecard details
        /// </summary>
        /// <returns></returns>
        public List<PlayerModel> GetAllPlayerDetails()
        {
            List<PlayerModel> listOfPlayers = null;


            using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
            {
                listOfPlayers = (from result in context.players
                                 select new PlayerModel
                                 {
                                     Recno = result.Recno,
                                     Name = result.Name,
                                     ScoreCards = result.scorecards_FK_ScoreCard_Palyers_Recno.Select(x => new ScoreCardModel
                                     {
                                         Recno = x.Recno,
                                         Level = x.Level,
                                         Score = x.Score,
                                         Attempt = x.Attempt,
                                         UpdatedOn = x.UpdatedOn,
                                         GameRecno = x.GameRecno
                                     }).ToList()
                                 }).ToList();
            }



            return listOfPlayers;
        }


        /// <summary>
        /// Get a player information with scorecard details
        /// </summary>
        /// <param name="recno"></param>
        /// <returns></returns>
        public PlayerModel GetAPlayerDetail(int recno)
        {
            PlayerModel player = null;

            using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
            {
                var val = context.scorecards.Select(x => x).ToList();
                var v1 = val;

                player = (from result in context.players
                          where result.Recno == recno
                          select new PlayerModel
                          {
                              Recno = result.Recno,
                              Name = result.Name,
                              ScoreCards = result.scorecards_FK_ScoreCard_Palyers_Recno.Select(x => new ScoreCardModel
                              {
                                  Recno = x.Recno,
                                  Level = x.Level,
                                  Score = x.Score,
                                  Attempt = x.Attempt,
                                  UpdatedOn = x.UpdatedOn,
                                  GameRecno = x.GameRecno
                              }).ToList()
                          }).FirstOrDefault();


            }

            return player;
        }

        /// <summary>
        /// Add or update player and scorecard details
        /// </summary>
        /// <param name="playerModel"></param>
        /// <returns></returns>
        public PlayerModel AddUpdatePlayerDetail(PlayerModel playerModel)
        {

            int playerRecno = AddUpdatePlayer(playerModel);
            if (playerRecno > 0)
                using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
                {
                    var scList = playerModel.ScoreCards.Where(x => x.IsNeedUpdateDB).Select(x => x).ToList();
                    if (null != scList && scList.Count > 0)
                    {
                        foreach (var sc in scList)
                        {

                            if (sc.Recno > 0)
                            {
                                var rsc = (from result in context.scorecards
                                           where result.Recno == sc.Recno
                                           select result).FirstOrDefault();
                                if (null != rsc)
                                {
                                    rsc.Level = sc.Level;
                                    rsc.Score = sc.Score;
                                    rsc.Attempt = sc.Attempt;
                                    rsc.UpdatedOn = DateTime.Now;

                                }
                            }
                            else
                            {
                                context.scorecards.InsertOnSubmit(
                                    new scorecard
                                    {
                                        Level = sc.Level,
                                        Score = sc.Score,
                                        Attempt = sc.Attempt,
                                        UpdatedOn = DateTime.Now,
                                        PalyerRecno = playerRecno,
                                        GameRecno = sc.GameRecno
                                    });
                            }
                        }
                        context.SubmitChanges();
                    }

                }
            return GetAPlayerDetail(playerRecno);
        }

        /// <summary>
        /// Add or update plyer
        /// </summary>
        /// <param name="playerModel"></param>
        /// <returns></returns>
        private int AddUpdatePlayer(PlayerModel playerModel)
        {
            int val = playerModel.Recno;

            if (val == 0)
                using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
                {
                    var player = new player { Name = playerModel.Name };
                    context.players.InsertOnSubmit(player);

                    context.SubmitChanges();
                    val = player.Recno;
                }

            return val;
        }

    }
}
