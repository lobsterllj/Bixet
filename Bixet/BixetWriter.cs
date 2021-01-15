﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bixet
{
    public class BixetWriter
    {
        public const string Verion = "0.0.1";
        public const int maxBytesSize = 8;
        public const int maxBitsSize = 64;
        private readonly BitArray bits;
        private readonly Endian byteEndian;
        private readonly Endian bitEndian;
        public int BytesCount { get { return this.bits.Length / 8; } }
        public int BitsCount { get { return this.bits.Count; } }

        public BixetWriter(int byteLength, Endian byteEndian = Endian.BigEndian, Endian bitEndian = Endian.SmallEndian)
        {
            if (byteLength <= 0) throw new IndexOutOfRangeException("给定的参数异常");
            this.bits = new BitArray(byteLength * 8);
            this.byteEndian = byteEndian;
            this.bitEndian = bitEndian;
        }

        public byte this[int i]
        {
            set => this.SetSingleByte(i, value);
        }

        public byte this[int i, int j]
        {
            set => this.SetSingleBit(i * 8 + j, value);
        }

        private void SetSingleByte(int index, byte b)
        {
            if(index >= this.BytesCount) throw new IndexOutOfRangeException("给定的参数异常");
            BitArray tmp = new BitArray(b);
            index *= 8;
            for(int i = 7; i >= 0; --i)
            {
                this.bits[index++] = tmp[i];
            }
        }

        private void SetSingleBit(int index, byte b)
        {
            if (index >= this.BitsCount || b > 1) throw new IndexOutOfRangeException("给定的参数异常");
            this.bits[index] = b != 0;
        }

        public byte[] GetWholeData()
        {
            byte[] res = new byte[this.BytesCount];
            BitArray bits = new BitArray(this.bits);
            if(this.bitEndian == Endian.SmallEndian)
            {
                BixetUtil.ReverseBitEndian(bits);
            }
            bits.CopyTo(res, 0);
            return res;
        }
    }
}