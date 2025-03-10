﻿
namespace Bamboomaui.Crypto
{
    public class KeyPair
    {
        public byte[] PublicKey { get; }
        public byte[] PrivateKey { get; }

        public KeyPair(byte[] publicKey, byte[] privateKey)
        {
            if (privateKey.Length % 16 != 0)
                throw new ArgumentOutOfRangeException("Private Key length must be a multiple of 16 bytes.");

            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
    }
}
