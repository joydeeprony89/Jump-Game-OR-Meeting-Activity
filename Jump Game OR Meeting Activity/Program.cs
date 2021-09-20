using System.Collections.Generic;

namespace Jump_Game_OR_Meeting_Activity
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 11, 22, 7, 7, 7, 7, 7, 7, 7, 22, 13 }; // 100, -23, -23, 404, 100, 23, 23, 23, 3, 404
            System.Console.WriteLine(MinJumps(arr));
        }

        // reference - https://www.youtube.com/watch?v=JYbU8RH1OSQ
        static int MinJumps(int[] arr)
        {
            // Base condition.
            int length = arr.Length;
            if (length == 1) return 0;
            // Create the hash
            Dictionary<int, List<int>> hash = new Dictionary<int, List<int>>();
            for (int i = 0; i < length; i++)
            {
                if (!hash.ContainsKey(arr[i]))
                    hash.Add(arr[i], new List<int>());
                hash[arr[i]].Add(i);
            }

            int jumps = 0;
            // Queue to track the jump indexes.
            Queue<int> q = new Queue<int>();
            q.Enqueue(0);

            // visited array to skip duplicate index processing
            bool[] visited = new bool[length];
            visited[0] = true;
            while (q.Count > 0)
            {
                jumps++;
                int size = q.Count;
                while (size-- > 0)
                {
                    int j = q.Dequeue();
                    // j-1
                    if ((j - 1) >= 0 && hash.ContainsKey(arr[j - 1]) && !visited[j - 1])
                    {
                        q.Enqueue(j - 1);
                        visited[j - 1] = true;
                    }

                    // j+1
                    if ((j + 1 < length) && hash.ContainsKey(arr[j + 1]) && !visited[j + 1])
                    {
                        if (j + 1 == length - 1) return jumps;
                        q.Enqueue(j + 1);
                        visited[j + 1] = true;
                    }


                    // arr[j] == arr[k], while j != k
                    if (hash.ContainsKey(arr[j]))
                    {
                        var values = hash[arr[j]];
                        foreach (int k in values)
                        {
                            if (k != j && !visited[k])
                            {
                                if (k == length - 1) return jumps;
                                q.Enqueue(k);
                                visited[k] = true;
                            }
                        }
                    }

                    hash.Remove(arr[j]);
                }
            }

            return jumps;
        }
    }
}
