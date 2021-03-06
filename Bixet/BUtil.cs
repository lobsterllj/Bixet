﻿using System;

namespace Bixet
{
    public static class BUtil
    {
        public const string version = "0.2.0";

        public static void ReverseByteEndian(byte[] bytes, int begin, int end)
        {
            if (begin < 0 || end < 0 || begin >= end) throw new ArgumentOutOfRangeException("待转换序号有误");
            byte tmp;
            while (begin < end)
            {
                tmp = bytes[begin];
                bytes[begin++] = bytes[end];
                bytes[end--] = tmp;
            }
        }

        public static void ReverseByteEndian(byte[] bytes)
        {
            BUtil.ReverseByteEndian(bytes, 0, bytes.Length - 1);
        }

        public static void ReverseBitEndian(System.Collections.BitArray bits, int begin, int end)
        {
            if (begin < 0 || end < 0 || begin >= end) throw new ArgumentOutOfRangeException("待转换序号有误");
            int count = end - begin + 1;
            if(count % 8 != 0) throw new FormatException("待转换比特不为整字节");
            bool tmp;
            for (int i = begin; i <= end; i += 8)
            {
                for (int j = 0; j < 4; ++j)
                {
                    tmp = bits[i + j];
                    bits[i + j] = bits[i + 7 - j];
                    bits[i + 7 - j] = tmp;
                }
            }
        }

        public static void ReverseBitEndian(System.Collections.BitArray bits)
        {
            BUtil.ReverseBitEndian(bits, 0, bits.Count - 1);
        }

        public static void ReverseBitEndian(byte[] bytes)
        {
            System.Collections.BitArray bits = new System.Collections.BitArray(bytes);
            BUtil.ReverseBitEndian(bits);
            bits.CopyTo(bytes, 0);
        }

        public static void ReverseBitsOrder(System.Collections.BitArray bits, int begin, int end)
        {
            bool bit;
            while(begin < end)
            {
                bit = bits[end];
                bits[end--] = bits[begin];
                bits[begin++] = bit;
            }
        }

        public static void ReverseBitsOrder(System.Collections.BitArray bits)
        {
            BUtil.ReverseBitsOrder(bits, 0, bits.Count - 1);
        }

        public static void ReverseBitsOrder(byte[] bytes)
        {
            System.Collections.BitArray bits = new System.Collections.BitArray(bytes);
            BUtil.ReverseBitsOrder(bits);
            bits.CopyTo(bytes, 0);
        }

        public static uint BitLengthOfType(Type T)
        {
            if (T == typeof(bool)) return 1;
            else if (T == typeof(sbyte)) return 8;
            else if (T == typeof(byte)) return 8;
            else if (T == typeof(short)) return 16;
            else if (T == typeof(ushort)) return 16;
            else if (T == typeof(int)) return 32;
            else if (T == typeof(uint)) return 32;
            else if (T == typeof(long)) return 64;
            else if (T == typeof(ulong)) return 64;
            return 0;
        }

        public static uint ByteLengthOfType(Type T)
        {
            return BUtil.BitLengthOfType(T) / 8;
        }
    }
}
