namespace DuplicateDetectionFuzzy
{
    public class Customer
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class DuplicateGroup
    {
        public List<string> CustomerIds { get; set; } = new();
        public string Reason { get; set; } = string.Empty;
    }

    public class DuplicateDetector
    {
        public List<DuplicateGroup> FindDuplicates(List<Customer> customers)
        {
            var groups = new List<DuplicateGroup>();
            var processed = new HashSet<string>();

            for (int i = 0; i < customers.Count; i++)
            {
                if (processed.Contains(customers[i].CustomerId))
                    continue;

                var duplicates = new List<string> { customers[i].CustomerId };
                var reasons = new List<string>();

                for (int j = i + 1; j < customers.Count; j++)
                {
                    if (processed.Contains(customers[j].CustomerId))
                        continue;

                    var isDuplicate = false;
                    var reason = "";

                    if (!string.IsNullOrWhiteSpace(customers[i].Phone) && 
                        customers[i].Phone == customers[j].Phone)
                    {
                        isDuplicate = true;
                        reason = "Same phone";
                    }
                    else if (!string.IsNullOrWhiteSpace(customers[i].Email) && 
                             customers[i].Email.Equals(customers[j].Email, StringComparison.OrdinalIgnoreCase))
                    {
                        isDuplicate = true;
                        reason = "Same email";
                    }
                    else if (GetSimilarity(customers[i].Name, customers[j].Name) >= 0.8)
                    {
                        isDuplicate = true;
                        reason = "Similar name";
                    }

                    if (isDuplicate)
                    {
                        duplicates.Add(customers[j].CustomerId);
                        processed.Add(customers[j].CustomerId);
                        if (!reasons.Contains(reason))
                            reasons.Add(reason);
                    }
                }

                if (duplicates.Count > 1)
                {
                    processed.Add(customers[i].CustomerId);
                    groups.Add(new DuplicateGroup
                    {
                        CustomerIds = duplicates,
                        Reason = string.Join(", ", reasons)
                    });
                }
            }

            return groups;
        }

        private double GetSimilarity(string s1, string s2)
        {
            if (string.IsNullOrWhiteSpace(s1) || string.IsNullOrWhiteSpace(s2))
                return 0;

            var distance = LevenshteinDistance(s1.ToLower(), s2.ToLower());
            var maxLength = Math.Max(s1.Length, s2.Length);
            return 1.0 - ((double)distance / maxLength);
        }

        private int LevenshteinDistance(string s1, string s2)
        {
            int[,] d = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                d[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= s2.Length; j++)
            {
                for (int i = 1; i <= s1.Length; i++)
                {
                    int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[s1.Length, s2.Length];
        }
    }
}
