﻿using System.Security.Cryptography;

namespace BinarySerialization.Test.Value
{
    public class FieldSha256Attribute : FieldValueAttributeBase
    {
        private readonly SHA256Managed _sha = new SHA256Managed();

        public FieldSha256Attribute(string valuePath) : base(valuePath)
        {
        }

        public override int BlockSize => _sha.InputBlockSize;

        protected override void Reset(BinarySerializationContext context)
        {
            _sha.Initialize();
        }

        protected override void Compute(byte[] buffer, int offset, int count)
        {
            _sha.TransformBlock(buffer, offset, count, buffer, offset);
        }

        protected override object ComputeFinal()
        {
            _sha.TransformFinalBlock(new byte[0], 0, 0);
            return _sha.Hash;
        }
    }
}