﻿using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Test66bit.BLL.DTO;
using Test66bit.BLL.Interfaces;
using Test66bit.DAL;
using Test66bit.DAL.EF;
using Test66bit.DAL.Entities;

namespace Test66bit.BLL.Implement;

public class PlayerService : IPlayerService
{
    private FootballContext db;

    public PlayerService(FootballContext footballContext)
    {
        this.db = footballContext;
    }
    
    /// <summary>
    /// Allows you to add one player to the DB PlayerContext
    /// </summary>
    /// <param name="playerDTO">Light Player Model</param>
    public void AddPlayer(PlayerDTO playerDTO)
    {
        Player player = new ()
        {
            Forename = playerDTO.Forename,
            Surname = playerDTO.Surname,
            Sex = playerDTO.Sex,
            Birthday = playerDTO.Birthday,
            TeamName = playerDTO.TeamName,
            Country = playerDTO.Country,
        };
        db.Players.AddRange(player);
        db.SaveChanges();
    }

    /// <returns>All players from DB PlayerContext</returns>
    public IEnumerable<PlayerDTO> GetAllPlayers()
    {
        var mapper = new MapperConfiguration(cfg 
            => cfg.CreateMap<Player, PlayerDTO>()).CreateMapper();   
        return mapper.Map<IEnumerable<Player>, List<PlayerDTO>>(db.Players.ToList());
    }

    /// <summary>
    /// Allows update one player data
    /// </summary>
    /// <param name="id">Id of the player</param>
    public void UpdatePlayer(PlayerDTO playerDTO)
    {
        var player = db.Players
            .FirstOrDefault(pl => pl.Id == playerDTO.Id);
        if (player == null) return;
        player = new MapperConfiguration(cfg 
            => cfg.CreateMap<PlayerDTO, Player>()).CreateMapper()
            .Map<PlayerDTO, Player>(playerDTO);
        db.Players.Update(player);
        db.SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<TeamDTO> GetAllTeams()
    {
        throw new Exception();
    }
}