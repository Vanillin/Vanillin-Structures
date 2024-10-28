using System;

namespace MultiThreading
{
    public readonly struct MyByte
    {
        private readonly int[] Bytes;
        public MyByte(Byte b)
        {
            int ToInt = (int)b;
            int[] MemoryBytes = new int[8];
            int i = 0;
            while (ToInt != 0)
            {
                MemoryBytes[i] = ToInt % 2;
                ToInt /= 2;
                i++;
            }

            Bytes = new int[i];
            for (int j = 0; j < i; j++)
            {
                Bytes[j] = MemoryBytes[j];
            }
        }
        private MyByte(int[] Bytes)
        {
            foreach(int b in Bytes)
            {
                if (b != 0 && b != 1) throw new ArgumentException("Неверно задан MyByte");
            }
            this.Bytes = Bytes;
        }
        public bool IsNull()
        {
            if (Bytes == null || Bytes.Length == 0)
            {
                return true;
            }
            return false;
        }
        public static MyByte operator +(MyByte a, MyByte b)
        {
            int[] retur;
            int count = Math.Max(a.Bytes.Length, b.Bytes.Length);
            int[] NewBytes = new int[count];
            int memory = 0;
            for (int i = 0; i < count; i++)
            {
                int sum = memory;
                if (i < a.Bytes.Length)
                {
                    sum += a.Bytes[i];
                }
                if (i < b.Bytes.Length)
                {
                    sum += b.Bytes[i];
                }
                NewBytes[i] = sum % 2;
                memory = sum / 2;
            }
            if (memory == 1)
            {
                retur = new int[count + 1];
                for (int i = 0; i < count; i++)
                {
                    retur[i] = NewBytes[i];
                }
                retur[count] = 1;
            }
            else
            {
                retur = NewBytes;
            }
            return new MyByte(retur);
        }
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Bytes.Length; i++)
            {
                ret = $"{Bytes[i]}{ret}";
            }
            return ret;
        }

    }
}
