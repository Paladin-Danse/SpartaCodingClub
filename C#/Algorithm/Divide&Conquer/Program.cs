namespace Divide_Conquer
{
    internal class Program
    {
        // 문제: 주어진 배열을 정렬하는 함수를 작성하세요. (퀵 정렬 사용)
        static public void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);  // Before pi
                QuickSort(arr, pi + 1, high); // After pi
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
        }

        static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        static int[] Weights_Sort(int[] Weight)
        {
            QuickSort(Weight, 0, Weight.Length - 1);

            return Weight;
        }

        static void Main(string[] args)
        {
            int[] Weights = new int[] { 5, 3, 9, 1, 7 };

            Weights = Weights_Sort(Weights);
            for(int i = 0; i < Weights.Length; i++)
            {
                Console.Write(Weights[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
