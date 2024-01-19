namespace ShortestPathProblem
{
    internal class Program
    {

    }

    class DijkstraExample
    {
        static int V = 6; // 정점의 수

        // 주어진 그래프의 최단 경로를 찾는 다익스트라 알고리즘
        static void Dijkstra(int[,] graph, int start)
        {
            int[] distance = new int[V]; // 시작 정점으로부터의 거리 배열
            bool[] visited = new bool[V]; // 방문 여부 배열

            // 거리 배열 초기화
            for (int i = 0; i < V; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[start] = 0; // 시작 정점의 거리는 0

            // 모든 정점을 방문할 때까지 반복
            for (int count = 0; count < V - 1; count++)
            {
                // 현재 방문하지 않은 정점들 중에서 최소 거리를 가진 정점을 찾음
                int minDistance = int.MaxValue;
                int minIndex = -1;

                for (int v = 0; v < V; v++)
                {
                    if (!visited[v] && distance[v] <= minDistance)
                    {
                        minDistance = distance[v];
                        minIndex = v;
                    }
                }

                // 최소 거리를 가진 정점을 방문 처리
                visited[minIndex] = true;

                // 최소 거리를 가진 정점과 인접한 정점들의 거리 업데이트
                for (int v = 0; v < V; v++)
                {
                    if (!visited[v] && graph[minIndex, v] != 0 && distance[minIndex] != int.MaxValue && distance[minIndex] + graph[minIndex, v] < distance[v])
                    {
                        distance[v] = distance[minIndex] + graph[minIndex, v];
                    }
                }
            }

            // 최단 경로 출력
            Console.WriteLine("정점\t거리");
            for (int i = 0; i < V; i++)
            {
                Console.WriteLine($"{i}\t{distance[i]}");
            }
        }

        static void Main(string[] args)
        {
            int[,] graph = {
            { 0, 4, 0, 0, 0, 0 },
            { 4, 0, 8, 0, 0, 0 },
            { 0, 8, 0, 7, 0, 4 },
            { 0, 0, 7, 0, 9, 14 },
            { 0, 0, 0, 9, 0, 10 },
            { 0, 0, 4, 14, 10, 0 }
        };

            int start = 0; // 시작 정점

            Dijkstra(graph, start);
        }
    }
}
