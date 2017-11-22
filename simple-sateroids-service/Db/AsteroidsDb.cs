using simple_sateroids_service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simple_sateroids_service.Db
{
    public static class AsteroidsDb
    {
        public static List<ScoreEntry> GetLeaderboard()
        {
            return Pg.Db.Query<ScoreEntry>("SELECT * FROM public.leaderboard;");
        }

        public static void AddScore(string name, int score)
        {
            var existing = Pg.Db.Query<ScoreEntry>($"SELECT * FROM public.leaderboard WHERE name = '{name}';").FirstOrDefault();
            if (existing != null)
                Pg.Db.Execute($"UPDATE public.leaderboard SET score = {score} WHERE name = '{name}'; ");
            else
                Pg.Db.Execute($"INSERT INTO public.leaderboard(name, score) VALUES('{name}', {score}); ");
        }
    }
}
