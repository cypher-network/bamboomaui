
using System.Runtime.InteropServices;
using static Bamboomaui.Crypto.Secp256K1Native;
using static Bamboomaui.Crypto.BulletProofNative;

namespace Bamboomaui.Crypto
{
    public class BulletProofs : IDisposable
    {
        public IntPtr Context { get; private set; }

        private IntPtr _gens;
        
        public BulletProofs()
        {
            Context = secp256k1_context_create((uint)(Flags.SECP256K1_CONTEXT_SIGN | Flags.SECP256K1_CONTEXT_VERIFY));
        }

        public BulletProofs(IntPtr ctx)
        {
            Context = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IntPtr Generators(IntPtr ctx)
        {
            _gens = secp256k1_bulletproof_generators_create(ctx, Constant.GENERATOR_G, 256);
            return _gens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="blind"></param>
        /// <param name="nonce"></param>
        /// <param name="rewindNonce"></param>
        /// <param name="extraCommit"></param>
        /// <param name="msg"></param>
        /// <param name="mValue"></param>
        /// <returns></returns>
        public ProofStruct Prove(ulong value, byte[] blind, byte[] nonce, byte[] rewindNonce, byte[] extraCommit, byte[] msg, int mValue = 0)
        {
            byte[] proof = new byte[Constant.MAX_PROOF_SIZE];
            int plen = Constant.MAX_PROOF_SIZE;
            var extraCommitLen = extraCommit == null ? 0 : extraCommit.Length;
            byte[] tau_x = null;
            byte[] t_one = null;
            byte[] t_two = null;
            byte[] commits = null;

            var blinds = new IntPtr[1];

            IntPtr ptr = Marshal.AllocHGlobal(32);
            Marshal.Copy(blind, 0, ptr, blind.Length);
            blinds[0] = ptr;

            IntPtr[] values = new IntPtr[1];
            values[0] = (IntPtr)value;

            IntPtr[] mvalues = null;

            if (mValue != 0)
            {
                mvalues = new IntPtr[1];
                mvalues[0] = (IntPtr)mValue;
            }

            var gens = Generators(Context);
            var scratch = secp256k1_scratch_space_create(Context, Constant.SCRATCH_SPACE_SIZE);
            var result = secp256k1_bulletproof_rangeproof_prove(
                            Context,
                            scratch,
                            gens,
                            proof,
                            ref plen,
                            tau_x,
                            t_one,
                            t_two,
                            values,
                            mvalues,
                            blinds,
                            commits,
                            1,
                            Constant.GENERATOR_H,
                            64,
                            nonce,
                            rewindNonce,
                            extraCommit,
                            extraCommitLen,
                            msg);

            if (result == 1)
            {
                Array.Resize(ref proof, plen);
            }

            _ = secp256k1_scratch_space_destroy(scratch);

            return new ProofStruct(proof, (uint)plen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ProofInfoStruct Rewind()
        {
            return new ProofInfoStruct();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commit"></param>
        /// <param name="proof"></param>
        /// <param name="extraCommit"></param>
        /// <param name="mValue"></param>
        /// <returns></returns>
        public bool Verify(byte[] commit, byte[] proof, byte[] extraCommit, int mValue = 0)
        {
            var extraCommitLen = extraCommit?.Length ?? 0;

            if (_gens  == IntPtr.Zero)
            {
                _gens = Generators(Context);
            }
            var scratch = secp256k1_scratch_space_create(Context, Constant.SCRATCH_SPACE_SIZE);

            IntPtr[] mvalues = null;

            if (mValue != 0)
            {
                mvalues = new IntPtr[1];
                mvalues[0] = (IntPtr)mValue;
            }

            bool success = secp256k1_bulletproof_rangeproof_verify(
                            Context,
                            scratch,
                            _gens,
                            proof,
                            proof.Length,
                            mvalues,
                            commit,
                            1,
                            64,
                            Constant.GENERATOR_H,
                            extraCommit,
                            extraCommitLen) == 1;

            _ = secp256k1_scratch_space_destroy(scratch);

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (Context == IntPtr.Zero) return;
            secp256k1_context_destroy(Context);
            Context = IntPtr.Zero;
        }
    }
}
