﻿namespace MobileKoisk.Services
{
     public class BadgeCounterService
    {

        private static int _count;
        public static event EventHandler<int>? CountChanged;

        public static int Count
        {
            get => _count;
            private set
            {
                if (_count != value)
                {
                    _count = value;
                    OnCountChanged(value);
                }
            }
        }

        public static void SetCount(int newCount)
        {
            Count = newCount;
        }

        public static void IncrementCount()
        {
            Count++;
        }

        public static void DecrementCount()
        {
            Count = Math.Max(0, Count - 1);
        }

        protected static void OnCountChanged(int newCount)
        {
            CountChanged?.Invoke(null, newCount);
        }
        //private static int _count;
        //public static int Count
        //{
        //    get => _count;

        //    private set
        //    {
        //        _count = value;
        //        CountChanged?.Invoke(null, _count);
        //    }
        //}

        //public static void SetCount(int count) => Count = count;

        //public static event EventHandler<int> CountChanged;
    }
}
