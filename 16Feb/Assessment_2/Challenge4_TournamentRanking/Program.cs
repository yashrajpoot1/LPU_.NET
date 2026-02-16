using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge4_TournamentRanking
{
    public class Team : IComparable<Team>
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        public int GoalDifference => GoalsFor - GoalsAgainst;

        public Team(string name)
        {
            Name = name;
            Points = 0;
            MatchesPlayed = 0;
            Wins = 0;
            Losses = 0;
            GoalsFor = 0;
            GoalsAgainst = 0;
        }

        public int CompareTo(Team other)
        {
            if (other == null) return 1;

            // Compare by points descending
            int pointsCompare = other.Points.CompareTo(Points);
            if (pointsCompare != 0) return pointsCompare;

            // If points are equal, compare by goal difference
            int gdCompare = other.GoalDifference.CompareTo(GoalDifference);
            if (gdCompare != 0) return gdCompare;

            // If still equal, compare by name alphabetically
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{Name} - Points: {Points}, W: {Wins}, L: {Losses}, GD: {GoalDifference:+#;-#;0}";
        }

        public Team Clone()
        {
            return new Team(Name)
            {
                Points = this.Points,
                MatchesPlayed = this.MatchesPlayed,
                Wins = this.Wins,
                Losses = this.Losses,
                GoalsFor = this.GoalsFor,
                GoalsAgainst = this.GoalsAgainst
            };
        }
    }

    public class Match
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool IsPlayed { get; set; }

        public Match(Team team1, Team team2, DateTime scheduledDate)
        {
            Team1 = team1;
            Team2 = team2;
            ScheduledDate = scheduledDate;
            IsPlayed = false;
        }

        public Match Clone()
        {
            return new Match(Team1, Team2, ScheduledDate)
            {
                Team1Score = this.Team1Score,
                Team2Score = this.Team2Score,
                IsPlayed = this.IsPlayed
            };
        }

        public override string ToString()
        {
            if (IsPlayed)
                return $"{Team1.Name} {Team1Score} - {Team2Score} {Team2.Name}";
            else
                return $"{Team1.Name} vs {Team2.Name} (Scheduled: {ScheduledDate:yyyy-MM-dd})";
        }
    }

    public class Tournament
    {
        private List<Team> _teams = new List<Team>();
        private LinkedList<Match> _schedule = new LinkedList<Match>();
        private Stack<(Match match, Team team1State, Team team2State)> _undoStack = new Stack<(Match, Team, Team)>();

        // Add team to tournament
        public void AddTeam(Team team)
        {
            if (_teams.Any(t => t.Name == team.Name))
            {
                Console.WriteLine($"Error: Team {team.Name} already exists.");
                return;
            }

            _teams.Add(team);
            Console.WriteLine($"Added team: {team.Name}");
        }

        // Add match to schedule
        public void ScheduleMatch(Match match)
        {
            if (match == null)
            {
                Console.WriteLine("Error: Cannot schedule null match.");
                return;
            }

            _schedule.AddLast(match);
            Console.WriteLine($"Scheduled: {match}");
        }

        // Record match result and update rankings
        public void RecordMatchResult(Match match, int team1Score, int team2Score)
        {
            if (match == null || !_schedule.Contains(match))
            {
                Console.WriteLine("Error: Match not found in schedule.");
                return;
            }

            // Save state for undo
            _undoStack.Push((match.Clone(), match.Team1.Clone(), match.Team2.Clone()));

            // Update match
            match.Team1Score = team1Score;
            match.Team2Score = team2Score;
            match.IsPlayed = true;

            // Update team statistics
            match.Team1.MatchesPlayed++;
            match.Team2.MatchesPlayed++;
            match.Team1.GoalsFor += team1Score;
            match.Team1.GoalsAgainst += team2Score;
            match.Team2.GoalsFor += team2Score;
            match.Team2.GoalsAgainst += team1Score;

            // Determine winner and award points
            if (team1Score > team2Score)
            {
                match.Team1.Points += 3;
                match.Team1.Wins++;
                match.Team2.Losses++;
                Console.WriteLine($"Match result: {match.Team1.Name} wins! {match}");
            }
            else if (team2Score > team1Score)
            {
                match.Team2.Points += 3;
                match.Team2.Wins++;
                match.Team1.Losses++;
                Console.WriteLine($"Match result: {match.Team2.Name} wins! {match}");
            }
            else
            {
                match.Team1.Points += 1;
                match.Team2.Points += 1;
                Console.WriteLine($"Match result: Draw! {match}");
            }
        }

        // Undo last match
        public void UndoLastMatch()
        {
            if (_undoStack.Count == 0)
            {
                Console.WriteLine("No matches to undo.");
                return;
            }

            var (match, team1State, team2State) = _undoStack.Pop();

            // Find the actual match and teams in the schedule
            var actualMatch = _schedule.FirstOrDefault(m => 
                m.Team1.Name == match.Team1.Name && m.Team2.Name == match.Team2.Name);

            if (actualMatch != null)
            {
                // Restore match state
                actualMatch.Team1Score = match.Team1Score;
                actualMatch.Team2Score = match.Team2Score;
                actualMatch.IsPlayed = false;

                // Restore team states
                RestoreTeamState(actualMatch.Team1, team1State);
                RestoreTeamState(actualMatch.Team2, team2State);

                Console.WriteLine($"Undone: {actualMatch.Team1.Name} vs {actualMatch.Team2.Name}");
            }
        }

        private void RestoreTeamState(Team team, Team savedState)
        {
            team.Points = savedState.Points;
            team.MatchesPlayed = savedState.MatchesPlayed;
            team.Wins = savedState.Wins;
            team.Losses = savedState.Losses;
            team.GoalsFor = savedState.GoalsFor;
            team.GoalsAgainst = savedState.GoalsAgainst;
        }

        // Get ranking position
        public int GetTeamRanking(Team team)
        {
            var sortedTeams = GetRankings();
            return sortedTeams.FindIndex(t => t.Name == team.Name) + 1;
        }

        // Get sorted rankings
        public List<Team> GetRankings()
        {
            return _teams.OrderBy(t => t).ToList();
        }

        // Get upcoming matches
        public IEnumerable<Match> GetUpcomingMatches()
        {
            return _schedule.Where(m => !m.IsPlayed).OrderBy(m => m.ScheduledDate);
        }

        // Get completed matches
        public IEnumerable<Match> GetCompletedMatches()
        {
            return _schedule.Where(m => m.IsPlayed);
        }

        // Get team statistics
        public void PrintStandings()
        {
            Console.WriteLine("\n=== Tournament Standings ===");
            Console.WriteLine($"{"Rank",-5} {"Team",-20} {"P",-4} {"W",-4} {"L",-4} {"GF",-4} {"GA",-4} {"GD",-5} {"Pts",-4}");
            Console.WriteLine(new string('-', 70));

            var rankings = GetRankings();
            for (int i = 0; i < rankings.Count; i++)
            {
                var team = rankings[i];
                Console.WriteLine($"{i + 1,-5} {team.Name,-20} {team.MatchesPlayed,-4} {team.Wins,-4} {team.Losses,-4} " +
                                $"{team.GoalsFor,-4} {team.GoalsAgainst,-4} {team.GoalDifference,-5} {team.Points,-4}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Tournament Ranking System ===\n");

            Tournament tournament = new Tournament();

            // Test Case 1: Add teams
            Console.WriteLine("--- Adding Teams ---");
            Team teamAlpha = new Team("Team Alpha");
            Team teamBeta = new Team("Team Beta");
            Team teamGamma = new Team("Team Gamma");
            Team teamDelta = new Team("Team Delta");

            tournament.AddTeam(teamAlpha);
            tournament.AddTeam(teamBeta);
            tournament.AddTeam(teamGamma);
            tournament.AddTeam(teamDelta);

            // Test Case 2: Schedule matches
            Console.WriteLine("\n--- Scheduling Matches ---");
            Match match1 = new Match(teamAlpha, teamBeta, DateTime.Now.AddDays(1));
            Match match2 = new Match(teamGamma, teamDelta, DateTime.Now.AddDays(2));
            Match match3 = new Match(teamAlpha, teamGamma, DateTime.Now.AddDays(3));
            Match match4 = new Match(teamBeta, teamDelta, DateTime.Now.AddDays(4));

            tournament.ScheduleMatch(match1);
            tournament.ScheduleMatch(match2);
            tournament.ScheduleMatch(match3);
            tournament.ScheduleMatch(match4);

            // Test Case 3: Record match results
            Console.WriteLine("\n--- Recording Match Results ---");
            tournament.RecordMatchResult(match1, 3, 1); // Team Alpha wins
            tournament.RecordMatchResult(match2, 2, 2); // Draw
            tournament.RecordMatchResult(match3, 1, 2); // Team Gamma wins

            // Test Case 4: Display standings
            tournament.PrintStandings();

            // Test Case 5: Get team ranking
            Console.WriteLine($"\n--- Team Rankings ---");
            Console.WriteLine($"{teamAlpha.Name} ranking: #{tournament.GetTeamRanking(teamAlpha)}");
            Console.WriteLine($"{teamBeta.Name} ranking: #{tournament.GetTeamRanking(teamBeta)}");
            Console.WriteLine($"{teamGamma.Name} ranking: #{tournament.GetTeamRanking(teamGamma)}");

            // Test Case 6: Undo last match
            Console.WriteLine("\n--- Undoing Last Match ---");
            Console.WriteLine($"Before undo - {teamGamma.Name}: {teamGamma.Points} points");
            tournament.UndoLastMatch();
            Console.WriteLine($"After undo - {teamGamma.Name}: {teamGamma.Points} points"); // Should be back to 1

            tournament.PrintStandings();

            // Test Case 7: Record more matches
            Console.WriteLine("\n--- Recording More Matches ---");
            tournament.RecordMatchResult(match3, 2, 1); // Team Alpha wins this time
            tournament.RecordMatchResult(match4, 0, 3); // Team Delta wins

            tournament.PrintStandings();

            // Test Case 8: View upcoming matches
            Console.WriteLine("\n--- Upcoming Matches ---");
            var upcoming = tournament.GetUpcomingMatches();
            foreach (var match in upcoming)
            {
                Console.WriteLine($"  {match}");
            }

            // Test Case 9: View completed matches
            Console.WriteLine("\n--- Completed Matches ---");
            var completed = tournament.GetCompletedMatches();
            foreach (var match in completed)
            {
                Console.WriteLine($"  {match}");
            }

            Console.WriteLine("\n=== Demo Completed Successfully ===");
        }
    }
}
