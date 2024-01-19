namespace Algorithm
{
    internal class Program
    {
        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;
            
            for(int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, right);

            return i + 1;
        }
        //메인
        static void Main(string[] args)
        {
            int[] arr = new int[] { 5, 2, 4, 6, 1, 3 };

            QuickSort(arr, 0, arr.Length - 1);

            foreach(int num in arr)
            {
                Console.Write(num + " ");
            }

            //SelectionSort();
            //Console.WriteLine();
            //InsertionSort();
        }
        static void SelectionSort()
        {
            int[] arr = new int[] { 5, 2, 4, 6, 1, 3 };

            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                int temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }

            foreach (int num in arr)
                Console.Write(num + " ");
        }

        static void InsertionSort()
        {
            int[] arr = new int[] { 5, 2, 4, 6, 1, 3 };

            for(int i = 0; i < arr.Length; i++)
            {
                int j = i - 1;
                int key = arr[i];

                while( j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }

            foreach(int num in arr)
            {
                Console.Write(num + " ");
            }
        }
        static void QuickSort(int[] arr, int left, int right)
        {
            if(left < right)
            {
                int pivot = Partition(arr, left, right);

                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }
    }
}
