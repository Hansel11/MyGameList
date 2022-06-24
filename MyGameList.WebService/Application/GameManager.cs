using Microsoft.Data.Sqlite;
using MyGameList.Entities;
using MyGameList.Domain.PsdDB;
using MyGameList.Domain.Request;
using MyGameList.Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace MyGameList.WebService.Application
{
    public class GameManager
    {
        public GameManager()
        {
        }

        public static List<GameResponseDTO> GetAllGame(Guid userId)
        {
            var retval = new List<GameResponseDTO>();
            try
            {
                using (var context = new EntityContext<MsGame>())
                {
                    retval = (from g in context.Data
                              where g.UserId == userId
                              select new GameResponseDTO
                              {
                                  Id = g.Id,
                                  Name = g.Name,
                                  Rating = g.Rating,
                                  Genre = g.Genre
                              }).ToList();
                }

                return retval;
            }
            catch (Exception) { }
            return null;
        }

        public static GameResponseDTO GetGame(Guid id)
        {
            var retval = new GameResponseDTO();
            try
            {
                using (var context = new EntityContext<MsGame>())
                {
                    retval = (from g in context.Data
                              where g.Id == id
                              select new GameResponseDTO
                              {
                                  Id = g.Id,
                                  Name = g.Name,
                                  Rating = g.Rating,
                                  Genre = g.Genre
                              }).FirstOrDefault();
                }

                return retval;
            }
            catch (Exception ex){
                var s = ex;
            }
            return null;
        }

        public static void AddGame(GameRequestDTO req)
        {
            try
            {
                using (var context = new EntityContext<MsGame>())
                {
                    var game = new MsGame
                    {
                        Id = Guid.NewGuid(),
                        UserId = req.UserId,
                        Name = req.Name,
                        Rating = req.Rating,
                        Genre = req.Genre
                    };
                    context.Add(game);
                    context.SaveChanges();
                }
            }
            catch (Exception) { }
        }

        public static void EditGame(GameRequestDTO req)
        {
            try
            {
                using (var context = new EntityContext<MsGame>())
                {
                    var game = new MsGame
                    {
                        Id = req.Id,
                        UserId = req.UserId,
                        Name = req.Name,
                        Rating = req.Rating,
                        Genre = req.Genre
                    };
                    context.Update(game);
                    context.SaveChanges();
                }
            }
            catch (Exception) { }
        }

        public static void RemoveGame(Guid gameId)
        {
            try
            {
                using (var context = new EntityContext<MsGame>())
                {
                    var game = new MsGame
                    {
                        Id = gameId
                    };
                    context.Remove(game);
                    context.SaveChanges();
                }
            }
            catch (Exception) { }
        }
    }
}