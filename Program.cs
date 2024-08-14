
using System.Globalization;

class Program
{
    static void Main()
    {
        
        string inputTable = "/Users/admin/Downloads/exhibitA-input.csv";  

        
        Dictionary<int, HashSet<int>> clientPlayCount = new Dictionary<int, HashSet<int>>();

       
        var lines = File.ReadAllLines(inputTable);


        foreach (var line in lines.Skip(1)) 
        {
            var columns = line.Split('\t').Select(c => c.Trim()).ToArray();
            //Console.WriteLine(columns.Length);


            string dateFormat = "dd/MM/yyyy HH:mm:ss";
            try
            {
                string playId = columns[0];
                int songId = int.Parse(columns[1]); 
                int clientId = int.Parse(columns[2]);
                DateTime playTs;
                bool playTsParsed = DateTime.TryParseExact(columns[3], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out playTs);

               
                if (playTs.Date == new DateTime(2016, 8, 10))
                {
                   
                    if (!clientPlayCount.ContainsKey(clientId))
                        clientPlayCount[clientId] = new HashSet<int>();

                    clientPlayCount[clientId].Add(songId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                continue;
            }
        }


        Dictionary<int, int> distinctPlayCount = new Dictionary<int, int>();

        foreach (var client in clientPlayCount)
        {
            int distinctSongCount = client.Value.Count;

            if (!distinctPlayCount.ContainsKey(distinctSongCount))
                distinctPlayCount[distinctSongCount] = 0;

            distinctPlayCount[distinctSongCount]++;
        }

        Console.WriteLine("DistinctSongCount,ClientCount");
        foreach (var e in distinctPlayCount.OrderBy(e => e.Key))
        {
            Console.WriteLine($"{e.Key},{e.Value}");
        }

 
        Console.ReadLine();
    }

}
